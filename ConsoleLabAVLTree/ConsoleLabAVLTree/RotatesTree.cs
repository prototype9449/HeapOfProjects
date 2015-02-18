using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    public partial class SubTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
       
        public void SmallRightRotate()
        {
            var mostNode = this;
            var averageNode = this.Left;

            averageNode.Root = Root;
            averageNode.Right = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.CalculateHeigh();
            mostNode.CalculateHeigh();
        }

        public void SmallLeftRotate()
        {
            var averageNode = Right;
            var leastNode = this;

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Right = null;

            averageNode.CalculateHeigh();
            leastNode.CalculateHeigh();
        }

        public void SmallLeftRightRotate()
        {
            var mostNode = this;
            var leastNode = Left;
            var averageNode = Left.Right;

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Right = null;
            averageNode.Right = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.CalculateHeigh();
            mostNode.CalculateHeigh();
            leastNode.CalculateHeigh();
        }

        public void SmallRightLeftRotate()
        {
            var mostNode = Right;
            var leastNode = this;
            var averageNode = Right.Left;

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Right = null;
            averageNode.Right = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.CalculateHeigh();
            mostNode.CalculateHeigh();
            leastNode.CalculateHeigh();
        }

        public void RightRotate()
        {
            var mostNode = this;
            var leastNode = Left;
            var averageNode = leastNode.Right;

            mostNode.Left = averageNode;
            mostNode.Root = leastNode;
            leastNode.Root = Root;
            leastNode.Right = mostNode;
            averageNode.Root = mostNode;
        }

        public void LeftRotate()
        {
            var mostNode = Right;
            var leastNode = this;
            var averageNode = mostNode.Left;

            mostNode.Root = Root;
            leastNode.Root = mostNode;
            leastNode.Right = averageNode;
            averageNode.Root = leastNode;
            mostNode.Left = leastNode;
        }

        private void TryToBalanceSubTree()
        {
            var balanceFactor = GetBalanceFactor();
            if (balanceFactor == 2 || balanceFactor == -2)
            {
                throw new Exception("2 or -2");
            }
        }

        private int GetBalanceFactor()
        {
            if (Left != null && Right != null)
            {
                return Left.heigh - Right.heigh;
            }
            return heigh;
        }
    }

}
