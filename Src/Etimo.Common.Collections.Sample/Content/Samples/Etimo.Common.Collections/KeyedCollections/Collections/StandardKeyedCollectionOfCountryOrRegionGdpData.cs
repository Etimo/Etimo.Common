using System.Collections.ObjectModel;
using Samples.Etimo.Common.Collections.KeyedCollections.Data;

namespace Samples.Etimo.Common.Collections.KeyedCollections.Collections
{
    public class StandardKeyedCollectionOfCountryOrRegionGdpData : KeyedCollection<string, CountryOrRegionGdpData>
    {
        protected override string GetKeyForItem(CountryOrRegionGdpData item)
        {
            return item.CountryCode;
        }
    }
}