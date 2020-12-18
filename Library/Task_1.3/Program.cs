using System;
using System.Collections.Generic;
using Task_1._1;
using Task_1._2;

namespace Task_1._3
{
    class Program
    {
        static void GetAccount(int id, Account[] accounts)
        {
            int[] rez = BinarySearch(accounts, id);
            if (rez[0] != -1)
                Console.WriteLine($"{id} was found at index {rez[0]} by {rez[1]} times");
            else Console.WriteLine($"There is no account {id} in the list");
        }
        static int[] BinarySearch(Account[] accounts, int key)
        {
            int[] rez = new int[2];
            int left = 0, right = accounts.Length, mid, k = 0;
            while (left <= right)
            {
                k++;
                mid = left + (right - left) / 2;
                if (key < accounts[mid].Id) right = mid - 1;
                else if (key > accounts[mid].Id) left = mid + 1;
                else 
                {
                    rez[0] = mid;
                    rez[1] = k;
                    return rez;
                }
            }
            rez[0] = -1;
            return rez;
        }
        static void Main(string[] args)
        {
            SortAccount sa = new SortAccount();
            sa.accounts = new Account[10000];
            for (int i = 0; i < sa.accounts.Length; i++)
            {
                sa.accounts[i] = new Account("UAH");
                //Console.WriteLine(sa.accounts[i].Id);
            }
            sa.accounts[2].Id = 41117866;
            sa.accounts = SortAccount.GetSortedAccounts(sa.accounts);
            sa.getFirstTenAccounts();

            GetAccount(41117866, sa.accounts);
        }
    }
}
