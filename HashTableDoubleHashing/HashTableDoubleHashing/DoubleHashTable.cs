using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableDoubleHashing
{
    public class DoubleHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public int Count { get; private set; }
        public ISizeTable SizeTable { get; set; }

        protected PairHashValue<KeyValuePair<TKey, TValue>>[] _elements;


        public DoubleHashTable(ISizeTable sizeTable)
        {
            SizeTable = sizeTable;
            _elements = new PairHashValue<KeyValuePair<TKey, TValue>>[sizeTable.Size];
        }

        public void Add(TKey key, TValue value)
        {
            if (Count == SizeTable.Size)
            {
                RefreshTable();
            }
            int hashCode = key.GetHashCode();
            int index = SizeTable.GetHashValue(hashCode);
            for (int i = 0; i < SizeTable.Size; i++)
            {
                if(_elements[index]!= null)
                    if(_elements[index].Value.Key.Equals(key))
                        return;

                if (_elements[index] == null || _elements[index].IsDeleted)
                {
                    _elements[index] =
                        new PairHashValue<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
                    Count++;
                    return;
                }
                index = SizeTable.GetHashValue(hashCode);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                var value = GetElement(key).Value.Value;
                if (value != null)
                {
                    return value;
                }
                throw new KeyNotFoundException("Key if not found");
            }
        }

        private PairHashValue<KeyValuePair<TKey, TValue>> GetElement(TKey key)
        {
            int hashCode = key.GetHashCode();
            int index = SizeTable.GetHashValue(hashCode);
            
            for (int i = 0; i < SizeTable.Size; i++)
            {
                if (_elements[index] != null)
                {
                    if (_elements[index].Value.Key.Equals(key) && !_elements[index].IsDeleted)
                    {
                        return _elements[index];
                    }
                }
                index = SizeTable.GetHashValue(hashCode);
            }
            return null;
        }

        public bool Contains(TKey key)
        {
            return GetElement(key) != null;
        }

        public void Remove(TKey key)
        {
            var element = GetElement(key);
            if (element != null)
            {
                element.IsDeleted = true;
                Count--;
            }
        }
        private void RefreshTable()
        {
            SizeTable.IncriseSize();
            var temporaryArray = _elements;
            _elements = new PairHashValue<KeyValuePair<TKey, TValue>>[SizeTable.Size];
            var hashTable = new DoubleHashTable<TKey, TValue>(new SizeHashTable(SizeTable.Size));
            foreach (var pair in temporaryArray)
            {
                if (pair != null)
                {
                    hashTable.Add(pair.Value.Key, pair.Value.Value);
                }
            }
            _elements = hashTable._elements;
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < SizeTable.Size; i++)
            {
                if (_elements[i] != null)
                    yield return _elements[i].Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }
}
