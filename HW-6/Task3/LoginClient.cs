using System;
using System.Threading;

namespace Task3
{
    class LoginClient
    {
        public string Login(string login, string pass)
        {
            Random random = new Random();
            int delay = (int)random.NextDouble() * 1000;
            Thread.Sleep(delay);

            return random.NextDouble() < 0.5 ? Guid.NewGuid().ToString() : null;
        }
    }
}
