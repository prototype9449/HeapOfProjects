using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppListSkip
{
    class Program
    {
        static void Main(string[] args)
        {
            var skipList = new ListSkip<int, int>(10, 0.5);
            var sortedList = new SortedList<int, int>();
            var random = new Random();

            var randomNumbers = new int[10000];

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                var number = random.Next(100000);
                while (randomNumbers.Contains(number))
                {
                    number = random.Next(100000);
                }
                randomNumbers[i] = number;
            }

            
            Console.WriteLine();
            var time1 = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 5000; i < 7000; i++)
            {
                skipList.Add(randomNumbers[i], randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp()-time1);

            Console.WriteLine();
            var time2 = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 5000; i < 7000; i++)
            {
                sortedList.Add(randomNumbers[i], randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - time2);

            Console.WriteLine();
            var time3 = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 5000; i < 7000; i++)
            {
                skipList.Remove(randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - time3);

            Console.WriteLine();
            var time4 = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 5000; i < 7000; i++)
            {
                sortedList.Remove(randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - time4);
        }
    }
}
