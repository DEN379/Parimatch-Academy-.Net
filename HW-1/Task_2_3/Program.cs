using System;

namespace Task_2_3
{
    class Program
    {
        public static int find_min(int[] mas)
        {
            int min = mas[0];
            for (int i = 0; i < mas.Length; i++)
            {
                if (i + 1 < mas.Length && mas[i + 1] < min)
                {
                    min = mas[i + 1];
                }
            }
            return min;
        }
        public static int find_max(int[] mas)
        {
            int max = mas[0];

            for (int i = 0; i < mas.Length; i++)
            {
                if (i + 1 < mas.Length && mas[i + 1] > max)
                {
                    max = mas[i + 1];
                }
            }
            return max;
        }
        public static double find_s_otkl(int[] mas, double sa)
        {
            int otk = 0;
            int rez = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                otk = mas[i] - (int)sa;
                rez += otk * otk;
            }
            return  Math.Sqrt(rez / mas.Length);
        }
        public static void display_array(int[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                Console.Write(mas[i] + " ");
            }
        }


        static void Main(string[] args)
        {
            
            int sum = 0;
            double sa = 0;
            if (args.Length != 0)
            {
                try
                {
                    int[] mas = new int[args.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        mas[i] = int.Parse(args[i]);
                        sum += mas[i];
                    }
                    sa = sum / mas.Length;

                    Console.WriteLine(find_min(mas));
                    Console.WriteLine(find_max(mas));
                    Console.WriteLine(sum);
                    Console.WriteLine(sa);
                    Console.WriteLine(find_s_otkl(mas, sa));
                    Array.Sort(mas);
                    display_array(mas);
                }
                catch (Exception)
                {
                    Console.WriteLine(-1);
                }
                
            }
            else
            {
                Console.WriteLine("Get stats for array, made by Denys Sakadel");
                L: Console.WriteLine("Enter length of array =>");
                int n = 0;
                try
                {
                    n = int.Parse(Console.ReadLine());
                } catch (Exception)
                {
                    goto L;
                }

                int[] mas = new int[n];
                M: Console.WriteLine("Enter elements of array by one =>");
                for(int i = 0; i < n; i++)
                {
                    try
                    {
                        mas[i] = int.Parse(Console.ReadLine());
                    } catch(Exception)
                    {
                        goto M;
                    }
                    sum += mas[i];
                }
                sa = sum / mas.Length;

                Console.WriteLine("Min element => " + find_min(mas));
                Console.WriteLine("Max element => " + find_max(mas));
                Console.WriteLine("Sum of elements => " + sum);
                Console.WriteLine("Arithmetic mean => " + sa);
                Console.WriteLine("Standard deviation => " + find_s_otkl(mas, sa));
                Array.Sort(mas);
                Console.WriteLine("Display sorted array => ");
                display_array(mas);

            }
        }
    }
}
