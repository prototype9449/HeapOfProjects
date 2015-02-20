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
        public void AfterRemoveFirstElementCountShouldEqualZero()
        {
            var tree = GetBinaryTree(new []{1});
            tree.Remove(1);
            Assert.AreEqual(tree.Count, 0);
        }

        [TestMethod]
        public void AfterRemoveExistingElementCountShouldDecreaseByOne()
        {
            var tree = GetBinaryTree(new[] { 1,2,3 });
            tree.Remove(2);
            Assert.AreEqual(tree.Count, 2);
        }
    }
}
