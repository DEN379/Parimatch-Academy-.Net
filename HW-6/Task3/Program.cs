using System;
using System.Collections.Concurrent;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test sign in, made by Denys Sakadel");

            ConcurrentQueue<User> listLogin = AuthReader.ParseLoginsAndPass();

            SignInTest signInTest = new SignInTest(listLogin);

            Console.WriteLine("Enter quantity of thread to test sign in => ");

            int quantity = 0;
            try
            {
                quantity = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("You entered wrong expression, enter pls number of threads next time");
                return;
            }

            Result result = signInTest.Test(quantity);

            JsonWriter.Write(result);
        }

    }
}
