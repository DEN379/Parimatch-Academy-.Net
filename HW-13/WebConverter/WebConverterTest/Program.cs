using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebAppTest;
using WebConverter.Models;

namespace WebConverterTest
{
    class Program
    {
        private static string AuthHeader { get; set; }

        static async Task Main(string[] args)
        {
            Console.WriteLine("WebCoverter test, made by Denys Sakadel\n");

            CultureInfo info = new CultureInfo("en-EN");
            info.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            try
            {
                var json = await File.ReadAllTextAsync("settings.json");
                var settings = JsonSerializer.Deserialize<Settings>(json);
                var client = new HttpClient();
                client.BaseAddress = new Uri(settings.BaseAddress);

                bool task1 = await Should_Return_Unauthorized_Result_Async(client);
                Console.WriteLine("Test1 succsed => " + task1 + "\n");

                bool task2 = await Should_Return_400_Result_For_Invalid_Auth_Parameter_Async(client);
                Console.WriteLine("Test2 succsed => " + task2 + "\n");

                bool task3 = await Should_Return_Correct_Base64_Result_Async(client);
                Console.WriteLine("Test3 succsed => " + task3 + "\n");

                bool task4 = await Should_Return_Correct_Result_Async(client);
                Console.WriteLine("Test4 succsed => " + task4 + "\n");

                bool task5 = await Should_Return_One_Unit_For_Query_Without_Parameters_Async(client);
                Console.WriteLine("Test5 succsed => " + task5 + "\n");

                bool task6 = await Should_Return_400_Result_For_Invalid_Query_Parameter_Async(client);
                Console.WriteLine("Test6 succsed => " + task6 + "\n");

                bool task7 = await Should_Return_400_Result_For_Invalid_Currency_Async(client);
                Console.WriteLine("Test7 succsed => " + task7 + "\n");


            }
            catch (IOException)
            {
                Console.WriteLine("File not found");
            }
            catch (JsonException)
            {
                Console.WriteLine("Can't deserialize object");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Can't set base adress of null");
            }
            catch (Exception)
            {
                Console.WriteLine("Somesing went wrong...");
            }

        }


        public static async Task<bool> Should_Return_Unauthorized_Result_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Unauthorized_Result_Async));

            string url = "Rates/usd/uah?amount=10";
            Console.WriteLine("GET: " + url);
            
            var response = await client.GetAsync(url);
            string rez = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response: " + rez);

            int expected = 401;
            Console.WriteLine("Expected result: " + expected);

            int actual = (int)response.StatusCode;
            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }


        public static async Task<bool> Should_Return_400_Result_For_Invalid_Auth_Parameter_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_400_Result_For_Invalid_Auth_Parameter_Async));

            string login = "123";
            string password = "123";
            
            var register = new Register()
            {
                Login = login,
                Password = password
            };

            string url = "register";
            Console.WriteLine("POST: /" + url);
            var content = new StringContent(JsonSerializer.Serialize<Register>(register), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            int expected = 400;
            Console.WriteLine($"Login => {login}, Password = > {password}");
            Console.WriteLine("Expected result: " + expected);

            int actual = (int)response.StatusCode;
            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }


        public static async Task<bool> Should_Return_Correct_Base64_Result_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Correct_Base64_Result_Async));

            string login = "string";
            string password = "string";
            string expected = "Basic c3RyaW5nOnN0cmluZw==";

            string url = "register";
            Console.WriteLine("POST: /" + url);
            Console.WriteLine($"Login => {login}, Password = > {password}");
            Console.WriteLine("Expected result: " + expected);

            var register = new Register()
            {
                Login = login,
                Password = password
            };

            var content = new StringContent(JsonSerializer.Serialize<Register>(register), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            string actual = await response.Content.ReadAsStringAsync();

            AuthHeader = actual;
            client.DefaultRequestHeaders.Add("Authorization", actual);

            Console.WriteLine("Actual result: " + actual);

            return expected.Equals(actual);
        }


        public static async Task<bool> Should_Return_Correct_Result_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Correct_Result_Async));

            bool expected = true;
            string url = "Rates/usd/uah?amount=10";
            Console.WriteLine("GET: /" + url);
            Console.WriteLine("Expected result: 10 usd => 250-300 uah: " + expected);
            var response = await client.GetAsync(url);
            
            string rez = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Result: " + rez);

            double rezDouble = double.Parse(rez);
            bool actual = rezDouble > 250 || rezDouble < 300;
            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

        public static async Task<bool> Should_Return_One_Unit_For_Query_Without_Parameters_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_One_Unit_For_Query_Without_Parameters_Async));

            string url1 = "Rates/usd/uah?amount=1";
            Console.WriteLine("GET: /" + url1);
            var expectedResponse = await client.GetAsync(url1);
            double expected = double.Parse(await expectedResponse.Content.ReadAsStringAsync());
            Console.WriteLine("Expected result: " + expected);

            string url2 = "Rates/usd/uah";
            Console.WriteLine("GET: /" + url2);
            var response = await client.GetAsync(url2);
            double actual = double.Parse(await response.Content.ReadAsStringAsync());

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

        public static async Task<bool> Should_Return_400_Result_For_Invalid_Query_Parameter_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_400_Result_For_Invalid_Query_Parameter_Async));

            int expected = 400;
            string url = "/Rates/usd/uah?amount=asdf";
            Console.WriteLine("GET: /" + url);

            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync(url);
            int actual = (int)response.StatusCode;

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

        public static async Task<bool> Should_Return_400_Result_For_Invalid_Currency_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_400_Result_For_Invalid_Currency_Async));

            int expected = 400;
            string url = "/Rates/usdf/uah?amount=10";
            Console.WriteLine("GET: /" + url);

            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync(url);
            int actual = (int)response.StatusCode;

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }


    }
}
