using System;
using System.Diagnostics;

namespace Task1
{
    class PrimeDiagnostic
    {
        public static void ExecuteAsLinq()
        {
            int[] range = GetRange();
            Stopwatch watchLinq = new Stopwatch();
            watchLinq.Start();
            int countLinq = PrimeNumbers.GetCountPrimesByLinq(range[0], range[1]);
            watchLinq.Stop();
            Console.WriteLine($"Count of prime numbers by Linq => {countLinq}, " +
                $"in range [{range[0]} ; {range[1]}] elapsed time => {watchLinq.Elapsed}");
        }

        public static void ExecuteAsPlinq()
        {
            int[] range = GetRange();
            Stopwatch watchPlinq = new Stopwatch();
            watchPlinq.Start();
            int countPlinq = PrimeNumbers.GetCountPrimesByPlinq(range[0], range[1]);
            watchPlinq.Stop();
            Console.WriteLine($"Count of prime numbers by Plinq => {countPlinq}, " +
                $"in range ({range[0]} - {range[1]}) elapsed time => {watchPlinq.Elapsed}");
        }

        public static int[] GetRange()
        {
            Console.WriteLine("Enter range of numbers:");
            Console.WriteLine("Start =>");
            int start = int.Parse(Console.ReadLine());
            Console.WriteLine("End =>");
            int end = int.Parse(Console.ReadLine());
            return new int[] { start, end };
        }

    }
}
