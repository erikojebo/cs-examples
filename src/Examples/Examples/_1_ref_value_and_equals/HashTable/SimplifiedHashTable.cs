using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.HashTable
{
    public class SimplifiedHashTable<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>>[] _values;
        private readonly int _size;

        public SimplifiedHashTable(int size)
        {
            _size = size;
            _values = new List<KeyValuePair<TKey, TValue>>[size];
        }

        private int GetIndex(TKey key)
        {
            var hashCode = key.GetHashCode();
            var index = hashCode % _size;
            return index;
        }
        
        public TValue this[TKey key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                SetValue(key, value);
            }
        }

        private void SetValue(TKey key, TValue value)
        {
            var index = GetIndex(key);

            if (_values[index] == null)
                _values[index] = new List<KeyValuePair<TKey, TValue>>();

            var pairsWithMatchingHash = _values[index];

            RemovePair(pairsWithMatchingHash, key);

            _values[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        private void RemovePair(List<KeyValuePair<TKey, TValue>> pairsWithMatchingHash, TKey key)
        {
            for (int i = 0; i < pairsWithMatchingHash.Count; i++)
            {
                if (Equals(key, pairsWithMatchingHash[i].Key))
                {
                    pairsWithMatchingHash.RemoveAt(i);
                    return;
                }
            }
        }

        public TValue GetValue(TKey key)
        {
            var index = GetIndex(key);

            var pairsWithMatchingHash = _values[index];

            if (pairsWithMatchingHash != null)
            {
                foreach (var keyValuePair in pairsWithMatchingHash)
                {
                    if (Equals(key, keyValuePair.Key))
                        return keyValuePair.Value;
                }    
            }

            throw new KeyNotFoundException();
        }
    }
}