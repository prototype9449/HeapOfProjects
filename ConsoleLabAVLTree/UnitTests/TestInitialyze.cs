
using System.Collections.Generic;
using ConsoleLabAVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestInitialyze
    {

        public BinaryTree<int, int> GetBinaryTree()
        {
            return new BinaryTree<int, int>();
        }
        public BinaryTree<int, int> GetBinaryTree(KeyValuePair<int, int> pair)
        {
            return new BinaryTree<int, int>(pair.Key, pair.Value);
        }

        public KeyValuePair<int, int> GetKeyValuePair()
        {
            return new KeyValuePair<int, int>(10, 10);
        }

        [TestMethod]
        public void AfterCreateTreeCountEqualZero()
        {
            var tree = GetBinaryTree();
            Assert.AreEqual(tree.Count, 0);
        }

        [TestMethod]
        public void AfterCreateOfParameter()
        {
            var tree = GetBinaryTree(GetKeyValuePair());
            
            Assert.AreEqual(tree.Count, 1);
        }

        [TestMethod]
        public void AfterCreateOfParameterValueMustMatch()
        {
            var tree = GetBinaryTree(GetKeyValuePair());
            Assert.AreEqual(tree[GetKeyValuePair().Key],GetKeyValuePair().Value);
        }

        [TestMethod]
        public void AfterCreateOfParameterTraversalThroughForeachMustMatch()
        {
            var tree = GetBinaryTree(GetKeyValuePair());
            foreach (var node in tree)
            {
                Assert.AreEqual(node.Key, GetKeyValuePair().Key);
            }
        }

       
    }
}
