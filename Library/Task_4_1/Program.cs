using System;

namespace Task_4_1
{
    class PaymentServiceException : Exception
    {
        public PaymentServiceException()
        {
            throw new PaymentServiceException();
        }
    }
    class LimitExceededException : PaymentServiceException
    {
        public LimitExceededException()
        {
            throw new LimitExceededException();
        }
    }
    class InsufficientFundsException: PaymentServiceException
    {
        public InsufficientFundsException()
        {
            throw new InsufficientFundsException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
