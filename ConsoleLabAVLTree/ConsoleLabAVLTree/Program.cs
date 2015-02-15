using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    class Program
    {
        static private Random _random = new Random();
        static void Main(string[] args)
        {
            const int CountNumbers = 10000;
            var sortedDictionary = new SortedDictionary<int, int>();
            var tree = new BinaryTree<int,int>();
            int[] randomNumbers = GenerateRandomNumbers(CountNumbers);

           // AddItemsToDictionary(randomNumbers, sortedDictionary);
            //AddItemsInTree(tree, randomNumbers);
            //RemoveItemsInDictionary(sortedDictionary, randomNumbers);
           // RemoveItemsInTree(tree, randomNumbers);

            var sampleTree = new BinaryTree<int,int>();
            var arrayInt = new []{100,200,300};
            var pairs = new KeyValuePair<int, int>[arrayInt.Length];
            for (int i = 0; i < arrayInt.Length; i++)
            {
                pairs[i] = new KeyValuePair<int, int>(arrayInt[i], arrayInt[i]);
            }
            sampleTree.AddRange(pairs);


            sampleTree.TreeTraversal = Traversal.Depth;
            foreach (var i in sampleTree)
            {
                Console.WriteLine(i.Key);
            }
            Console.WriteLine();

            sampleTree.TreeTraversal = Traversal.Width;
            foreach (var i in sampleTree)
            {
                Console.WriteLine(i.Key);
            }
            Console.WriteLine();


            Console.ReadKey();
        }

        private static void RemoveItemsInTree(BinaryTree<int,int> tree, int[] randomNumbers)
        {
            Console.WriteLine();
            Console.WriteLine("Remove items in tree");
            var currenttamp = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 1; i < 10000; i++)
            {
                tree.Remove(randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - currenttamp);
        }

        private static void RemoveItemsInDictionary(SortedDictionary<int, int> sortedDictionary, int[] randomNumbers)
        {
            Console.WriteLine();
            Console.WriteLine("Remove items in Sort Dictionary");
            var currenttamp = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 5000; i < 7000; i++)
            {
                sortedDictionary.Remove(randomNumbers[i]);
            }
            Console.WriteLine(Stopwatch.GetTimestamp() - currenttamp);
        }

        private static void AddItemsInTree(BinaryTree<int,int> tree, int[] randomNumbers)
        {
            Console.WriteLine();
            Console.WriteLine("Add items in tree");
            var currenttamp = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            var pairs = new KeyValuePair<int, int>[randomNumbers.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                pairs[i] = new KeyValuePair<int, int>(randomNumbers[i],randomNumbers[i]);
            }
            tree.AddRange(pairs);
            Console.WriteLine(Stopwatch.GetTimestamp() - currenttamp);
        }

        private static void AddItemsToDictionary(int[] randomNumbers, SortedDictionary<int, int> sortedDictionary)
        {
            Console.WriteLine();
            Console.WriteLine("Add items in Sort Dictionary");
            var currenttamp = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            Array.ForEach(randomNumbers, item =>
            {
                if (!sortedDictionary.ContainsKey(item))
                    sortedDictionary.Add(item, item);
            });
            Console.WriteLine(Stopwatch.GetTimestamp() - currenttamp);
        }

        public static int[] GenerateRandomNumbers(int number)
        {

            const int UpperLimit = 1000000;
            var randomNumbers = new int[number];
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                int randomNum = 0;
                while (randomNumbers.Contains(randomNum = _random.Next(UpperLimit)))
                {
                }

                randomNumbers[i] = randomNum;
            }
            return randomNumbers;
        }
    }
}
