using System;

namespace Task_1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Show all prime numbers in range, made by Denys Sakadel");

            Console.WriteLine("Enter first number, from which start");
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter number, which is end a range");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine($"All prime numbers between {k} to {n}");

            bool isPrime;
            for (int i = k; i <= n; i++)
            {
                isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                    Console.Write(i + " ");
            }
        }
    }
}
