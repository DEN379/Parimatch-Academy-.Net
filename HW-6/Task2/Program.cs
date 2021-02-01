using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Task2
{
    class Program
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private static ThreadSafeList<int> safeList = new ThreadSafeList<int>();
        static void Main(string[] args)
        {
            string error = null;
            string json = "";
            List<Settings> settings = null;

            try
            {
                json = File.ReadAllText("settings.json");
                settings = JsonConvert.DeserializeObject<List<Settings>>(json);
            }
            catch (Exception)
            {
                error = "Json file didn't exist or is corrupted";
            }
            
            var threads = new List<Thread>();
            
            stopwatch.Start();
            if (settings != null)
            {
                foreach (var i in settings)
                {
                    var tread = new Thread(GetPrimeNumbers);
                    tread.Start(i);
                    threads.Add(tread);
                }
                foreach (Thread thread in threads) thread.Join();
            }
            else
            {
                if (error != null) error += " or ";
                error += "Json file is empty";
            }
            stopwatch.Stop();

            var array = safeList.GetElements();
            foreach (int i in array) Console.WriteLine(i);

            Result result = new Result()
            {
                Success = error == null,
                Duration = stopwatch.Elapsed.ToString(),
                Error = error,
                Primes = array
            };

            JsonWriter.Write(result);
        }

        public static void GetPrimeNumbers(object settings)
        {
            var list = PrimeNumbers.GetPrimeNumbers((Settings)settings);

            foreach(int primeNumber in list)
            {
                safeList.Add(primeNumber);
            }
        }
    }
}
