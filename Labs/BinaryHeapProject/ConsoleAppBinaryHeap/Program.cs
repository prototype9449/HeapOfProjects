using System;
using System.Collections.Generic;
using System.Diagnostics;
using BinaryHeapLib;

namespace ConsoleAppBinaryHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            const int CountRandomNumbers = 10000;
            var randomNambers = new int[CountRandomNumbers];
            for (int i = 0; i < randomNambers.Length; i++)
            {
                randomNambers[i] = random.Next(CountRandomNumbers * 10);
            }

            Console.WriteLine("Test heap");
            var countTactsHeap = Stopwatch.GetTimestamp();
            new BinaryHeap<int>(randomNambers).GetSortedArray();
            Console.WriteLine("Heap : " + (Stopwatch.GetTimestamp() - countTactsHeap));

            Console.WriteLine("Test simple quick sort");
            var countTactsSimple = Stopwatch.GetTimestamp();
            QuickSort<int>.Sort(randomNambers);
            Console.WriteLine("Heap : " + (Stopwatch.GetTimestamp() - countTactsSimple));
        }
    }

    public static class QuickSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] array)
        {
            RecursiveSort(array, 0, array.Length - 1);
        }
        private static void RecursiveSort(T[] array, int begin, int end)
        {
            int first = begin;
            int last = end;
            T keyElement = array[(begin + end) / 2];

            do
            {
                while (array[first].CompareTo(keyElement) == -1) first++;
                while (array[last].CompareTo(keyElement) == 1) last--;

                if (first <= last)
                {
                    if (array[first].CompareTo(array[last]) == 1) 
                        array.Swap(first, last);

                    first++;
                    last--;
                }
            } while (first <= last);

            if (first < end)
                RecursiveSort(array, first, end);
            if (begin < last)
                RecursiveSort(array, begin, last);
        }
    }
}
