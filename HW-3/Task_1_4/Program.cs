using System;
using System.Collections.Generic;

namespace Task_1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Check if it's pare of brackets, made by Denys Sakadel");
            Console.WriteLine("Enter pls expression");
            var expression = Console.ReadLine();


            var stack = new Stack<(char, int)>();

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if ( (c == '<') || (c == '(') || (c == '{') || (c == '[') ) stack.Push((c, i));
                
                else if ( (c == '>') || (c == ')') || (c == '}') || (c == ']') )
                {
                    if (!stack.TryPop(out var pos) || ( (pos.Item1 + 1 != c) && (pos.Item1 + 2 != c) ) )
                    {
                        Console.WriteLine($"Error at position =>  {i}, bracket {c} doesn't have pair");
                        return;
                    }
                }
            }
            if (stack.Count != 0)
            {
                Console.WriteLine($"Error at position => {stack.Peek().Item2}, bracket => {stack.Peek().Item1} doesn't have pair");
            }
            else
            {
                Console.WriteLine("Correct!!!");
            }

        }
    }
}
