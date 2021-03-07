using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebAppTest;

namespace WebConverterTest
{
    class Program
    {
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

                bool task1 = await Should_Return_Correct_Result_Async(client);
                Console.WriteLine("Test1 succsed => " + task1 + "\n");

                bool task2 = await Should_Return_One_Unit_For_Query_Without_Parameters_Async(client);
                Console.WriteLine("Test2 succsed => " + task2 + "\n");

                bool task3 = await Should_Return_400_Result_For_Invalid_Query_Parameter_Async(client);
                Console.WriteLine("Test3 succsed => " + task3 + "\n");

                bool task4 = await Should_Return_400_Result_For_Invalid_Currency_Async(client);
                Console.WriteLine("Test4 succsed => " + task4 + "\n");

                bool task5 = await Should_Throw_Exception_Async(client);
                Console.WriteLine("Test5 succsed => " + task5 + "\n");

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


        public static async Task<bool> Should_Return_Correct_Result_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Correct_Result_Async));

            bool expected = true;
            Console.WriteLine("Expected result: 10 usd => 250-300 uah: " + expected);
            var response = await client.GetAsync("Rates/usd/uah?amount=10");
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

            var expectedResponse = await client.GetAsync("Rates/usd/uah?amount=1");
            double expected = double.Parse(await expectedResponse.Content.ReadAsStringAsync());
            Console.WriteLine("Expected result: " + expected);

            var response = await client.GetAsync("Rates/usd/uah");
            double actual = double.Parse(await response.Content.ReadAsStringAsync());

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

        public static async Task<bool> Should_Return_400_Result_For_Invalid_Query_Parameter_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_400_Result_For_Invalid_Query_Parameter_Async));

            int expected = 400;
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync("/Rates/usd/uah?amount=asdf");
            int actual = (int)response.StatusCode;

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

        public static async Task<bool> Should_Return_400_Result_For_Invalid_Currency_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_400_Result_For_Invalid_Currency_Async));

            int expected = 400;
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync("/Rates/usdf/uah?amount=10");
            int actual = (int)response.StatusCode;

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

        public static async Task<bool> Should_Throw_Exception_Async(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Throw_Exception_Async));

            string expected = "{\"code\":12,\"message\":\"The method or operation is not implemented.\"}";
            Console.WriteLine("Expected result: " + expected);
            var content = new StringContent("{\"login\": \"string\",\"password\": \"string\"}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/register", content);
            string actual = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Actual result: " + actual);

            return expected == actual;
        }

    }
}
