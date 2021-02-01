using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    class PrimeNumbers
    {
        public static List<int> GetPrimeNumbers(Settings settings)
        {
            int start = settings.PrimesFrom;
            int end = settings.PrimesTo;
            if(start > end)
            {
                start = 1;
                end = 1;
            }
            var numbers = Enumerable.Range(start, end - start);
            return numbers.Where(n => n >= 2 && Enumerable.Range(2, n - 2).All(div => n % div != 0)).ToList();
        }
    }
}
