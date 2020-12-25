using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HW_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Get stats of array, made by Denys Sakadel");
            M:  Console.WriteLine("Enter numbers, between which u must use space or coma");
            string s = "";
            try
            {
                s = Console.ReadLine();
            }catch (Exception)
            {
                Console.WriteLine( "You entered wrong expression");
                goto M;
            }
            //string s = "1, 2,3 ,,4 , 5";
            Regex r = new Regex(@"[\s,]?(\d+)[\s,]?", RegexOptions.IgnoreCase);
            Match matches = r.Match(s);
            List<int> mas = new List<int>();
            while (matches.Success)
            {
                Group g = matches.Groups[1];
                string m = g.ToString();
                mas.Add(int.Parse(m));
                matches = matches.NextMatch();
            }

            Console.WriteLine("Min: " + mas.Min() );
            Console.WriteLine("Max: " + mas.Max());
            Console.WriteLine("Sum: " + mas.Sum());
            Console.WriteLine("Avg: " + mas.Average());
            Console.WriteLine ("Otkl: " + Math.Sqrt(mas.Sum(s=>Math.Pow(s - mas.Average(),2))/mas.Count) );
            var arr = mas.Distinct().OrderBy(i => i).ToArray();
            foreach (var i in arr)
            {
                Console.Write(i + " ");
            }
        }
    }
}
