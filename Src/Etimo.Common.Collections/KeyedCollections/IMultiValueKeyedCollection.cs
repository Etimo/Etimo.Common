namespace Etimo.Common.Collections.KeyedCollections
{
    public interface IMultiValueKeyedCollection<in TKey, TValue, out TValueContainer> : IKeyedCollectionBase<TKey, TValue, TValueContainer>
    {
    }
}