using System;
using Emil.GMP;
using ProbablePrime;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bigInt = "911";
            IProbabilisticPrimeTest testPRimeNumber = new TestPrimeNumber();
            Console.WriteLine(testPRimeNumber.isPrimeNumber(bigInt,23));
          
        }
    }
}