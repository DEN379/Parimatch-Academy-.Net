using System;
using Task_1._1;
using Task_1._2;

namespace Task_1._4
{
    class Program
    {
        static int partition(Account[] array, int start, int end)
        {
            Account temp;//swap helper
            int marker = start;//divides left and right subarrays
            for (int i = start; i <= end; i++)
            {
                if (array[i].Id < array[end].Id) //array[end] is pivot
                {
                    temp = array[marker]; // swap
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                }
            }
            //put pivot(array[end]) between left and right subarrays
            temp = array[marker];
            array[marker] = array[end];
            array[end] = temp;
            return marker;
        }

        static void quicksort(Account[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(array, start, end);
            quicksort(array, start, pivot - 1);
            quicksort(array, pivot + 1, end);
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
            quicksort(sa.accounts, 0, sa.accounts.Length -1);
            sa.getFirstTenAccounts();
        }
    }
}
