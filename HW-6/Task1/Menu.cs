using System;

namespace Task1
{
    class Menu
    {
        public static void Run()
        {
            Console.WriteLine("Prime numbers, made by Denys Sakadel");

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Get prime numbers by Linq");
                Console.WriteLine("2. Get prime numbers by Plinq");
                Console.WriteLine("3. Exit");

                string command = Console.ReadLine().Trim();
                switch (command)
                {
                    case "1":
                        try
                        {
                            PrimeDiagnostic.ExecuteAsLinq();
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("You entered start number > end number, pls enter correct range");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You entered wrong number, pls try again enter correct number");
                        }
                        break;

                    case "2":
                        try
                        {
                            PrimeDiagnostic.ExecuteAsPlinq();
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("You entered start number > end number, pls enter correct range");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You entered wrong number, pls try again enter correct number");
                        }
                        break;

                    case "3": isRunning = false;
                        break;
                    default: Console.WriteLine("You enterd wrong command, must be 1, 2 or 3 as number");
                        break;
                }
            }
        }
    }
}
