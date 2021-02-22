using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppTest
{
    public class Program
    {
       public static async Task Main(string[] args)
        {
            try
            {
                var json = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<Settings>(json);
                var client = new HttpClient();
                client.BaseAddress = new Uri(settings.BaseAddress);

                bool task1 = await Should_Return_Program_DescriptionAsync(client);
                Console.WriteLine("Test1 succsed => " + task1 + "\n");

                bool task2 = await Should_Return_Prime_Number(client);
                Console.WriteLine("Test2 succsed => " + task2 + "\n");

                bool task3 = await Should_Return_Not_Prime_Number(client);
                Console.WriteLine("Test3 succsed => " + task3 + "\n");

                bool task4 = await Should_Return_Sequence_Prime_Numbers(client);
                Console.WriteLine("Test4 succsed => " + task4 + "\n");

                bool task5 = await Should_Return_Empty_Sequence_Prime_Numbers(client);
                Console.WriteLine("Test5 succsed => " + task5 + "\n");

                bool task6 = await Should_Return_BadRequest(client);
                Console.WriteLine("Test6 succsed => " + task6 + "\n");

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


        public static async Task<bool> Should_Return_Program_DescriptionAsync(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Program_DescriptionAsync));

            string expected = "Web prime numbers, made by Denys Sakadel";
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync("/");
            string actual = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Actual result: " + actual);

            return actual.Equals(expected);
        }

        public static async Task<bool> Should_Return_Prime_Number(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Prime_Number));

            int testNumber = 7;
            string expected = $"200 code. " +
                            $"Status: OK, a number => {testNumber} is prime";
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync("/primes/"+testNumber);
            string actual = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Actual result: " + actual);

            return actual.Equals(expected);
        }

        public static async Task<bool> Should_Return_Not_Prime_Number(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Not_Prime_Number));

            int testNumber = 9;
            int expected = 404;

            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync("/primes/" + testNumber);
            int actual = (int)response.StatusCode;

            Console.WriteLine("Actual result: " + actual);

            return actual == expected;
        }

        public static async Task<bool> Should_Return_Sequence_Prime_Numbers(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Sequence_Prime_Numbers));

            int from = 2;
            int to = 8;
            string expected = "Primes: 2 3 5 7";
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync($"/primes?from={from}&to={to}");
            string actual = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Actual result: " + actual);

            return actual.Equals(expected);
        }

        public static async Task<bool> Should_Return_Empty_Sequence_Prime_Numbers(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_Empty_Sequence_Prime_Numbers));

            int from = -12;
            int to = -1;
            string expected = "Primes: ";
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync($"/primes?from={from}&to={to}");
            string actual = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Actual result: " + actual);

            return actual.Equals(expected);
        }

        public static async Task<bool> Should_Return_BadRequest(HttpClient client)
        {
            Console.WriteLine("Task => " + nameof(Should_Return_BadRequest));

            int expected = 400;
            Console.WriteLine("Expected result: " + expected);
            var response = await client.GetAsync($"/primes?to=sfdsg");
            int actual = (int) response.StatusCode;

            Console.WriteLine("Actual result: " + actual);

            return actual == expected;
        }
    }
}
