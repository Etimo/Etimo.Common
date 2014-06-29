namespace Etimo.Common.Collections.KeyedCollections
{
    public class ByKeyWrapper<TKey, TValue, TValueContainerOrValue>
    {
        protected readonly IKeyedCollectionBase<TKey, TValue, TValueContainerOrValue> _keyedCollection;

        public ByKeyWrapper(IKeyedCollectionBase<TKey, TValue, TValueContainerOrValue> keyedCollection)
        {
            _keyedCollection = keyedCollection;
        }

        public bool ContainsKey(TKey key)
        {
            return this._keyedCollection.ContainsKey(key);
        }

        public TValueContainerOrValue this[TKey key]
        {
            get { return this._keyedCollection[key]; }
        }
    }
}