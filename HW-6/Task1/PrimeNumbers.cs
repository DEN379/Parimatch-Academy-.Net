using System;
using System.Linq;

namespace Task1
{
    class PrimeNumbers
    {
        public static int GetCountPrimesByLinq(int start, int end)
        {
            if (start > end) throw new ArgumentException();
            if (start < 2) start = 2;
            int countOfPrimesByLinq = Enumerable.Range(start, end - start + 1)
                    .Count(number => Enumerable.Range(2, number - 2).All(div => number % div != 0));
            return countOfPrimesByLinq;
        }

        public static int GetCountPrimesByPlinq(int start, int end)
        {
            if (start > end) throw new ArgumentException();
            if (start < 2) start = 2;
            int countOfPrimesByPlinq = Enumerable.Range(start, end - start + 1).AsParallel()
                    .Count(number => Enumerable.Range(2, number - 2).All(div => number % div != 0));
            return countOfPrimesByPlinq;
        }

    }
}
