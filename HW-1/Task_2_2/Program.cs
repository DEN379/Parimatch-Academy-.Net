using System;

namespace Task_2_2
{
    class Program
    {
        public static double square_square(double a) => a * a;
        public static double rectangle_square(double a, double b) => a * b;
        public static double triangle_square(double a, double h) => 0.5 * a * h;
        public static double circle_square(double r) => Math.PI * r * r;
        public static void rez_switch(String switcher, double a, double b)
        {
            switch (switcher)
            {
                case "square":
                    Console.WriteLine(square_square(a));
                    break;
                case "rectangle":
                    Console.WriteLine(rectangle_square(a, b));
                    break;
                case "triangle":
                    Console.WriteLine(triangle_square(a, b));
                    break;
                case "circle":
                    Console.WriteLine(circle_square(a));
                    break;
                default:
                    Console.WriteLine(-1);
                    break;
            }
        }
        static void Main(string[] args)
        {
            double x = 0;
            double y = 0;
            String s = "";
            if(args.Length != 0)
            {
                try { 
                    s = args[0];
                    x = double.Parse(args[1]);
                    y = args.Length > 2 ? double.Parse(args[2]) : 0;
                    rez_switch(s, x, y);
                } catch (Exception e)
                {
                    Console.WriteLine(-1);
                }
            }
            else
            {
                Console.WriteLine("Calculator of square figures, made by Denys Sakadel");
                Console.WriteLine("Enter one of the figures: square, rectangle, triangle, circle");
                Console.WriteLine("Then you must enter 2 number if it's rectangle(2 sides) or triangle(side and height), else one number: for circle - radius, for square - 1 side");
                Console.WriteLine("Number must be in right format (in my case must be with coma)");
                Console.WriteLine("You can exit from calculator by enter exit");

                while (s != "exit")
                {
                    Console.WriteLine("Enter figure => ");
                    s = Console.ReadLine();
                    try
                    {
                        switch (s)
                        {
                            case "square":
                                Console.WriteLine("Enter 1 side of square => ");
                                x = double.Parse(Console.ReadLine());
                                if (x < 0 && x > double.MaxValue)
                                {
                                    Console.WriteLine($"You enter wrong number, you must enter number > 0 and < than max value of double({double.MaxValue})");
                                    break;
                                }
                                Console.WriteLine(square_square(x));
                                break;
                            case "rectangle":
                                Console.WriteLine("Enter first side of rectangle => ");
                                x = double.Parse(Console.ReadLine());
                                Console.WriteLine("Enter second side of rectangle => ");
                                y = double.Parse(Console.ReadLine());
                                if (x < 0 && x > double.MaxValue && y < 0 && y > double.MaxValue)
                                {
                                    Console.WriteLine($"You enter wrong number, you must enter number > 0 and < than max value of double({double.MaxValue})");
                                    break;
                                }
                                Console.WriteLine(rectangle_square(x, y));
                                break;
                            case "triangle":
                                Console.WriteLine("Enter first side of triangle => ");
                                x = double.Parse(Console.ReadLine());
                                Console.WriteLine("Enter second side of triangle => ");
                                y = Convert.ToDouble(Console.ReadLine());
                                if (x < 0 && x > double.MaxValue && y < 0 && y > double.MaxValue)
                                {
                                    Console.WriteLine($"You enter wrong number, you must enter number > 0 and < than max value of double({double.MaxValue})");
                                    break;
                                }
                                Console.WriteLine(triangle_square(x, y));
                                break;
                            case "circle":
                                Console.WriteLine("Enter radius of circle => ");
                                x = double.Parse(Console.ReadLine());
                                if (x < 0 && x > double.MaxValue)
                                {
                                    Console.WriteLine($"You enter wrong number, you must enter number > 0 and < than max value of double({double.MaxValue})");
                                    break;
                                }
                                Console.WriteLine(circle_square(x));
                                break;
                            case "exit": Console.WriteLine("Good bye!");
                                break;
                            default:
                                Console.WriteLine("You enter wrong figure, please enter one of this figure: square, rectangle, triangle, circle");
                                break;
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"You enter wrong number, you must enter number > 0 and < than max value of double({double.MaxValue}), {e}");
                    }
                }
            }

        }
    }
}
