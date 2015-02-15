using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    public class SubTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public SubTree<TKey, TValue> Root { get; private set; }
        public SubTree<TKey, TValue> Left { get; set; }
        public SubTree<TKey, TValue> Rigth { get; set; }
        public TValue Value { get; set; }
        public TKey Key { get; private set; }
        public TValue this[TKey key]
        {
            get { return GetSubTreeByKey(key).Value; }
            set { GetSubTreeByKey(key).Value = value; }
        }

        public short _balanceFactor;

        public SubTree(TKey key, TValue value)
        {
            Value = value;
            Key = key;
            _balanceFactor = 0;
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
                return true;
            }
            if (AddToRightSubTree(key, value))
            {
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
                    _balanceFactor--;

                    if (Rigth == null)
                        ChangeBalanceFactorOfRoot();
                }
                else
                {
                    Left.Add(key, value);
                }
                return true;
            }
            return false;
        }
        
        private bool AddToRightSubTree(TKey otherKey, TValue otherValue)
        {
            if (otherKey.CompareTo(Key) == 1)
            {
                if (Rigth == null)
                {
                    Rigth = new SubTree<TKey, TValue>(otherKey, otherValue) { Root = this };
                    _balanceFactor++;

                    if (Left == null)
                        ChangeBalanceFactorOfRoot();
                }
                else
                {
                    Rigth.Add(otherKey, otherValue);
                }
                return true;
            }
            return false;
        }
        

        private void DecreaseBalanceFactor()
        {
            _balanceFactor--;
            if (_balanceFactor == -2)
            {
                throw new Exception("-2");
            }
            ChangeBalanceFactorOfRoot();
        }

        private void IncreaseBalanceFactor()
        {
            _balanceFactor++;
            if (_balanceFactor == 2)
            {
                throw new Exception("2");
            }
            ChangeBalanceFactorOfRoot();
        }
        private void ChangeBalanceFactorOfRoot()
        {
            if (Root != null)
            {
                if (IsLeft())
                {
                    Root.DecreaseBalanceFactor();
                }
                else
                {
                    Root.IncreaseBalanceFactor();
                }
            }
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
                if (Rigth == null)
                {
                    Rigth = subTree;
                    subTree.Root = this;
                }
                else
                {
                    Rigth.AddSubTree(subTree);
                }
            }
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
            if (Rigth != null)
            {
                if (otherKey.CompareTo(Key) == 1)
                {
                    return Rigth.GetSubTreeByKey(otherKey);
                }
            }
            throw new ArgumentNullException("Value is not found");
        }

        public bool Delete(TKey otherKey)
        {
            try
            {
                var deleteNode = GetSubTreeByKey(otherKey);
                if (deleteNode.IsLeft())
                {
                    if (DeleteNodeInLeft(deleteNode)) return true;
                }
                return (DeleteNodeInRight(deleteNode));
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
        private bool DeleteNodeInRight(SubTree<TKey, TValue> deleteNode)
        {
            if (deleteNode.Rigth != null)
            {
                deleteNode.Root.Rigth = deleteNode.Rigth;
                deleteNode.Rigth.Root = deleteNode.Root;
                if (deleteNode.Left != null)
                {
                    deleteNode.Rigth.AddSubTree(deleteNode.Left);
                }
                return true;
            }
            if (deleteNode.Left != null)
            {
                deleteNode.Root.Rigth = deleteNode.Left;
                deleteNode.Left.Root = deleteNode.Root;
                return true;
            }
            deleteNode.Root.Rigth = null;
            return false;
        }
        private bool DeleteNodeInLeft(SubTree<TKey, TValue> deleteNode)
        {
            if (deleteNode.Rigth != null)
            {
                deleteNode.Root.Left = deleteNode.Rigth;
                deleteNode.Rigth.Root = deleteNode.Root;
                if (deleteNode.Left != null)
                {
                    deleteNode.Rigth.AddSubTree(deleteNode.Left);
                }
                return true;
            }
            if (deleteNode.Left != null)
            {
                deleteNode.Root.Left = deleteNode.Left;
                deleteNode.Left.Root = deleteNode.Root;
                return true;
            }
            deleteNode.Root.Left = null;
            return false;
        }

        private bool IsRight()
        {
            if (Root.Rigth == null)
                return false;
            return Root.Rigth.Key.Equals(Key);
        }
        private bool IsLeft()
        {
            if (Root.Left == null)
                return false;
            return Root.Left.Key.Equals(Key);
        }

        private bool IsHaveBothChild()
        {
            return Left != null && Rigth != null;
        }

    }
}
