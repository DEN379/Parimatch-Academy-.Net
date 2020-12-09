using System;

namespace Task_2_4
{
    class Program
    {
        public static void getPoints(int f, int l, int k)
        {
            double st = 0;
            int i = 0;
            while(st < l)
            {
                i++;
                st = Math.Pow(2, i);
            }
            double points = 100 * ((i - 1) - (k - 1)) / (i - 1);
            Console.WriteLine($"Your points => {points}");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Game more-less, made by Denys Sakadel");

            Console.WriteLine("Rules: you must guess a number which is given by computer");
            Console.WriteLine("Computer will say more or less to guess easier");
            Console.WriteLine("You will get some points for guessing a number depending how much times you guessed");
            M: Console.WriteLine("Enter range of numbers => ");
            Console.WriteLine("First number =>");
            int f = 0, l =0;
            try
            {
                f = int.Parse(Console.ReadLine());
                Console.WriteLine("Last number =>");
                l = int.Parse(Console.ReadLine());
            } catch (Exception)
            {
                Console.WriteLine("You enter wrong number, try again!");
                goto M;
            }
            
            

            Random random = new Random();
            int numb = random.Next(f, l);
            String word = "";
            int guess = 0;
            int k = 0;
            do
            {
                k++;
                L: Console.WriteLine("Guess the number => ");
                word = Console.ReadLine();
                if (word == "exit") break;
                try
                {
                    guess = int.Parse(word);
                }
                catch (Exception)
                {
                    Console.WriteLine("You enter wrong number, try again!");
                    goto L;
                }
                if (guess > numb) Console.WriteLine("Less");
                if (guess < numb) Console.WriteLine("More");
            }
            while (numb != guess);
            if (numb == guess)
            {
                Console.WriteLine("You guessed!!!");
                Console.WriteLine($"You've guessed {k} times");
                getPoints(f, l, k);
            }
            else
            {
                Console.WriteLine("You conciede :(");
                Console.WriteLine("You don't get any points");
            }
        }
    }
}
