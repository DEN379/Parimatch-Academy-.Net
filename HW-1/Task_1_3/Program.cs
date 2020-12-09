using System;

namespace Task_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculate series, made by Denys Sakadel");

            double i = 1;
            double rez = 0;
            Console.WriteLine("Series => 1/(i *(i + 1))");
            Console.WriteLine($"Where i = {i}");
            Console.WriteLine("Enter your birth year");
            double eps = 1 / (double.Parse(Console.ReadLine()));
            Console.WriteLine($"Epsilon => {eps}");
            double el = 0;

            do
            {
                el = 1 / (i * (i + 1));
                rez += el;
                i++;
            }
            while (el > eps); 

            Console.WriteLine($"Result of series => {rez}");
        }
    }
}
