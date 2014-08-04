using System.Collections.Generic;
using Etimo.Common.Collections.KeyedCollections;
using Samples.Etimo.Common.Collections.KeyedCollections.Data;

namespace Samples.Etimo.Common.Collections.KeyedCollections.Collections
{
    public class MultiplyIndexedKeyedCollectionOfCountryOrRegionGdpData : MultiplyIndexedKeyedCollectionBase<CountryOrRegionGdpData>
    {
        public readonly IndexedMapping<string, CountryOrRegionGdpData, CountryOrRegionGdpData> ByCountryCode;
        public readonly IndexedMapping<string, CountryOrRegionGdpData, CountryOrRegionGdpData> ByCountryName;
        public readonly IndexedMapping<bool, CountryOrRegionGdpData, IEnumerable<CountryOrRegionGdpData>> ByHasFiveDoubled;
        public readonly IndexedMapping<bool, CountryOrRegionGdpData, IEnumerable<CountryOrRegionGdpData>> ByHasTenDoubled;
        public readonly IndexedMapping<bool, CountryOrRegionGdpData, IEnumerable<CountryOrRegionGdpData>> ByHasTwentyDoubled;

        public MultiplyIndexedKeyedCollectionOfCountryOrRegionGdpData()
        {
            // Here we do not explicitly specify any equality comparer, so a default equality comparer will be used
            this.ByCountryCode = CreateAndRegisterIndexedOneToOneMapping(countryOrRegionGdpData => countryOrRegionGdpData.CountryCode);

            // When specifying null as equality comparer, a default equality comparer will be used (just like the previous case)
            this.ByCountryName = CreateAndRegisterIndexedOneToOneMapping(countryOrRegionGdpData => countryOrRegionGdpData.CountryName, null);

            // It is also possible to explicitly specify an equality comparer
            this.ByHasFiveDoubled = CreateAndRegisterIndexedOneToManyMapping(countryOrRegionGdpData => countryOrRegionGdpData.GdpYear2010 >= countryOrRegionGdpData.GdpYear1960 * 5, EqualityComparer<bool>.Default);
            this.ByHasTenDoubled = CreateAndRegisterIndexedOneToManyMapping(countryOrRegionGdpData => countryOrRegionGdpData.GdpYear2010 >= countryOrRegionGdpData.GdpYear1960 * 10, EqualityComparer<bool>.Default);
            this.ByHasTwentyDoubled = CreateAndRegisterIndexedOneToManyMapping(countryOrRegionGdpData => countryOrRegionGdpData.GdpYear2010 >= countryOrRegionGdpData.GdpYear1960 * 20, EqualityComparer<bool>.Default);
        }
    }
}