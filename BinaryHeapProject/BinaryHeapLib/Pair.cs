using System;

namespace BinaryHeapLib
{
    internal struct Pair<TKey, TValue> : IEquatable<Pair<TKey, TValue>> where TKey : IComparable<TKey> where TValue : IEquatable<TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Pair(TKey key, TValue value) : this()
        {
            Key = key;
            Value = value;
        }

        public static bool operator >(Pair<TKey, TValue> firstPair, Pair<TKey, TValue> secondPair)
        {
            return firstPair.Key.CompareTo(secondPair.Key) == 1;
        }

        public static bool operator <(Pair<TKey, TValue> firstPair, Pair<TKey, TValue> secondPair)
        {
            return firstPair.Key.CompareTo(secondPair.Key) ==-1;
        }

        public static bool operator ==(Pair<TKey, TValue> firstPair, Pair<TKey, TValue> secondPair)
        {
            return firstPair.Equals(secondPair);
        }

        public static bool operator !=(Pair<TKey, TValue> firstPair, Pair<TKey, TValue> secondPair)
        {
            return !firstPair.Equals(secondPair);
        }

        public bool Equals(Pair<TKey, TValue> other)
        {
            return other.Key.CompareTo(Key)==0 && Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode()/357 + Value.GetHashCode()/357;
        }
    }
}
