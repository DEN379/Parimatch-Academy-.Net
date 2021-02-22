using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class PrimeFindService : IPrimeFindService
    {
        public async Task<IEnumerable<int>> FindPrimeNumber(int from, int to)
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = from; i <= to; i++)
            {
                if(await CheckPrimeNumber(i))
                {
                    list.AddLast(i);
                }

            }
            return list;
        }

        public Task<bool> CheckPrimeNumber(int i)
        {
            if (i < 2)
            {
                return Task.FromResult(false);
            }
            if (i == 2) return Task.FromResult(true);
            //bool isPrimeNumber = false;
            
            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(true);
        }
    }

}
