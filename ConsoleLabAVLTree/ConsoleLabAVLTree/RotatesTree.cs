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
            MakeLeafOfRoot(averageNode);

            averageNode.Root = Root;
            averageNode.Right = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.CalculateHeighAndBallance();
            mostNode.CalculateHeighAndBallance();
        }

        protected void MakeLeafOfRoot(SubTree<TKey, TValue> averageNode)
        {
            if (Root == null) return;

            if (IsLeft())
            {
                Root.Left = averageNode;
            }
            if (IsRight())
            {
                Root.Right = averageNode;
            }
        }

        public void SmallLeftRotate()
        {
            var averageNode = Right;
            var leastNode = this;

            MakeLeafOfRoot(averageNode);

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Right = null;

            averageNode.CalculateHeighAndBallance();
            leastNode.CalculateHeighAndBallance();
        }

        public void SmallLeftRightRotate()
        {
            var mostNode = this;
            var leastNode = Left;
            var averageNode = Left.Right;

            MakeLeafOfRoot(averageNode);

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Right = null;
            averageNode.Right = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.CalculateHeighAndBallance();
            mostNode.CalculateHeighAndBallance();
            leastNode.CalculateHeighAndBallance();
        }

        public void SmallRightLeftRotate()
        {
            var mostNode = Right;
            var leastNode = this;
            var averageNode = Right.Left;

            MakeLeafOfRoot(averageNode);

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Right = null;
            averageNode.Right = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.CalculateHeighAndBallance();
            mostNode.CalculateHeighAndBallance();
            leastNode.CalculateHeighAndBallance();
        }

        public void RightRotate()
        {
            var mostNode = this;
            var leastNode = Left;
            var averageNode = leastNode.Right;

            MakeLeafOfRoot(leastNode);

            mostNode.Left = averageNode;
            leastNode.Root = Root;
            mostNode.Root = leastNode;
            leastNode.Right = mostNode;

            if (averageNode != null)
            {
                averageNode.Root = mostNode;
            }

            if (averageNode != null) averageNode.CalculateHeighAndBallance();
            mostNode.CalculateHeighAndBallance();
            leastNode.CalculateHeighAndBallance();
        }

        public void LeftRotate()
        {
            var mostNode = Right;
            var leastNode = this;
            var averageNode = mostNode.Left;

            MakeLeafOfRoot(mostNode);

            mostNode.Root = Root;
            leastNode.Root = mostNode;
            leastNode.Right = averageNode;
            if (averageNode != null)
            {
                averageNode.Root = leastNode;
            }
            mostNode.Left = leastNode;

            if (averageNode != null) averageNode.CalculateHeighAndBallance();
            mostNode.CalculateHeighAndBallance();
            leastNode.CalculateHeighAndBallance();
        }

        public List<int> GetBalanceFactors()
        {
            return GetBalanceFactorsRecursiv(new List<int>());
        }

        public List<int> GetBalanceFactorsRecursiv(List<int> balances)
        {
            var balanceFactor = GetBalanceFactor();
            balances.Add(balanceFactor);
            if (Root == null)
            {
                return balances;
            }
            return GetBalanceFactorsRecursiv(balances);
        }

        public void TryToBalanceSubTree()
        {
            CalculateHight();
            var balanceFactor = GetBalanceFactor();
            if (balanceFactor == 2 || balanceFactor == -2)
            {
                var balanceFactorOfSecondNode = 0;
                if (balanceFactor == -2)
                {
                    balanceFactorOfSecondNode = Left.GetBalanceFactor();
                }
                else
                {
                    balanceFactorOfSecondNode = Right.GetBalanceFactor();
                }

                if (Heigh == 2)
                {
                    SmallRotateSubTree(balanceFactor, balanceFactorOfSecondNode);
                }
                else
                {
                    BigRotateSubTree(balanceFactor, balanceFactorOfSecondNode);
                }
            }
        }

        public void BigRotateSubTree(int firstBalanceFactor, int secondBalanceFactor)
        {
            if (firstBalanceFactor == -2 && secondBalanceFactor == 1)
            {
                Left.LeftRotate();
                RightRotate();
            }
            if (firstBalanceFactor == -2 && secondBalanceFactor == -1)
            {
                RightRotate();
            }
            if (firstBalanceFactor == 2 && secondBalanceFactor == 1)
            {
                LeftRotate();
            }
            if (firstBalanceFactor == 2 && secondBalanceFactor == -1)
            {
                Right.RightRotate();
                LeftRotate();
            }
        }

        public void SmallRotateSubTree(int firstBalanceFactor, int secondBalanceFactor)
        {
            if (firstBalanceFactor == -2 && secondBalanceFactor == 1)
            {
                SmallLeftRightRotate();
            }
            if (firstBalanceFactor == -2 && secondBalanceFactor == -1)
            {
                SmallRightRotate();
            }
            if (firstBalanceFactor == 2 && secondBalanceFactor == 1)
            {
                SmallLeftRotate();
            }
            if (firstBalanceFactor == 2 && secondBalanceFactor == -1)
            {
                SmallRightLeftRotate();
            } 
        }

        public int GetBalanceFactor()
        {
            if (Left != null && Right != null)
            {
                return Right.Heigh - Left.Heigh;
            }
            if (Left != null && Right == null)
            {
                return -1*Heigh;
            }
            return Heigh;
        }
    }
}
