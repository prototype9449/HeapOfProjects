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
            var tree = new BinaryTree<int, int>();
            int[] randomNumbers = GenerateRandomNumbers(CountNumbers);

            AddItemsToDictionary(randomNumbers, sortedDictionary);
            AddItemsInTree(tree, randomNumbers);
            RemoveItemsInDictionary(sortedDictionary, randomNumbers);
            RemoveItemsInTree(tree, randomNumbers);

            //ActionsWithTree();
            Console.ReadKey();
        }

        private static void ActionsWithTree()
        {
            var sampleTree = new BinaryTree<int, int>();
            int keyReadLineInt;

            while (int.TryParse(Console.ReadLine(), out keyReadLineInt))
            {
                Console.Clear();
                sampleTree.Add(keyReadLineInt, keyReadLineInt);
                Console.WriteLine();

                PritnToConsole(sampleTree);
            }
            Console.Write("Please enter node for removing = ");
            var node = int.Parse(Console.ReadLine());

            sampleTree.Remove(node);
            PritnToConsole(sampleTree);

            Console.WriteLine("The end");
        }

        private static void PritnToConsole(BinaryTree<int, int> sampleTree)
        {
            Console.WriteLine();
            sampleTree.TreeTraversal = Traversal.Width;
            var privousNode = sampleTree.First();
            foreach (var item in sampleTree)
            {
                if (item.Key < privousNode.Key)
                {
                    Console.WriteLine();
                }
                Console.WriteLine(item.Key);
                privousNode = item;
            }
            Console.WriteLine();
        }

        private static void RemoveItemsInTree(BinaryTree<int, int> tree, int[] randomNumbers)
        {
            Console.WriteLine();
            Console.WriteLine("Remove items in tree");
            var currenttamp = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();
            for (int i = 5000; i < 7000; i++)
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

        private static void AddItemsInTree(BinaryTree<int, int> tree, int[] randomNumbers)
        {
            Console.WriteLine();
            Console.WriteLine("Add items in tree");
            var currenttamp = Stopwatch.GetTimestamp();
            Stopwatch.StartNew();

            foreach (var number in randomNumbers)
            {
                tree.Add(number,number);
            }
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
