﻿using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Util
{
    public static class RegexExtensions
    {
        public static TResult Parse<TResult>(this string input, Regex regex, Func<string, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value) : default;
        }
        
        public static TResult Parse<TResult, T1>(this string input, Regex regex, Func<string, T1, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value, match.GetValueFromGroup<T1>(1)) : default;
        }

        public static TResult Parse<TResult, T1, T2>(this string input, Regex regex, Func<string, T1, T2, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value
                , match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3>(this string input, Regex regex, Func<string, T1, T2, T3, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value
                , match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3, T4>(this string input, Regex regex, Func<string, T1, T2, T3, T4, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value
                , match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
                , match.GetValueFromGroup<T4>(4)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3, T4, T5>(this string input, Regex regex, Func<string, T1, T2, T3, T4, T5, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value
                , match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
                , match.GetValueFromGroup<T4>(4)
                , match.GetValueFromGroup<T5>(5)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3, T4, T5, T6>(this string input, Regex regex, Func<string, T1, T2, T3, T4, T5, T6, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.Value
                , match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
                , match.GetValueFromGroup<T4>(4)
                , match.GetValueFromGroup<T5>(5)
                , match.GetValueFromGroup<T6>(6)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1>(this string input, Regex regex, Func<T1, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(match.GetValueFromGroup<T1>(1)) : default;
        }

        public static TResult Parse<TResult, T1, T2>(this string input, Regex regex, Func<T1, T2, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(
                match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3>(this string input, Regex regex, Func<T1, T2, T3, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(
                match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3, T4>(this string input, Regex regex, Func<T1, T2, T3, T4, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(
                match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
                , match.GetValueFromGroup<T4>(4)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3, T4, T5>(this string input, Regex regex, Func<T1, T2, T3, T4, T5, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(
                match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
                , match.GetValueFromGroup<T4>(4)
                , match.GetValueFromGroup<T5>(5)
            ) : default;
        }
        
        public static TResult Parse<TResult, T1, T2, T3, T4, T5, T6>(this string input, Regex regex, Func<T1, T2, T3, T4, T5, T6, TResult> parse)
        {
            var match = regex.Match(input);
            return match.Success ? parse(
                match.GetValueFromGroup<T1>(1)
                , match.GetValueFromGroup<T2>(2)
                , match.GetValueFromGroup<T3>(3)
                , match.GetValueFromGroup<T4>(4)
                , match.GetValueFromGroup<T5>(5)
                , match.GetValueFromGroup<T6>(6)
            ) : default;
        }

        private static TResult GetValueFromGroup<TResult>(this Match match, int groupNum)
        {
            return (TResult)Convert.ChangeType(match.Groups[groupNum].Value, typeof(TResult));
        }
    }
}