using System;
using System.Text.RegularExpressions;

namespace Task_3_1
{
    class Program
    {
        public static int sum(int x, int y) => x + y;
        public static int sub(int x, int y) => x - y;
        public static int multi(int x, int y) => x * y;
        public static double div(int x, int y)
        {
            double rez = -1;
            try
            {
                rez = x / y;
            }catch (DivideByZeroException)
            {
                if(flag)
                Console.WriteLine("You can't divide by 0!");
            }
            return rez;
        }
        public static double mod(int x, int y) => x % y;
        public static double pow(int x, int y) => Math.Pow(x,y);
        public static double l_and(int x, int y)
        {
            if (x >= 0 && y >= 0) return x & y;
            else
            {
                if(flag)Console.WriteLine("Numbers must be more than 0");
                return -1;
            }
        }
        public static double l_or(int x, int y)
        {
            if (x >= 0 && y >= 0) return x | y;
            else
            {
                if (flag) Console.WriteLine("Numbers must be more than 0");
                return -1;
            }
        }
        public static double l_xor(int x, int y)
        {
            if (x >= 0 && y >= 0) return x ^ y;
            else
            {
                if (flag) Console.WriteLine("Numbers must be more than 0");
                return -1;
            }
        }
        public static double l_not(int x)
        {
            if (x >= 0) return ~x;
            else
            {
                if (flag) Console.WriteLine("Number must be more than 0");
                return -1;
            }
        }
        public static int factorial(int x)
        {
            int fact = 1;
            for (int i = 1; i <= x; i++) fact *= i;
            return fact;
        }

        private static double switcher(String sign, int x, int y)
        {
            switch (sign)
            {
                case "+":
                    return sum(x, y);
                case "-":
                    return sub(x, y);
                case "x":
                case "*":
                    return multi(x, y);
                case "\\":
                case "/":
                    return div(x, y);
                case "%":
                    return mod(x, y);
                case "pow":
                    return pow(x, y);
                case "&":
                    return l_and(x, y);
                case "|":
                    return l_or(x, y);
                case "^":
                    return l_xor(x, y);
                case "!x":
                    return l_not(x);
                case "!":
                    return factorial(x);
                case "":
                    return x;
                default:
                    if (flag)
                    {
                        Console.WriteLine("No such operation, you may enter help for display rules");
                    }
                    return -1;
            }
        }
        private static int[] parseNumb(string s, string patternNumber)
        {
            MatchCollection matches = Regex.Matches(s, patternNumber);
            int k = matches.Count;
            int[] numbers = new int[k];
            int j = 0;
            foreach (Match match in matches)
            {
                try
                {
                    numbers[j] = checked(Convert.ToInt32(match.Value));
                } catch (OverflowException)
                {
                    Console.WriteLine("Overflow exception, please enter numbers in range");
                }
                catch (Exception)
                {
                    Console.WriteLine("Some unknown error");
                }

                j++;
            }
            return numbers;
        }
        private static string parseOperand(string s, string patternSign)
        {
            Regex r = new Regex(patternSign, RegexOptions.IgnoreCase);
            Match matches2 = r.Match(s);
            return Convert.ToString(matches2.Groups[1]);
        }

        static bool flag = true;
        static void Main(string[] args)
        {
            string s = "";
            string read = "";
            string rules = "";
            
            if (args.Length != 0)
            {
                string[] mas = new string[args.Length];
                for(int i = 0; i<args.Length; i++)
                {
                    mas[i] = args[i];
                }
                s = string.Concat(mas);
            }
            else
            {
                rules = "Rules: you can use this commands: + - * x / \\ pow & | ^ !";
                Console.WriteLine("Binary and unary calculator by Denys Sakadel");
                Console.WriteLine(rules);
                Console.WriteLine("You can use commands: exit - exit from program, help - display rules");
                Console.WriteLine("Permit: 1 or 2 operands, number below 0, you can write with space and without");
                Console.WriteLine("Prohibited: 3 operands or more, extra signs");
            }

            M: while( (read != "exit" || read != "help") && flag)
            {
                if (args.Length > 0) flag = false;
                if (args.Length == 0)
                {
                    Console.WriteLine("Enter your function =>");
                    try
                    {
                        s = Console.ReadLine();
                    } catch (Exception)
                    {
                        Console.WriteLine("You enter wrong expression, please try again");
                        Console.WriteLine(rules);
                        goto M;
                    }
                }
                string patternNumber = @"-?\d+";
                string patternSign = @"\d\s*(\W|pow|x|х)";

                string sign = parseOperand(s, patternSign);
                int[] numbers = parseNumb(s, patternNumber);
                
                int x = 0;
                int y = 0;
                try
                {
                    if (numbers.Length == 1)
                    {
                        x = numbers[0];
                        s = s.Replace(" ", "");
                        if (s.StartsWith("!")) sign = "!x";
                    }
                    else
                    {
                        x = numbers[0];
                        y = numbers[1];
                        if (y < 0 && sign == "-")
                        {
                            s = s.Replace(" ", "");
                            sign = s.Contains("--") ? "-" : "+";
                        }
                    }
                    if(numbers.Length != 0) Console.WriteLine(switcher(sign, x, y));
                } catch (Exception)
                {
                    if (flag)
                    {
                        Console.WriteLine("You enter wrong expression, please try again, you may use command \"help\" to display rules");
                        Console.WriteLine(rules);
                    }
                    else Console.WriteLine(-1);
                }
            }
            if (read == "help")
            {
                Console.WriteLine(rules);
                read = "";
                goto M;
            }
        }
    }
}
