using System;
using Task_1._1;

namespace Task_1._2
{
    public class SortAccount
    {
        static int length = 0;
        
        public Account[] accounts = new Account[length];
        public static Account[] GetSortedAccounts(Account[] accounts)
        {
            Account temp;
            for (int i = 0; i < accounts.Length - 1; i++)
            {
                for (int j = 0; j < accounts.Length - i - 1; j++)
                {
                    if (accounts[j + 1].Id < accounts[j].Id)
                    {
                        temp = accounts[j + 1];
                        accounts[j + 1] = accounts[j];
                        accounts[j] = temp;
                    }
                }
            }
            return accounts;
        }
        public void getFirstTenAccounts()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(accounts[i].Id);
            }
        }
        public void getLastTenAccounts()
        {
            for (int i = accounts.Length - 10; i < accounts.Length; i++)
            {
                Console.WriteLine(accounts[i].Id);
            }
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            SortAccount sa = new SortAccount();
            sa.accounts = new Account[100];
            for(int i=0; i< sa.accounts.Length; i++)
            {
                sa.accounts[i] = new Account("UAH");
                //Console.WriteLine(sa.accounts[i].Id);
            }


        }
    }
}
