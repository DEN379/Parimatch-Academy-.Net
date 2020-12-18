using System;

namespace Task_2._1
{
    public class BetService
    {
        decimal Odd;
        Random random = new Random();
        public BetService()
        {
            Odd = Convert.ToDecimal(random.NextDouble() * (25.0 - 1.01) + 1.01);
        }
        public decimal GetOdds()
        {
            Odd = Convert.ToDecimal(random.NextDouble() * (25.0 - 1.01) + 1.01);
            return Odd = Math.Round(Odd, 2);
        }
        public bool IsWon()
        {
            Random random = new Random();
            int persent = (int)(1 / Odd * 100);
            int win_chanse = 0;
            if (persent <= 50) win_chanse = (100 - persent) / persent;
            else win_chanse = persent / (100 - persent);
            int r = random.Next(0, win_chanse+1);
            if ((persent <= 50) && (r == win_chanse)) return true;
            else if ((persent > 50) && (r != win_chanse)) return true;
            else return false;
        }
        public decimal Bet(decimal amount)
        {
            return IsWon() ? amount*Odd : 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BetService betService = new BetService();
            Console.WriteLine( betService.IsWon() ); 
        }
    }
}
