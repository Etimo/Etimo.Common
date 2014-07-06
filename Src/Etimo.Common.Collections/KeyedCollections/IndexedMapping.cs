namespace Etimo.Common.Collections.KeyedCollections
{
    public class IndexedMapping<TKey, TValue, TValueContainerOrValue>
    {
        protected readonly IKeyedCollectionBase<TKey, TValue, TValueContainerOrValue> KeyedCollection;

        public IndexedMapping(IKeyedCollectionBase<TKey, TValue, TValueContainerOrValue> keyedCollection)
        {
            this.KeyedCollection = keyedCollection;
        }

        public bool ContainsKey(TKey key)
        {
            return this.KeyedCollection.ContainsKey(key);
        }

        public TValueContainerOrValue this[TKey key]
        {
            get { return this.KeyedCollection[key]; }
        }
    }
}