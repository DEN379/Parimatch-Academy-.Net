using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IPrimeFindService
    {
        Task<IEnumerable<int>> FindPrimeNumber(int from, int to);
        Task<bool> CheckPrimeNumber(int i);
    }
}
