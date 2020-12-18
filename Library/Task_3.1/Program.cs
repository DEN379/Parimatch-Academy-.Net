using System;
using Task_1._1;
using Task_1._2;
using Task_1_5;

namespace Task_3_1
{
    public interface ISupportDeposit
    {
        void StartDeposit(decimal amount, string currency);
    }
    public interface ISupportWithdrawal
    {
        void StartWithdrawal(decimal amount, string currency);
    }
    public abstract class PaymentMethodBase
    {
        protected string name;

    }
    public class CreditCard : PaymentMethodBase, ISupportWithdrawal, ISupportDeposit
    {
        public string cardName;
        protected Account account = new Account("UAH");
        public static Player active;

        public CreditCard(string currency)
        {
            name = "Credit card";
            account = new Account(currency);
        }
        public CreditCard(string cardName,string currency)
        {
            this.cardName = cardName;

            account = new Account(currency);
        }
        public CreditCard()
        {
            name = "Credit card";
            account = new Account("UAH");
        }
        public void StartDeposit(decimal amount, string currency)
        {
            Console.WriteLine("Enter your card number (16 digits, Mastercard begins with 5, Visa - 4");
            string cardNumber = Console.ReadLine();
            Console.WriteLine(cardNumber[0]);
            //if ( (cardNumber[0].Equals('5')) ) throw new InvalidOperationException("Card number"); //(cardNumber.Length != 16) || || (cardNumber[0] != '4')
            Console.WriteLine("Enter your expiry date (format 01/22)");
            string expireDate = Console.ReadLine();
            //if ((expireDate.Length != 5) || (cardNumber[3] != '/')) throw new InvalidOperationException("Expire date");
            Console.WriteLine("Enter your CVV (3 digits on a back)");
            string CVV = Console.ReadLine();
            //if (CVV.Length != 3) throw new InvalidOperationException("CVV");
            active.Deposit(amount, currency);
            account.Deposit(amount, currency);
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            Console.WriteLine("Enter your card number (16 digits, Mastercard begins with 5, Visa - 4");
            int cardNumber = Convert.ToInt32(Console.ReadLine());
            try
            {
                active.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account");
            }
            try
            {
                account.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is some problem on the platform side. Please try it later");
            }


        }
    }

    public abstract class Bank : PaymentMethodBase, ISupportWithdrawal, ISupportDeposit
    {
        protected CreditCard[] AvalibleCards;
        protected Account account;
        public virtual void StartDeposit(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {name}");
            Console.WriteLine("Please, enter your login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password");
            string pass = Console.ReadLine();
            Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction");
            for(int i = 0; i < AvalibleCards.Length; i++)
            {
                Console.WriteLine($"{i}. {AvalibleCards[i].cardName}");
            }
            int n = Convert.ToInt32(Console.ReadLine());
            AvalibleCards[n].StartDeposit(amount, currency);

            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {AvalibleCards[n].cardName} card successfully");
        }

        public virtual void StartWithdrawal(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {name}");
            Console.WriteLine("Please, enter your login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password");
            string pass = Console.ReadLine();
            Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction");
            for (int i = 0; i < AvalibleCards.Length; i++)
            {
                Console.WriteLine($"{i}. {AvalibleCards[i].cardName}");
            }
            int n = Convert.ToInt32(Console.ReadLine());
            AvalibleCards[n].StartWithdrawal(amount, currency);
            Console.WriteLine($"You’ve diposite {amount} {currency} to your {AvalibleCards[n].cardName} card successfully");
        }
    }
    public class Privet48: Bank
    {
        
        public Privet48()
        {
            name = "Privet48";
            AvalibleCards = new CreditCard[] { new CreditCard("Gold","UAH"), new CreditCard("Platinum", "USD") };
        }
        public override void StartDeposit(decimal amount, string currency)
        {
            base.StartDeposit(amount, currency);
        }
        public override void StartWithdrawal(decimal amount, string currency)
        {
            base.StartWithdrawal(amount, currency);
        }

    }
    public class Stereobank : Bank
    {
        public Stereobank()
        {
            name = "Stereobank";
            AvalibleCards = new CreditCard[] { new CreditCard("Black", "UAH"), new CreditCard("White", "USD") , new CreditCard("Iron", "EUR") };
        }

    }
    public class GiftVoucher : PaymentMethodBase, ISupportDeposit
    {
        public GiftVoucher()
        {
            name = "GiftVoucher";
            
        }

        public void StartDeposit(decimal amount, string currency)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
