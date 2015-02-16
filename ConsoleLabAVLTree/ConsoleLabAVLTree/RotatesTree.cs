using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    public partial class SubTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public void RecalculateBalanceFactor()
        {
            _balanceFactor = 0;
            if (Left != null)
            {
                _balanceFactor--;
                if (Left.IsHaveAnyChild())
                {
                    _balanceFactor--;
                }
            }
            if (Rigth != null)
            {
                _balanceFactor++;
                if (Rigth.IsHaveAnyChild())
                {
                    _balanceFactor++;
                }
            }
        }

        private bool IsHaveAnyChild()
        {
            return Left != null || Rigth != null;
        }
        public void LeftLeftRotate()
        {
            var mostNode = this;
            var averageNode = this.Left;

            averageNode.Root = Root;
            averageNode.Rigth = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.RecalculateBalanceFactor();
            mostNode.RecalculateBalanceFactor();
        }

        public void RightRightRotate()
        {
            var averageNode = Rigth;
            var leastNode = this;

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Rigth = null;

            averageNode.RecalculateBalanceFactor();
            leastNode.RecalculateBalanceFactor();
        }

        public void LeftRightRotate()
        {
            var mostNode = this;
            var leastNode = Left;
            var averageNode = Left.Rigth;

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Rigth = null;
            averageNode.Rigth = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.RecalculateBalanceFactor();
            mostNode.RecalculateBalanceFactor();
            leastNode.RecalculateBalanceFactor();
        }

        

        public void RightLeftRotate()
        {
            var mostNode = Rigth;
            var leastNode = this;
            var averageNode = Rigth.Left;

            averageNode.Root = Root;
            averageNode.Left = leastNode;
            leastNode.Root = averageNode;
            leastNode.Rigth = null;
            averageNode.Rigth = mostNode;
            mostNode.Root = averageNode;
            mostNode.Left = null;

            averageNode.RecalculateBalanceFactor();
            mostNode.RecalculateBalanceFactor();
            leastNode.RecalculateBalanceFactor();
        }
    }

}
