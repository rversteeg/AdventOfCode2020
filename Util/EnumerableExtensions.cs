﻿using System;
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

        public static IEnumerator<int> GetEnumerator(this Range range)
            => Enumerable.Range(range.Start.Value, range.End.Value - range.Start.Value).GetEnumerator();

        public static IEnumerable<TResult> Select<TResult>(this Range range, Func<int, TResult> selectFun)
            => Enumerable.Range(range.Start.Value, range.End.Value - range.Start.Value).Select(selectFun);
        
        public static IEnumerable<TResult> Select<TResult>(this Range range, Func<int, int, TResult> selectFun)
            => Enumerable.Range(range.Start.Value, range.End.Value - range.Start.Value).Select(selectFun);
    }
}