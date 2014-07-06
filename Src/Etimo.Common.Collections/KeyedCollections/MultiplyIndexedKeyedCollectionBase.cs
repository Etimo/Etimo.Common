using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Etimo.Common.Collections.KeyedCollections
{
    public class MultiplyIndexedKeyedCollectionBase<TValue> : ICollection<TValue>
    {
        readonly HashSet<TValue> _valueSet = new HashSet<TValue>();
        readonly HashSet<ICollection<TValue>> _mappings = new HashSet<ICollection<TValue>>();

        protected IndexedMapping<TKey, TValue, TValue> CreateAndRegisterIndexedOneToOneMapping<TKey>(Func<TValue, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> equalityComparer)
        {
            DelegatedKeyedCollection<TKey, TValue> delegatedKeyedCollection = new DelegatedKeyedCollection<TKey, TValue>(getKeyForItemDelegate, equalityComparer);
            this._mappings.Add(delegatedKeyedCollection);

            return new IndexedMapping<TKey, TValue, TValue>(delegatedKeyedCollection);
        }

        protected IndexedMapping<TKey, TValue, IEnumerable<TValue>> CreateAndRegisterIndexedOneToManyMapping<TKey>(Func<TValue, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> equalityComparer)
        {
            MultiValueDelegatedKeyedCollection<TKey, TValue> multiValueDelegatedKeyedCollection = new MultiValueDelegatedKeyedCollection<TKey, TValue>(getKeyForItemDelegate, equalityComparer);
            this._mappings.Add(multiValueDelegatedKeyedCollection);

            return new IndexedMapping<TKey, TValue, IEnumerable<TValue>>(multiValueDelegatedKeyedCollection);
        }

        private IEnumerable<ICollection<TValue>> EnumerateAllCollections()
        {
            return new[] { this._valueSet }
                .Union(this._mappings.AsEnumerable());
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return this._valueSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TValue item)
        {
            foreach (ICollection<TValue> collection in this.EnumerateAllCollections())
                collection.Add(item);
        }

        public void Clear()
        {
            foreach (ICollection<TValue> collection in this.EnumerateAllCollections())
                collection.Clear();
        }

        public bool Contains(TValue item)
        {
            return this._valueSet.Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            this._valueSet.CopyTo(array, arrayIndex);
        }

        public bool Remove(TValue item)
        {
            foreach (ICollection<TValue> collection in this.EnumerateAllCollections().Except(new[] { this._valueSet }))
                collection.Remove(item);

            return this._valueSet.Remove(item);
        }

        public int Count { get { return this._valueSet.Count; } }
        public bool IsReadOnly { get { return ((ICollection<TValue>)this._valueSet).IsReadOnly; } }
    }
}
