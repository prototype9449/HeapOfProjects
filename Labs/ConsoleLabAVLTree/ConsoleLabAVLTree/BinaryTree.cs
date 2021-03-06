﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLabAVLTree
{
    public enum Traversal
    {
        Width,
        Depth
    }
    public class BinaryTree<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private SubTree<TKey, TValue> _head;

        public BinaryTree()
        {
            Count = 0;
        }
        public BinaryTree(TKey key, TValue value)
        {
            Add(key, value);
        }
        

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public Traversal TreeTraversal { get; set; }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (_head == null)
            {
                _head = new SubTree<TKey, TValue>(item.Key,item.Value);
                Count = 1;
            }
            else
            {
                if (_head.Add(item.Key, item.Value))
                {
                    Count++;
                    RefreshHead();
                }
            }
        }
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Clear()
        {
            _head = null;
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _head.Contains(item.Key)&& _head[item.Key].Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(TKey key)
        {
            if (_head == null)
                return false;

            if (_head.Key.Equals(key))
            {
                RemoveHead();
                return true;
            }
            if (_head.Delete(key))
            {
                Count--;
                RefreshHead();
                return true;
            }
            return false;
        }

        private void RefreshHead()
        {
            while (_head.Root != null) _head = _head.Root;
            if (_head.Right != null) _head = _head.Right.Root;
            if (_head.Left != null) _head = _head.Left.Root;
        }

        private void RemoveHead()
        {
            var hightOfLeftSubTree = _head.Left != null ? _head.Left.Heigh : 0;
            var hightOfRightSubTree = _head.Right != null ? _head.Right.Heigh : 0;
            if (_head.Right == null && _head.Left == null)
            {
                _head = null;
                Count = 0;
                return;
            }

            if (hightOfLeftSubTree > hightOfRightSubTree)
            {
                SubTree<TKey, TValue> movingNode = _head.Left;
                while (movingNode.Right != null)
                {
                    movingNode = movingNode.Right;
                }

                ActionWithNode<TKey, TValue>.ChangeAndDelete(_head, movingNode);
                _head = movingNode;
            }
            else
            {
                SubTree<TKey, TValue> movingNode = _head.Right;
                while (movingNode.Left != null)
                {
                    movingNode = movingNode.Left;
                }
                ActionWithNode<TKey, TValue>.ChangeAndDelete(_head, movingNode);
                _head = movingNode;
            }
            Count--;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (_head.Delete(item.Key))
            {
                Count--;
                return true;
            }
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return _head.Contains(key);
        }



        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = _head[key];
                return true;
            }
            catch (ArgumentNullException)
            {
                value = default(TValue);
                return false;
            }
        }

        public TValue this[TKey key]
        {
            get { return _head[key]; }
            set { _head[key] = value; }
        }

        public void AddRange(KeyValuePair<TKey, TValue>[] elements)
        {
            Array.ForEach(elements, Add);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (TreeTraversal == Traversal.Width)
            {
                return EnumeratorWidthTraversal;
            }
            return EnumeratorDepthTraversal;
        }

        private IEnumerator<KeyValuePair<TKey, TValue>> EnumeratorWidthTraversal
        {
            get
            {
                var queueNodes = new Queue<SubTree<TKey, TValue>>();
                if (_head != null)
                {
                    queueNodes.Enqueue(_head);
                }
                while (queueNodes.Count != 0)
                {
                    var currentElement = queueNodes.Dequeue();
                    if (currentElement.Left != null)
                    {
                        queueNodes.Enqueue(currentElement.Left);
                    }
                    if (currentElement.Right != null)
                    {
                        queueNodes.Enqueue(currentElement.Right);
                    }
                    yield return new KeyValuePair<TKey, TValue>(currentElement.Key, currentElement.Value);
                }
            }
        }

        private IEnumerator<KeyValuePair<TKey, TValue>> EnumeratorDepthTraversal
        {
            get
            {
                var stackNodes = new Stack<SubTree<TKey, TValue>>();
                if (_head != null)
                {
                    stackNodes.Push(_head);
                }
                while (stackNodes.Count != 0)
                {
                    var currentElement = stackNodes.Pop();
                    if (currentElement.Right != null)
                    {
                        stackNodes.Push(currentElement.Right);
                    }
                    if (currentElement.Left != null)
                    {
                        stackNodes.Push(currentElement.Left);
                    }
                    yield return new KeyValuePair<TKey, TValue>(currentElement.Key, currentElement.Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        

        public ICollection<TKey> Keys { get; private set; }
        public ICollection<TValue> Values { get; private set; }
    }
}
