using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLabAVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestTree
{
    [TestClass]
    public class TestAdding
    {
        public BinaryTree<int, int> GetBinaryTree()
        {
            return new BinaryTree<int, int>() {TreeTraversal = Traversal.Width};
        }
        public BinaryTree<int, int> GetBinaryTree(int[] numbers)
        {
            var tree = new BinaryTree<int, int>();
            tree.AddRange(numbers.ToDictionary(i => i).ToArray());
            return tree;
        }

        [TestMethod]
        public void AfterAddingCountMustBeOne()
        {
            var tree = GetBinaryTree();
            tree.Add(1,1);
            Assert.AreEqual(tree.Count, 1);
        }

        [TestMethod]
        public void AfterAddingTraversalOfForeachMustMatch()
        {
            var tree = GetBinaryTree();
            tree.Add(1,1);
            foreach (var node in tree)
            {
                Assert.AreEqual(node.Key, 1);
            }
        }

        [TestMethod]
        public void AfterAddingThreeElementsCountMustMatch()
        {
            var tree = GetBinaryTree();
            tree.Add(1,1);
            tree.Add(2,2);
            tree.Add(3,3);
            Assert.AreEqual(tree.Count, 3);
        }

        [TestMethod]
        public void AfterAddingRangeElementsCountMustMatch()
        {
            var tree = GetBinaryTree(new []{1,2,3});
            Assert.AreEqual(tree.Count, 3);
        }

        [TestMethod]
        public void TestSmallLeftRotate()
        {
            var tree = GetBinaryTree(new[] {1, 2, 3});
            var matchCollection = new int[] {2, 1, 3};
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestSmallRightRotate()
        {
            var tree = GetBinaryTree(new[] { 3,2,1 });
            var matchCollection = new int[] { 2, 1, 3 };
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestSmallLeftRightRotate()
        {
            var tree = GetBinaryTree(new[] { 10,5,7 });
            var matchCollection = new int[] { 7,5,10 };
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestSmallRightLeftRotate()
        {
            var tree = GetBinaryTree(new[] { 10, 15, 12 });
            var matchCollection = new int[] { 12, 10,15 };
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestBigRightRotate()
        {
            var tree = GetBinaryTree(new[] { 10, 15, 5, 3, 7, 1 });
            var matchCollection = new int[] { 5,3,10,1,7,15};
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestBigLeftRotate()
        {
            var tree = GetBinaryTree(new[] { 10, 15, 5, 12, 20, 25 });
            var matchCollection = new int[] { 15,10,20,5,12,25 };
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestBigRightWithSmallLeftRotate()
        {
            var tree = GetBinaryTree(new[] { 10, 15, 5, 3, 7, 8 });
            var matchCollection = new int[] { 7,5,10,3,8,15 };
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void TestBigLeftWithSmallRightRotate()
        {
            var tree = GetBinaryTree(new[] { 10, 15, 5, 12, 20, 11 });
            var matchCollection = new int[] { 12,10,15,5,11,20 };
            var index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchCollection[index], node.Key);
                index++;
            }
        }

    }
}
