using System;
using Task_3_1;

namespace Task_3._2
{
    public class PaymentService
    {
        protected PaymentMethodBase[] AvailablePaymentMethod;
        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[] { new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher() };
        }
        public void StartDeposit(decimal amount, string currency)
        {
            Console.WriteLine("1. CreditCard");
            Console.WriteLine("2. Privet48");
            Console.WriteLine("3. Stereobank");
            Console.WriteLine("4. GiftVoucher");
            string n = Console.ReadLine();

            CreditCard card;
            Privet48 privet;
            Stereobank stereo;
            GiftVoucher gift;
            switch (n)
            {
                case "1":
                    card = new CreditCard();
                    card.StartDeposit(amount, currency);
                    break;
                case "2":
                    privet = new Privet48();
                    privet.StartDeposit(amount, currency);
                    break;
                case "3":
                    stereo = new Stereobank();
                    stereo.StartDeposit(amount, currency);
                    break;
                case "4":
                    gift = new GiftVoucher();
                    gift.StartDeposit(amount, currency);
                    break;
                default:
                    Console.WriteLine("You entered wrong number, pls enter 1, 2, 3 or 4");
                    break;
            }
        }
        public void StartWithdraw(decimal amount, string currency)
        {
            Console.WriteLine("1. CreditCard");
            Console.WriteLine("2. Privet48");
            Console.WriteLine("3. Stereobank");
            string n = Console.ReadLine();

            CreditCard card;
            Privet48 privet;
            Stereobank stereo;
            switch (n)
            {
                case "1":
                    card = new CreditCard();
                    card.StartWithdrawal(amount, currency);
                    break;
                case "2":
                    privet = new Privet48();
                    privet.StartWithdrawal(amount, currency);
                    break;
                case "3":
                    stereo = new Stereobank();
                    stereo.StartWithdrawal(amount, currency);
                    break;
                default:
                    Console.WriteLine("You entered wrong number, pls enter 1, 2 or 3");
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService ps = new PaymentService();
            ps.StartDeposit(101,"USD");
        }
    }
}
