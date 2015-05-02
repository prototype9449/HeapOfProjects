using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    public partial class SubTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public SubTree<TKey, TValue> Root { get; set; }
        public SubTree<TKey, TValue> Left { get; set; }
        public SubTree<TKey, TValue> Right { get; set; }
        public TValue Value { get; set; }
        public TKey Key { get; protected set; }


        public TValue this[TKey key]
        {
            get { return GetSubTreeByKey(key).Value; }
            set { GetSubTreeByKey(key).Value = value; }
        }

        public int Heigh;


        public SubTree(TKey key, TValue value)
        {
            Value = value;
            Key = key;
            Heigh = 0;
        }

        public bool Contains(TKey key)
        {
            try
            {
                GetSubTreeByKey(key);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public bool Add(TKey key, TValue value)
        {
            if (AddToLeftSubTree(key, value))
            {
                CalculateHeighAndBallance();
                return true;
            }
            if (AddToRightSubTree(key, value))
            {
                CalculateHeighAndBallance();
                return true;
            }
            return false;
        }




        protected bool AddToLeftSubTree(TKey key, TValue value)
        {
            if (key.CompareTo(Key) == -1)
            {
                if (Left == null)
                {
                    Left = new SubTree<TKey, TValue>(key, value) { Root = this };
                }
                else
                {
                    Left.Add(key, value);
                }
                return true;
            }
            return false;
        }

        public void CalculateHeighAndBallance()
        {
            CalculateHight();
            TryToBalanceSubTree();
        }

        public void CalculateHight()
        {
            if (Left != null && Right != null)
            {
                Heigh = Math.Max(Left.Heigh, Right.Heigh) + 1;
            }
            else if (Left != null && Right == null)
            {
                Heigh = Left.Heigh + 1;
            }
            else if (Left == null && Right != null)
            {
                Heigh = Right.Heigh + 1;
            }
            else
            {
                Heigh = 0;
            }
        }

        private bool AddToRightSubTree(TKey otherKey, TValue otherValue)
        {
            if (otherKey.CompareTo(Key) == 1)
            {
                if (Right == null)
                {
                    Right = new SubTree<TKey, TValue>(otherKey, otherValue) { Root = this };
                }
                else
                {
                    Right.Add(otherKey, otherValue);
                }
                return true;
            }
            return false;
        }


        public void AddSubTree(SubTree<TKey, TValue> subTree)
        {
            if (subTree.Key.CompareTo(Key) == -1)
            {
                if (Left == null)
                {
                    Left = subTree;
                    subTree.Root = this;
                }
                else
                {
                    Left.AddSubTree(subTree);
                }
            }
            if (subTree.Key.CompareTo(Key) == 1)
            {
                if (Right == null)
                {
                    Right = subTree;
                    subTree.Root = this;
                }
                else
                {
                    Right.AddSubTree(subTree);
                }
            }
            CalculateHeighAndBallance();
        }



        public SubTree<TKey, TValue> GetSubTreeByKey(TKey otherKey)
        {
            if (Key.CompareTo(otherKey) == 0)
            {
                return this;
            }

            if (Left != null)
            {
                if (otherKey.CompareTo(Key) == -1)
                {
                    return Left.GetSubTreeByKey(otherKey);
                }
            }
            if (Right != null)
            {
                if (otherKey.CompareTo(Key) == 1)
                {
                    return Right.GetSubTreeByKey(otherKey);
                }
            }
            throw new ArgumentNullException("Value is not found");
        }

        public bool Delete(TKey otherKey)
        {
            try
            {
                var deleteNode = GetSubTreeByKey(otherKey);
                if (deleteNode.IsHaveOneChild())
                {
                    ActionWithNode<TKey, TValue>.DeleteAndTie(deleteNode);
                    if (deleteNode.Root != null)
                    {
                        deleteNode.Root.BalanceAllNodeToTheRoot();
                    }
                    return true;
                }
                if (deleteNode.IsNotHaveChild())
                {
                    ActionWithNode<TKey, TValue>.DeleteLeaf(deleteNode);
                    if (deleteNode.Root != null)
                    {
                        deleteNode.Root.BalanceAllNodeToTheRoot();
                    }
                    return true;
                }

                ActionWithNode<TKey, TValue>.DeleteNodeWithMoving(deleteNode);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public void BalanceAllNodeToTheRoot()
        {
            TryToBalanceSubTree();
            if (Root != null)
            {
                Root.TryToBalanceSubTree();
            }
        }



        public bool IsNotHaveChild()
        {
            return Left == null && Right == null;
        }

        public bool IsHaveOneChild()
        {
            return Left != null ^ Right != null;
        }

        //public bool Delete(TKey otherKey)
        //{
        //    try
        //    {
        //        var deleteNode = GetSubTreeByKey(otherKey);
        //        if (deleteNode.IsLeft())
        //        {
        //            if (DeleteNodeInLeft(deleteNode)) return true;
        //        }
        //        return (DeleteNodeInRight(deleteNode));
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        return false;
        //    }
        //}
        //private bool DeleteNodeInRight(SubTree<TKey, TValue> deleteNode)
        //{
        //    if (deleteNode.Right != null)
        //    {
        //        deleteNode.Root.Right = deleteNode.Right;
        //        deleteNode.Right.Root = deleteNode.Root;
        //        if (deleteNode.Left != null)
        //        {
        //            deleteNode.Right.AddSubTree(deleteNode.Left);
        //        }
        //        return true;
        //    }
        //    if (deleteNode.Left != null)
        //    {
        //        deleteNode.Root.Right = deleteNode.Left;
        //        deleteNode.Left.Root = deleteNode.Root;
        //        return true;
        //    }
        //    deleteNode.Root.Right = null;
        //    return false;
        //}
        //private bool DeleteNodeInLeft(SubTree<TKey, TValue> deleteNode)
        //{
        //    if (deleteNode.Right != null)
        //    {
        //        deleteNode.Root.Left = deleteNode.Right;
        //        deleteNode.Right.Root = deleteNode.Root;
        //        if (deleteNode.Left != null)
        //        {
        //            deleteNode.Right.AddSubTree(deleteNode.Left);
        //        }
        //        return true;
        //    }
        //    if (deleteNode.Left != null)
        //    {
        //        deleteNode.Root.Left = deleteNode.Left;
        //        deleteNode.Left.Root = deleteNode.Root;
        //        return true;
        //    }
        //    deleteNode.Root.Left = null;
        //    return false;
        //}

        public bool IsRight()
        {
            if (Root.Right == null)
                return false;
            return Root.Right.Key.Equals(Key);
        }
        public bool IsLeft()
        {
            if (Root.Left == null)
                return false;
            return Root.Left.Key.Equals(Key);
        }
    }
}
