using System;
using System.Collections.Concurrent;
using System.IO;

namespace Task3
{
    class AuthReader
    {
        private static ConcurrentQueue<User> conQueue = new ConcurrentQueue<User>();
        public static ConcurrentQueue<User> ParseLoginsAndPass()
        {
            string csv = "";
            try
            {
                csv = File.ReadAllText("logins.csv");
            }
            catch (Exception)
            {
                Console.WriteLine("Error to read from the file, maybe it doesn't exist");
            }

            string[] logins = csv.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < logins.Length; i++)
            {
                string[] auth = logins[i].Split(';');
                conQueue.Enqueue(new User(auth[0], auth[1]));
            }

            return conQueue;
        }
    }
}
