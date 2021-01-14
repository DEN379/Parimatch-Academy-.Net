using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Task21
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Converter currency from NBU, made by Denys Sakadel");
            string startCurrency;
            string endCurrency;
            decimal amount;
            while (true)
            {
                Console.WriteLine("Start currency =>");
                startCurrency = Console.ReadLine();
                Console.WriteLine("End currency =>");
                endCurrency = Console.ReadLine();
                Console.WriteLine("Amount =>");

                try
                {
                    amount = Convert.ToDecimal(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You entered uncorect data, enter pls currency with 3 letters and amount must be > 0");
                    continue;
                }
                if (startCurrency.Length == 3 || endCurrency.Length == 3) break;
            }

            var js = GetJsonFromUri() ?? JsonReader.FromJson();
            if (js == null)
            {
                Console.WriteLine("Didn't get a data. Exiting...");
                return;
            }
            JsonReader objStartCurrency = null;
            JsonReader objEndCurrency = null;
            foreach (JsonReader p in js)
            {
                if (startCurrency.Trim().ToUpper().Equals(p.Cc))
                {
                    objStartCurrency = p;
                }
            }
            foreach (JsonReader p in js)
            {
                if (endCurrency.Trim().ToUpper().Equals(p.Cc))
                {
                    objEndCurrency = p;
                }
            }
            if(objStartCurrency == null || objEndCurrency == null)
            {
                Console.WriteLine($"Error: Pare {startCurrency.Trim().ToUpper()}, " +
                    $"{endCurrency.Trim().ToUpper()} didn't find");
            } 
            else
            {
                decimal result = CashConverter(objStartCurrency, objEndCurrency, amount);
                Console.WriteLine(amount + " " + objStartCurrency.Cc + " => " 
                    + Math.Round(result, 2) + " " + objEndCurrency.Cc + " (" 
                    + objEndCurrency.Exchangedate + ")");
            }
            

        }


        private static decimal CashConverter(JsonReader objStartCurrency, JsonReader objEndCurrency, decimal amount)
        {
            if (objStartCurrency.Cc.Equals("UAH")) return amount / (decimal) objEndCurrency.Rate;
            if (objEndCurrency.Cc.Equals("UAH")) return amount * (decimal)objStartCurrency.Rate;

            return ((decimal) objStartCurrency.Rate * amount) / (decimal) objEndCurrency.Rate;
        }

        private static List<JsonReader> GetJsonFromUri()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var json = webClient.DownloadString("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
                    
                    JToken o = JToken.Parse(json);
                    JsonReader.ToJson(o);
                    
                    var js = JsonConvert.DeserializeObject<List<JsonReader>>(json);
                    return js;
                }
            }
            catch (WebException)
            {
                Console.WriteLine("Error with conection to the server, reading from a cache...");
                return null;
            }
        }
    }
}
