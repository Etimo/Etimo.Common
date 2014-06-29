using System.Collections.Generic;

namespace Etimo.Common.Collections.KeyedCollections
{
    public interface IKeyedCollectionBase<in TKey, TValue, out TValueContainerOrValue> : ICollection<TValue>
    {
        TValueContainerOrValue this[TKey key] { get; }
        bool ContainsKey(TKey key);
    }
}