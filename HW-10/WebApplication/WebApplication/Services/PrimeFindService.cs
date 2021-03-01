using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class PrimeFindService : IPrimeFindService
    {
        public IEnumerable<int> FindPrimeNumber(int from, int to)
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = from; i <= to; i++)
            {
                if(CheckPrimeNumber(i))
                {
                    list.AddLast(i);
                }

            }
            return list;
        }

        public bool CheckPrimeNumber(int i)
        {
            if (i < 2)
            {
                return false;
            }
            if (i == 2) return true;
            
            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
