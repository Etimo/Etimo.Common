using System;
using System.Collections.Generic;

namespace Etimo.Common.Collections.Comparers
{
    public class DelegatedDefaultEqualityComparer<T, TDelegated> : IEqualityComparer<T>
    {
        private readonly Func<T, TDelegated> _objectToCompareSelector;
        private readonly IEqualityComparer<TDelegated> _delegatedEqualityComparer;

        public DelegatedDefaultEqualityComparer(Func<T, TDelegated> objectToCompareSelector)
        {
            _objectToCompareSelector = objectToCompareSelector;
            _delegatedEqualityComparer = EqualityComparer<TDelegated>.Default;
        }

        public bool Equals(T x, T y)
        {
            return _delegatedEqualityComparer.Equals(_objectToCompareSelector(x), _objectToCompareSelector(y));
        }

        public int GetHashCode(T obj)
        {
            return _delegatedEqualityComparer.GetHashCode(_objectToCompareSelector(obj));
        }
    }
}