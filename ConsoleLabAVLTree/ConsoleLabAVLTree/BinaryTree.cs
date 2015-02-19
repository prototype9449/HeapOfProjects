using System;
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

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (_head == null)
            {
                _head = new SubTree<TKey, TValue>(item.Key,item.Value);
            }
            else
            {
                if (_head.Add(item.Key, item.Value))
                {
                    Count++;
                    while (_head.Root != null) _head = _head.Root;
                }
            }
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

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (_head.Delete(item.Key))
            {
                Count--;
                return true;
            }
            return false;
        }


        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public Traversal TreeTraversal { get; set; }
        

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

        public bool ContainsKey(TKey key)
        {
            return _head.Contains(key);
        }

        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool Remove(TKey key)
        {
            if (_head.Delete(key))
            {
                Count--;
                while (_head.Root != null) _head = _head.Root;
                if (_head.Right != null) _head = _head.Right.Root;
                if (_head.Left != null) _head = _head.Left.Root;
                return true;
            }
            return false;
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

        public ICollection<TKey> Keys { get; private set; }
        public ICollection<TValue> Values { get; private set; }
    }
}
