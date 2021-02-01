using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Task3
{
    class SignInTest
    {
        private ConcurrentQueue<User> users;
        private static LoginClient loginClient = new LoginClient();
        private User user;
        private int failed = 0;
        private int success = 0;

        public SignInTest(ConcurrentQueue<User> queue)
        {
            users = new ConcurrentQueue<User>(queue);
        }

        public Result Test(int threadQuantity)
        {
            if (threadQuantity < 0) threadQuantity = 0;
            CountdownEvent cde = new CountdownEvent(threadQuantity);
            
            for (int i = 0; i < threadQuantity; i++)
            {
                new Thread(
                    () => {
                        ExecuteLogin();
                        cde.Signal();
                    }).Start();
            }
            cde.Wait();

            return new Result(success, failed);
        }

        private void ExecuteLogin()
        {
            while (users.TryDequeue(out user))
            {
                if (loginClient.Login(user.Login, user.Password) == null)
                    Interlocked.Increment(ref failed);
                else
                    Interlocked.Increment(ref success);
            }
        }
    }
}
