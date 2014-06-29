using System.Collections.Generic;
using System.Linq;

namespace Etimo.Common.Collections.ExtensionMethods
{
    public static class AddRangeExtension
    {
        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            collection.AddRange(items.AsEnumerable());
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is List<T>)
                ((List<T>) collection).AddRange(items);
            else
                foreach (T item in items)
                    collection.Add(item);
        }
    }
}