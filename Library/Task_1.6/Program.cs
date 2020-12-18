using System;
using System.Collections.Generic;
using Task_1._1;
using Task_1_5;
using Task_2._1;
using Task_3._2;
using Task_3_1;

namespace BettingPlatformEmulatorTest
{
    public class BettingPlatformEmulator
    {
        List<Player> Players = new List<Player>();
        PaymentService paymentService;
        Player ActivePlayer;
        Account Account;
        BetService betService;
        bool isLogin = false;
        public BettingPlatformEmulator()
        {
            Account = new Account("USD");
            betService = new BetService();
            paymentService = new PaymentService();
        }

        public void Start()
        {
            displayNonRegister();
            string r_command = "";
            string l_command = "";
            while (!r_command.Equals("3"))
            {
                r_command = Console.ReadLine();
                switch (r_command)
                {
                    case "1": Register();
                        break;
                    case "2":
                        Login();
                        if (!isLogin)
                        {
                            displayNonRegister();
                            break;
                        }
                        while (l_command != "5")
                        {
                            l_command = Console.ReadLine();
                            switch (l_command)
                            {
                                case "1":
                                    Deposit();
                                    displayRegister();
                                    break;
                                case "2":
                                    Withdraw();
                                    displayRegister();
                                    break;
                                case "3":
                                    Console.WriteLine("Coeficient => " + betService.GetOdds() ); 
                                    displayRegister();
                                    break;
                                case "4":
                                    Bet();
                                    displayRegister();
                                    break;
                                case "5":
                                    Logout();
                                    displayNonRegister();
                                    break;
                                default:
                                    Console.WriteLine("You entered wrong command, try 1, 2 or 3");
                                    break;
                            }
                        }
                        break;
                    case "3":
                        Exit();
                        break;
                    default:
                        Console.WriteLine("You entered wrong command, try 1, 2 or 3");
                        break;
                }
            }


        }
        public void Exit() { }
        public void Register()
        {
            Console.WriteLine("Enter your name, please");
            string fname = Console.ReadLine();
            Console.WriteLine("Enter your Last name, please");
            string lname = Console.ReadLine();
            Console.WriteLine("Enter your email, please");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password, please");
            string pass = Console.ReadLine();
            Console.WriteLine("Enter your currency, please");
            string curr = Console.ReadLine();
            try
            {
                Player player = new Player(fname, lname, email, pass, curr);
                Players.Add(player);
                displayNonRegister();
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("You entered wrong currency, you can only use this currency: UAH, USD, EUR");
            }

        }
        public void Login()
        {
            Console.WriteLine("Enter your email, please");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password, please");
            string pass = Console.ReadLine();

            foreach (Player p in Players)
            {
                if ((email == p.Email) && (pass == p.Password))
                {
                    Console.WriteLine("You successfuly logged in");
                    ActivePlayer = p;
                    CreditCard.active = ActivePlayer;
                    displayRegister();
                    isLogin = true;
                    break;
                }
            }

            if (!isLogin)
            {
                Console.WriteLine("Your login or password is incorect, try again");
            }
        }
        public void Logout()
        {
            ActivePlayer = null;
        }
        public void Deposit()
        {
            Console.WriteLine("Enter your amount of money, please");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter your currency, please");
            string Currency = Console.ReadLine();
            try
            {
                paymentService.StartDeposit(amount, Currency);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message == "CVV") Console.WriteLine("CVV error");
                if (e.Message == "Card number") Console.WriteLine("Card number error");
                if (e.Message == "Expire date") Console.WriteLine("Expire date error");
            }

            //Account.Deposit(amount, Currency);
            //ActivePlayer.Deposit(amount, Currency);
        }
        public void Withdraw()
        {
            Console.WriteLine("Enter your amount of money, please");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter your currency, please");
            string Currency = Console.ReadLine();
            
            try 
            {
                paymentService.StartWithdraw(amount, Currency);
                //ActivePlayer.Withdraw(amount, Currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account");
            }
            //try
            //{
            //    Account.Withdraw(amount, Currency);
            //}
            //catch (InvalidOperationException)
            //{
            //    Console.WriteLine("There is some problem on the platform side. Please try it later");
            //}
        }
        public void Bet()
        {
            Console.WriteLine("Enter amount of money");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter currency");
            string currency = Console.ReadLine();
            decimal win_cashe = -1;
            try
            {
                ActivePlayer.Withdraw(amount, currency);
                win_cashe = betService.Bet(amount);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account");
            }
            if (win_cashe > 0)
            {
                Console.WriteLine($"You won {win_cashe} {currency}");
                ActivePlayer.Deposit(win_cashe, currency);
            } else if(win_cashe == 0)
            {
                Console.WriteLine("You lost.");
            }
            
        }
        public void displayNonRegister()
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Stop");
        }
        public void displayRegister()
        {
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Get Odds");
            Console.WriteLine("4. Bet");
            Console.WriteLine("5. Logout");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BettingPlatformEmulator bettingPlatformEmulator = new BettingPlatformEmulator();
            bettingPlatformEmulator.Start();
            
        }
    }
}
