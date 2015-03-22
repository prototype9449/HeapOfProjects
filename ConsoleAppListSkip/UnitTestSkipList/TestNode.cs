using System;
using System.Collections.Generic;
using ConsoleAppListSkip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestSkipList
{
    [TestClass]
    public class TestNode
    {

        private List<KeyValuePair<int, int>> GetPairs()
        {
            var pairs = new List<KeyValuePair<int, int>>();
            var numbers = new [] {10, 15, 20, 30};
            Array.ForEach(numbers, item => pairs.Add(new KeyValuePair<int, int>(item,item)));
            return pairs;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var node = new Node<int, int>();
            Assert.IsFalse(node == null);
            node = new Node<int, int>(10,10);
            Assert.IsTrue(node.Equals(new Node<int, int>(10,25)));
            node = new Node<int, int>(10,10);
            var othernode = new Node<int, int>(15, 15);
            Assert.IsTrue(node < othernode);
            Node<int, int> element = null;
            Assert.AreEqual(element,null);

        }

        [TestMethod]
        public void TestAdding()
        {
            var skipList = new ListSkip<int, int>(3,0.5);
            var pairs = GetPairs();
            Array.ForEach(pairs.ToArray(), item => skipList.Add(item.Key, item.Value));
            int i = 0;
            foreach(var pair in skipList)
            {
                Assert.AreEqual(pairs[i].Key, pair.Key);
                Assert.IsTrue(skipList.Contains(pair.Key, pair.Value));
                i++;
            }
            Assert.AreEqual(skipList.Count, pairs.Count);
            Assert.AreEqual(skipList[10],10);
            Assert.AreEqual(skipList[15], 15);
            Assert.AreEqual(skipList[20], 20);
        }

        [TestMethod]
        public void TestRemoving1()
        {
            var skipList = new ListSkip<int, int>(3, 0.5);
            var pairs = GetPairs();
            Array.ForEach(pairs.ToArray(), item => skipList.Add(item.Key, item.Value));
            skipList.Remove(10);
            Assert.IsFalse(skipList.Contains(10, 10));
        }

        [TestMethod]
        public void TestRemoving2()
        {
            var skipList = new ListSkip<int, int>(3, 0.5);
            var pairs = GetPairs();
            Array.ForEach(pairs.ToArray(), item => skipList.Add(item.Key, item.Value));
            skipList.Remove(15);
            skipList.Remove(20);
            skipList.Remove(30);

            Assert.IsFalse(skipList.Contains(15, 15));
            Assert.IsFalse(skipList.Contains(20, 20));
            Assert.IsFalse(skipList.Contains(30, 30));

        }
    }
}
