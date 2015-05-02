using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashTableDoubleHashing;
using UnitTestDoubleHashTable;

namespace ConsoleMeasuringAndСomparisonTimeWorkDoubleHashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var table =new DoubleHashTable<MyInt, int>(new SizeHashTable());
            var dictionary = new Dictionary<MyInt, int>();
            var random = new Random();
            var randomNumbers = new MyInt[10000];
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = new MyInt(random.Next(100000));
            }

            var beginTime = Stopwatch.GetTimestamp();
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                table.Add(randomNumbers[i], randomNumbers[i].Number);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - beginTime);

            beginTime = Stopwatch.GetTimestamp();
            Array.ForEach(randomNumbers, item =>
            {
                if (!dictionary.ContainsKey(item))
                    dictionary.Add(item, item.Number);
            });
            Console.WriteLine(Stopwatch.GetTimestamp() - beginTime);

            beginTime = Stopwatch.GetTimestamp();
            for (int i = 5000; i < 7000; i++)
            {
                table.Remove(randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - beginTime);

            beginTime = Stopwatch.GetTimestamp();
            for (int i = 5000; i < 7000; i++)
            {
                dictionary.Remove(randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - beginTime);
            Console.ReadKey();
        }
    }
}
