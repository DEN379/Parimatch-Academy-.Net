using System;
using System.Collections.Generic;
using Task_1._1;

namespace Task_1_5
{
    public class Player : Account
    {
        int Id;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;
        public Account Account;
        Random random = new Random();
        public Player(string FirstName, string LastName, string Email, string Password, string Currency): base(Currency)
        {
            Id = random.Next(100_000, 99_999_999);
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            Account = new Account(Currency);
        }
        public bool IsPasswordValid(string password) => password == Password;
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "USD");
            player.Deposit(2, "UAH");
            Console.WriteLine(  player.GetBalance("USD"));
            Console.WriteLine($"Login with {player.LastName} and password pass is {player.IsPasswordValid("pass")}");
            player.Deposit(100, "USD");
            player.Withdraw(50, "EUR");
            try
            {
                player.Withdraw(1000, "USD");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }

        }
    }
}
