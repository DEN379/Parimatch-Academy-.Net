using System;

namespace Task_1._1
{
    public class Account
    {
        public static decimal convertCashe(decimal amount, string fromCurrency, string toCurrency)
        {
            if(toCurrency == "UAH")
            switch (fromCurrency)
            {
                case "UAH": return amount;
                case "USD": return amount * (decimal)28.36;
                case "EUR": return amount * (decimal)33.63;
                default: return 0;
            }

            if (toCurrency == "USD")
                switch (fromCurrency)
                {
                    case "UAH": return amount / (decimal)28.36;
                    case "USD": return amount;
                    case "EUR": return amount * (decimal)1.19;
                    default: return 0;
                }
            if (toCurrency == "EUR")
                switch (fromCurrency)
                {
                    case "UAH": return amount / (decimal)33.63;
                    case "USD": return amount / (decimal)1.19;
                    case "EUR": return amount;
                    default: return 0;
                }
            return 0;
        }
        Random random = new Random();
        public int Id;
        private string Currency = "UAH";
        private decimal Amount = 0;
        public Account(string currency)
        {
            Id = random.Next(100_000, 99_999_999);
            if ((currency != "USD") && (currency != "EUR") && (currency != "UAH")) throw new NotSupportedException();
            Currency = currency;
        }

        public virtual void Deposit(decimal amount, string Currency)
        {
            if (Currency == this.Currency) this.Amount += amount;
            else this.Amount += convertCashe(amount, Currency, this.Currency);
        }
        public virtual void Withdraw(decimal amount, string Currency)
        {
            decimal convert = 0;
            if (Currency.Equals(this.Currency))
            {
                if ((this.Amount - amount) > 0)
                    this.Amount -= amount;
                else throw new InvalidOperationException();
            }
            else
            {
                convert = convertCashe(amount, Currency, this.Currency);
                if (convert > 0) this.Amount -= convert;
                else throw new InvalidOperationException();
            }
        }
        public decimal GetBalance(string currency)
        {
            return convertCashe(this.Amount,this.Currency, currency);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Account ac1 = new Account("UAH");
            Account ac2 = new Account("EUR");
            Account ac3 = new Account("USD");
            
            ac2.Deposit(10, "EUR");
            ac2.Withdraw(3,"UAH");

            ac1.Deposit(121, "USD");

            //ac3.Withdraw(5, "USD");

            Console.WriteLine(ac2.GetBalance("EUR"));
            Console.WriteLine(ac1.GetBalance("UAH"));
        }
    }
}
