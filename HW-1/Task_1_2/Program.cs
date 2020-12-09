using System;

namespace Task_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculate chance of event and bookmacker's margin, made by Denys Sakadel");

            Console.WriteLine("Enter name of player 1:");
            String player1 = Console.ReadLine();
            Console.WriteLine("Enter name of player 2:");
            String player2 = Console.ReadLine();
            Console.WriteLine("Enter coefficient of player 1 (in my case need to enter number with coma):");
            double p1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter coefficient of player 2:");
            double p2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter coefficient of deuce:");
            double x = double.Parse(Console.ReadLine());

            int p1_with_margin = (int)(1 / p1 * 100);
            int p2_with_margin = (int)(1 / p2 * 100);
            int x_with_margin = (int)(1 / x * 100);

            int margin = p1_with_margin + p2_with_margin + x_with_margin - 100;
            int persent_p1 = p1_with_margin - (p1_with_margin * margin / 100);
            int persent_p2 = p2_with_margin - (p2_with_margin * margin / 100);
            int persent_x = x_with_margin - (x_with_margin * margin / 100);
           

            Console.WriteLine($"Win of {player1}: {persent_p1}%");
            Console.WriteLine($"Win of {player2}: {persent_p2}%");
            Console.WriteLine($"Deuce: {persent_x}%");
            Console.WriteLine($"Bookmacker's margin: {margin}%");

        }
    }
}
