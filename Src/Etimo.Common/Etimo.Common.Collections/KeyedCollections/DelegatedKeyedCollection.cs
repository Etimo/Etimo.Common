using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Etimo.Common.Collections.KeyedCollections
{
    public class DelegatedKeyedCollection<TKey, TValue> : KeyedCollection<TKey, TValue>, IKeyedCollection<TKey, TValue>
    {
        private readonly Func<TValue, TKey> _getKeyForItemDelegate;

        public DelegatedKeyedCollection(Func<TValue, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> equalityComparer) : base(equalityComparer)
        {
            _getKeyForItemDelegate = getKeyForItemDelegate;
        }

        protected override TKey GetKeyForItem(TValue item)
        {
            return _getKeyForItemDelegate(item);
        }

        public bool ContainsKey(TKey key)
        {
            return Contains(key);
        }
    }
}