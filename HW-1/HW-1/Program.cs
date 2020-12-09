using System;

namespace HW_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double y;
            const int b = 1999, c = 12, d = 9;

            Console.WriteLine("Calculate value Y, made by Denys Sakadel");
            Console.WriteLine("(e^a) + 4 * Log10(c) / Sqrt(b) * |Atan(d)| + (5 / Sin(a))");
            Console.WriteLine("b => 1999, c => 12, d => 9");
            Console.WriteLine("Please, enter value A:");
            int a = int.Parse(Console.ReadLine());

            y = ((Math.Pow(Math.E, a) + 4 * Math.Log10(c)) / (Math.Sqrt(b)) * (Math.Abs(Math.Atan(d))) + (5 / Math.Sin(a)));

            Console.WriteLine($"Result of operations =>  y = {y}");
            

        }
    }
}
