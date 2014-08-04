using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace Samples.Etimo.Common.Collections.KeyedCollections.Data
{
    public class DataImporter
    {
        public IList<CountryOrRegionGdpData> Import()
        {
            DirectoryInfo directoryInfoExecutingAssembly = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent.Parent;
            FileInfo fileInfoData = directoryInfoExecutingAssembly.EnumerateFiles("The World Bank - GDP Per Capita.csv", System.IO.SearchOption.AllDirectories).First();

            return ParseDataFile(fileInfoData);
        }

        private static IList<CountryOrRegionGdpData> ParseDataFile(FileInfo fileInfoData)
        {
            IList<CountryOrRegionGdpData> toReturn = new List<CountryOrRegionGdpData>();

            using (TextFieldParser parser = new TextFieldParser(fileInfoData.FullName))
            {
                parser.SetDelimiters("\t");

                // Skip first line since it only contains headers
                parser.ReadLine();

                NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = ".",
                };

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    toReturn.Add(new CountryOrRegionGdpData()
                    {
                        CountryCode = fields[3],
                        CountryName = fields[2],
                        GdpYear1960 = string.IsNullOrWhiteSpace(fields[5]) ? (decimal?)null : Convert.ToDecimal(fields[5], numberFormatInfo),
                        GdpYear1970 = string.IsNullOrWhiteSpace(fields[15]) ? (decimal?)null : Convert.ToDecimal(fields[15], numberFormatInfo),
                        GdpYear1980 = string.IsNullOrWhiteSpace(fields[25]) ? (decimal?)null : Convert.ToDecimal(fields[25], numberFormatInfo),
                        GdpYear1990 = string.IsNullOrWhiteSpace(fields[35]) ? (decimal?)null : Convert.ToDecimal(fields[35], numberFormatInfo),
                        GdpYear2000 = string.IsNullOrWhiteSpace(fields[45]) ? (decimal?)null : Convert.ToDecimal(fields[45], numberFormatInfo),
                        GdpYear2010 = string.IsNullOrWhiteSpace(fields[55]) ? (decimal?)null : Convert.ToDecimal(fields[55], numberFormatInfo),
                    });
                }
            }

            return toReturn;
        }
    }
}
