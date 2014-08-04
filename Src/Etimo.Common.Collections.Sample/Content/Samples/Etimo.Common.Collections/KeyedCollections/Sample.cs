using System;
using System.Collections.Generic;
using System.Linq;
using Samples.Etimo.Common.Collections.KeyedCollections.Collections;
using Samples.Etimo.Common.Collections.KeyedCollections.Data;

namespace Samples.Etimo.Common.Collections.KeyedCollections
{
    public class Sample
    {
        public static void RunSample()
        {
            IEnumerable<CountryOrRegionGdpData> listOfCountryOrRegionGdpData = new DataImporter().Import();

            RunSampleUsingStandardKeyedCollection(listOfCountryOrRegionGdpData);

            RunSampleUsingMultiplyIndexedKeyedCollection(listOfCountryOrRegionGdpData);

            Console.ReadLine();
        }

        private static void RunSampleUsingStandardKeyedCollection(IEnumerable<CountryOrRegionGdpData> listOfCountryOrRegionGdpData)
        {
            StandardKeyedCollectionOfCountryOrRegionGdpData standardKeyedCollectionOfCountryOrRegionGdpData = new StandardKeyedCollectionOfCountryOrRegionGdpData();

            foreach (var item in listOfCountryOrRegionGdpData)
                standardKeyedCollectionOfCountryOrRegionGdpData.Add(item);

            CountryOrRegionGdpData itemByCode = standardKeyedCollectionOfCountryOrRegionGdpData["SWE"];

            CountryOrRegionGdpData itemByName = standardKeyedCollectionOfCountryOrRegionGdpData.Single(q => q.CountryName == "Sweden");

            IEnumerable<CountryOrRegionGdpData> itemsByHasFiveDoubled = standardKeyedCollectionOfCountryOrRegionGdpData.Where(countryOrRegionGdpData => countryOrRegionGdpData.GdpYear2010 >= countryOrRegionGdpData.GdpYear1960 * 5);

            IEnumerable<CountryOrRegionGdpData> itemsByHasTenDoubled = standardKeyedCollectionOfCountryOrRegionGdpData.Where(countryOrRegionGdpData => countryOrRegionGdpData.GdpYear2010 >= countryOrRegionGdpData.GdpYear1960 * 10);

            IEnumerable<CountryOrRegionGdpData> itemsByHasTwentyDoubled = standardKeyedCollectionOfCountryOrRegionGdpData.Where(countryOrRegionGdpData => countryOrRegionGdpData.GdpYear2010 >= countryOrRegionGdpData.GdpYear1960 * 20);

            PrintResults("StandardKeyedCollection", itemByCode, itemByName, itemsByHasFiveDoubled, itemsByHasTenDoubled, itemsByHasTwentyDoubled);
        }

        private static void RunSampleUsingMultiplyIndexedKeyedCollection(IEnumerable<CountryOrRegionGdpData> listOfCountryOrRegionGdpData)
        {
            MultiplyIndexedKeyedCollectionOfCountryOrRegionGdpData multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData = new MultiplyIndexedKeyedCollectionOfCountryOrRegionGdpData();

            foreach (var item in listOfCountryOrRegionGdpData)
                multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData.Add(item);

            CountryOrRegionGdpData itemByCode = multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData.ByCountryCode["SWE"];

            CountryOrRegionGdpData itemByName = multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData.ByCountryName["Sweden"];

            IEnumerable<CountryOrRegionGdpData> itemsByHasFiveDoubled = multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData.ByHasFiveDoubled[true];

            IEnumerable<CountryOrRegionGdpData> itemsByHasTenDoubled = multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData.ByHasTenDoubled[true];

            IEnumerable<CountryOrRegionGdpData> itemsByHasTwentyDoubled = multiplyIndexedKeyedCollectionOfCountryOrRegionGdpData.ByHasTwentyDoubled[true];

            PrintResults("MultiplyIndexedKeyedCollection", itemByCode, itemByName, itemsByHasFiveDoubled, itemsByHasTenDoubled, itemsByHasTwentyDoubled);
        }

        private static void PrintResults(string sampleName, CountryOrRegionGdpData itemByCode, CountryOrRegionGdpData itemByName, IEnumerable<CountryOrRegionGdpData> itemsByHasFiveDoubled, IEnumerable<CountryOrRegionGdpData> itemsByHasTenDoubled, IEnumerable<CountryOrRegionGdpData> itemsByHasTwentyDoubled)
        {
            string output = string.Format(@"
Sample Name: {0}
Item By Code: {1}
Item By Name: {2}
Count of Items By Has Five Doubled: {3}
Count of Items By Has Ten Doubled: {4}
Count of Items By Has Twenty Doubled: {5}", sampleName, itemByCode.CountryCode, itemByName.CountryName, itemsByHasFiveDoubled.Count(), itemsByHasTenDoubled.Count(), itemsByHasTwentyDoubled.Count());

            Console.WriteLine(output);
        }
    }
}