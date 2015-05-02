using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleAppListSkip
{
    public class Node<TKey, TValue> : IEquatable<Node<TKey, TValue>>, IComparable<Node<TKey, TValue>> where TKey : IComparable<TKey>, new()
    {
        public Node<TKey, TValue> Up { get; set; }
        public Node<TKey, TValue> Down { get; set; }
        public Node<TKey, TValue> Right { get; set; }

        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public Node()
        {
            Key = default(TKey);
            Value = default(TValue);
        }

        public Node(Node<TKey, TValue> otherNode)
        {
            Key = otherNode.Key;
            Value = otherNode.Value;
        }

        public static bool operator <(Node<TKey, TValue> first, Node<TKey,TValue> second)
        {
            return first.CompareTo(second) == -1;

        }

        public static bool operator >(Node<TKey, TValue> first, Node<TKey, TValue> second)
        {
            return first.CompareTo(second) == 1;
        }

        public int CompareTo(Node<TKey, TValue> other)
        {
            return Key.CompareTo(other.Key);
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node<TKey, TValue>;
            if (node == null) return false;
            return Equals(node);
        }

        public bool Equals(Node<TKey, TValue> other)
        {
            return Key.Equals(other.Key);
        }


    }
}
