using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task1
{
    class JsonWriter
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("primes")]
        public long[] Primes { get; set; }


        public static void ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            JsonReader jr = null;
            string error = null;
            try
            {
                jr = JsonReader.FromJson();
            }
            catch (IOException)
            {
                error = "missing file";
            }
            catch (JsonException)
            {
                error = "json not valid";
            }
            catch (Exception)
            {
                error = "something went wrong...";
            }
            Stopwatch time = new Stopwatch();
            time.Start();
            long[] mas = CalculatePrimeNumbers(jr == null ? 0 : jr.PrimesFrom, jr == null ? 0 : jr.PrimesTo);
            time.Stop();
            string duration = "" + time.Elapsed;
            JsonWriter jw = new JsonWriter() { Primes = mas, Duration = duration, Error = error, Success = mas.Length > 0 };
            File.WriteAllText("result.json", JsonSerializer.Serialize<JsonWriter>(jw, options));

        }

        public static long[] CalculatePrimeNumbers(long k = 0, long n = 0)
        {
            if (k < 0 || n < 0 || k > n) return new long[0];
            if (k < 2) k = 2;
            List<long> list = new List<long>();
            bool isPrime;
            for (long i = k; i <= n; i++)
            {
                isPrime = true;
                for (long j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                    list.Add(i);
            }
            return list.ToArray();


        }
        
    }
}
