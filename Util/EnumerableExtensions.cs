using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Util
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> AllExcept<T>(this IEnumerable<T> collection, IEnumerable<T> other)
        {
            var hashSet = other.ToHashSet();
            return collection.Where(x => !hashSet.Contains(x));
        }
    }
}