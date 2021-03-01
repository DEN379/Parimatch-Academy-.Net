using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IPrimeFindService
    {
        IEnumerable<int> FindPrimeNumber(int from, int to);
        bool CheckPrimeNumber(int i);
    }
}
