using System.Collections.Generic;
using System.Linq;

namespace Etimo.Common.Collections.Comparers
{
    public class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.SequenceEqual(y, EqualityComparer<T>.Default);
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            return obj.Aggregate(0, (total, next) => total ^ next.GetHashCode());
        }
    }
}