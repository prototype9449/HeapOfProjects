using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLabAVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestTree
{
    [TestClass]
    public class TestRemoving
    {
        public BinaryTree<int, int> GetBinaryTree()
        {
            return new BinaryTree<int, int>() { TreeTraversal = Traversal.Width };
        }
        public BinaryTree<int, int> GetBinaryTree(int[] numbers)
        {
            var tree = new BinaryTree<int, int>();
            tree.AddRange(numbers.ToDictionary(i => i).ToArray());
            return tree;
        }

        [TestMethod]
        public void AfterRemoveNonexistentElementCountShouldNotChange()
        {
            var tree = GetBinaryTree();
            tree.Remove(1);
            Assert.AreEqual(tree.Count,0);
        }

        [TestMethod]
        public void AfterRemoveHeadCountShouldEqualZero()
        {
            var tree = GetBinaryTree(new []{1});
            tree.Remove(1);
            Assert.AreEqual(tree.Count, 0);
        }

        [TestMethod]
        public void AfterRemoveHeadCountShouldDecreaseByOneIfElementsTwo()
        {
            var tree = GetBinaryTree(new[] { 1, 2});
            tree.Remove(1);
            Assert.AreEqual(tree.Count, 1);
        }

        [TestMethod]
        public void AfterRemoveHeadCountShouldDecreaseByOneIfElementsThree()
        {
            var tree = GetBinaryTree(new[] { 1,2,3 });
            tree.Remove(2);
            Assert.AreEqual(tree.Count, 2);
        }
        [TestMethod]
        public void AfterRemoveHeadElementTraversalMustMatchIfElementsThree()
        {
            var tree = GetBinaryTree(new[] { 1, 2, 3 });
            tree.Remove(2);
            var matchNumbers = new int[] {3, 1};
            int index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchNumbers[index],node.Key);
                index++;
            }
        }

        [TestMethod]
        public void AfterRemoveHeadTraversalMustMatchIfElementsTwo()
        {
            var tree = GetBinaryTree(new[] { 1, 2});
            tree.Remove(1);
            var matchNumbers = new int[] {2};
            int index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchNumbers[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void AfterRemoveExistingElementTraversalMustMatchIfElementsFour()
        {
            var tree = GetBinaryTree(new[] { 1, 2, 3, 4 });
            tree.Remove(3);
            var matchNumbers = new int[] { 2,1,4 };
            int index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchNumbers[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void AfterRemoveHeadTraversalMustMatchIfElementsFour()
        {
            var tree = GetBinaryTree(new[] { 1, 2, 3, 4 });
            tree.Remove(2);
            var matchNumbers = new int[] { 3, 1, 4 };
            int index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchNumbers[index], node.Key);
                index++;
            }
        }

        [TestMethod]
        public void AfterRemoveHeadTraversalMustMatchIfElementsFive()
        {
            var tree = GetBinaryTree(new[] { 1, 2, 3, 4, 5 });
            tree.Remove(2);
            var matchNumbers = new int[] { 3,1,4,5 };
            int index = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(matchNumbers[index], node.Key);
                index++;
            }
        }
    }
}
