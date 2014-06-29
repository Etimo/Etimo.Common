using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Etimo.Common.Collections.KeyedCollections
{
    public class MultiValueDelegatedKeyedCollection<TKey, TValue> : IMultiValueKeyedCollection<TKey, TValue, HashSet<TValue>>
    {
        private readonly Func<TValue, TKey> _getKeyForItemDelegate;
        private readonly DelegatedKeyedCollection<TKey, HashSet<TValue>> _delegatedKeyedCollection;

        public MultiValueDelegatedKeyedCollection(Func<TValue, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> equalityComparer)
        {
            _getKeyForItemDelegate = getKeyForItemDelegate;
            this._delegatedKeyedCollection = new DelegatedKeyedCollection<TKey, HashSet<TValue>>(set => getKeyForItemDelegate(set.First()), equalityComparer);
        }

        public void Add(TValue item)
        {
            TKey key = this._getKeyForItemDelegate(item);
            bool containerExists = this._delegatedKeyedCollection.Contains(key);

            HashSet<TValue> container = containerExists ?
                                            this._delegatedKeyedCollection[key]
                                            : new HashSet<TValue>();

            container.Add(item);

            if (!containerExists)
                this._delegatedKeyedCollection.Add(container);
        }

        public void Clear()
        {
            this._delegatedKeyedCollection.Clear();
        }

        public bool Contains(TValue item)
        {
            TKey key = this._getKeyForItemDelegate(item);
            return this._delegatedKeyedCollection.Contains(key);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            int count = this.Count;

            if (count == 0)
                return;

            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (arrayIndex + count > array.Length)
                throw new ArgumentException();

            int index = arrayIndex;
            //, i = 0;
            foreach (TValue item in this)
                array[index++] = item;
            //{
            //    //if (i == count)
            //    //    break;

            //    index++;
            //    //i++;
            //}
        }

        public bool Remove(TValue item)
        {
            TKey key = this._getKeyForItemDelegate(item);

            if (!this._delegatedKeyedCollection.Contains(key))
                return false;

            HashSet<TValue> container = this._delegatedKeyedCollection[key];

            if (!container.Contains(item))
                return false;

            if (container.Count == 1)
                this._delegatedKeyedCollection.Remove(container);

            return container.Remove(item);
        }

        public int Count { get { return this._delegatedKeyedCollection.Sum(set => set.Count); } }
        public bool IsReadOnly { get { return false; } }

        public IEnumerator<TValue> GetEnumerator()
        {
            return this._delegatedKeyedCollection.SelectMany(set => set.AsEnumerable()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public HashSet<TValue> this[TKey key]
        {
            get
            {
                return this._delegatedKeyedCollection[key];
            }
        }

        public bool ContainsKey(TKey key)
        {
            return this._delegatedKeyedCollection.ContainsKey(key);
        }
    }
}