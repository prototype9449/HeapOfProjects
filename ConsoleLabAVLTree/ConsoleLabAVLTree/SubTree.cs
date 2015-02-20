using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    public partial class SubTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public SubTree<TKey, TValue> Root { get; private set; }
        public SubTree<TKey, TValue> Left { get; set; }
        public SubTree<TKey, TValue> Right { get; set; }
        public TValue Value { get; set; }
        public TKey Key { get; private set; }
        public TValue this[TKey key]
        {
            get { return GetSubTreeByKey(key).Value; }
            set { GetSubTreeByKey(key).Value = value; }
        }

        

        public int heigh;

        public SubTree(TKey key, TValue value)
        {
            Value = value;
            Key = key;
            heigh = 0;
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

        


        private bool AddToLeftSubTree(TKey key, TValue value)
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

        private void CalculateHeighAndBallance()
        {
            CalculateHight();
            TryToBalanceSubTree();
        }

        private void CalculateHight()
        {
            if (Left != null && Right != null)
            {
                heigh = Math.Max(Left.heigh, Right.heigh) + 1;
            }
            else if (Left != null && Right == null)
            {
                heigh = Left.heigh + 1;
            }
            else if (Left == null && Right != null)
            {
                heigh = Right.heigh + 1;
            }
            else
            {
                heigh = 0;
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
       

        private void AddSubTree(SubTree<TKey, TValue> subTree)
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



        private SubTree<TKey, TValue> GetSubTreeByKey(TKey otherKey)
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
                    DeleteAndTie(deleteNode);
                    deleteNode.Root.BalanceAllNodeToTheRoot();
                    return true;
                }
                if (deleteNode.IsNotHaveChild())
                {
                    DeleteLeaf(deleteNode);
                    if (deleteNode.Root != null)
                    {
                        deleteNode.Root.BalanceAllNodeToTheRoot();
                    }
                    return true;
                }

                DeleteNodeWithMoving(deleteNode);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        private void BalanceAllNodeToTheRoot()
        {
            TryToBalanceSubTree();
            if (Root != null)
            {
                Root.TryToBalanceSubTree();
            }
        }

        private void DeleteLeaf(SubTree<TKey, TValue> deleteNode)
        {
            if (deleteNode.Root == null)
            {
                deleteNode = null;
                return;
            }
            if (deleteNode.IsRight())
            {
                deleteNode.Root.Right = null;
            }
            else
            {
                deleteNode.Root.Left = null;
            }
        }

        private bool IsNotHaveChild()
        {
            return Left == null && Right == null;
        }

        private bool IsHaveOneChild()
        {
            return Left != null ^ Right != null;
        }

        private void DeleteNodeWithMoving(SubTree<TKey, TValue> deleteNode)
        {
            var hightOfLeftSubTree = deleteNode.Left != null ? deleteNode.Left.heigh : 0;
            var hightOfRightSubTree = deleteNode.Right != null ? deleteNode.Right.heigh : 0;
            
            if (hightOfLeftSubTree > hightOfRightSubTree)
            {
                SubTree<TKey, TValue> movingNode = deleteNode.Left;
                while (movingNode.Right!= null)
                {
                    movingNode = movingNode.Right;
                }

                ChangeAndDelete(deleteNode, movingNode);
            }
            else
            {
                SubTree<TKey, TValue> movingNode = deleteNode.Right;
                while (movingNode.Left != null)
                {
                    movingNode = movingNode.Left;
                }

                ChangeAndDelete(deleteNode, movingNode);
            }

        }

        private void ChangeAndDelete(SubTree<TKey, TValue> deleteNode, SubTree<TKey, TValue> movingNode)
        {
            DeleteAndTie(movingNode);
            movingNode.Root = deleteNode.Root;
            if (deleteNode.Root != null)
            {
                if (deleteNode.IsLeft())
                {
                    deleteNode.Root.Left = movingNode;
                }
                else
                {
                    deleteNode.Root.Right = movingNode;
                }
            }
            movingNode.Left = deleteNode.Left;
            deleteNode.Left.Root = movingNode;

            movingNode.Right = deleteNode.Right;
            deleteNode.Right.Root = movingNode;
        }

        private void DeleteAndTie(SubTree<TKey, TValue> deleteNode)
        {
            if (deleteNode.heigh == 0)
            {
                DeleteLeaf(deleteNode);
            }
            if (deleteNode.IsLeft())
            {
                if (deleteNode.Left != null)
                {
                    deleteNode.Left.Root = deleteNode.Root;
                    deleteNode.Root.Left = deleteNode.Left;
                    return;
                }
                if (deleteNode.Right != null)
                {
                    deleteNode.Right.Root = deleteNode.Root;
                    deleteNode.Root.Left = deleteNode.Right;
                }
            }
            else
            {
                if (deleteNode.Right!= null)
                {
                    deleteNode.Right.Root = deleteNode.Root;
                    deleteNode.Root.Right = deleteNode.Right;
                    return;
                }
                if (deleteNode.Left != null)
                {
                    deleteNode.Left.Root = deleteNode.Root;
                    deleteNode.Root.Right = deleteNode.Left;
                }
            }
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

        private bool IsRight()
        {
            if (Root.Right == null)
                return false;
            return Root.Right.Key.Equals(Key);
        }
        private bool IsLeft()
        {
            if (Root.Left == null)
                return false;
            return Root.Left.Key.Equals(Key);
        }
    }
}
