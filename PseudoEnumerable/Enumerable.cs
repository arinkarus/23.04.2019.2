﻿using System;
using System.Collections;
using System.Collections.Generic;
using PseudoLINQ;

namespace PseudoEnumerable
{
    /// <summary>
    /// Enumerable extension class.
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">An <see cref="IEnumerable{TSource}"/> to filter.</param>
        /// <param name="predicate">A function to test each source element for a condition</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> that contains elements from the input
        ///     sequence that satisfy the condition.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            CheckOnNull(source);
            CheckOnNull(predicate);

            IEnumerable<TSource> GetFilteredItems()
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }

            return GetFilteredItems();
        }

        /// <summary>
        /// Transforms each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by transformer.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="transformer">A transform function to apply to each source element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TResult}"/> whose elements are the result of
        ///     invoking the transform function on each element of source.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="transformer"/> is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            CheckOnNull(source);
            CheckOnNull(transformer);

            IEnumerable<TResult> GetTransformedItems()
            {
                foreach (var item in source)
                {
                    yield return transformer(item);
                }
            }

            return GetTransformedItems();
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            CheckOnNull(source);
            CheckOnNull(key);
            return SortBy(source, key, null, true);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
             Func<TSource, TKey> key)
        {
            CheckOnNull(source);
            CheckOnNull(key);
            return SortBy(source, key, null, false);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according by using a specified comparer for a key .
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source,
             Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            CheckOnNull(source);
            CheckOnNull(key);
            return SortBy(source, key, comparer, false);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according by using a specified comparer for a key .
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            CheckOnNull(source);
            CheckOnNull(key);
            return SortBy(source, key, comparer, true);
        }

        /// <summary>
        /// Casts the elements of an IEnumerable to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <param name="source">The <see cref="IEnumerable"/> that contains the elements to be cast to type TResult.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains each element of the source sequence cast to the specified type.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidCastException">An element in the sequence cannot be cast to type TResult.</exception>
        public static IEnumerable<TResult> CastTo<TResult>(IEnumerable source)
        {
            CheckOnNull(source);

            if (source is IEnumerable<TResult> resultSource)
            {
                return resultSource;
            }

            IEnumerable<TResult> GetCastedValues()
            {
                foreach (var item in source)
                {
                    yield return (TResult)item;
                }
            }

            return GetCastedValues();
        }

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     true if every element of the source sequence passes the test in the specified predicate,
        ///     or if the sequence is empty; otherwise, false
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static bool ForAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckOnNull(source);
            CheckOnNull(predicate);

            foreach(var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<int> GenerateNumbers(int start, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentNullException($"Can't be a negative number: {nameof(count)}");
            }

            IEnumerable<int> GetNumbers()
            {
                for (int i = start; i < start + count; i++)
                {
                    yield return i;
                }
            }

            return GetNumbers();
        }

        private class ComparerAdapter<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly IComparer<TKey> comparer;

            public ComparerAdapter(IComparer<TKey> comparer)
            {
                this.comparer = comparer;
            }

            public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                return comparer.Compare(x.Key, y.Key);
            }
        }

        private class ComparerDesc<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly IComparer<KeyValuePair<TKey, TValue>> comparer;

            public ComparerDesc(IComparer<KeyValuePair<TKey, TValue>> comparer)
            {
                this.comparer = comparer;
            }

            public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                return -this.comparer.Compare(x, y);
            }
        }

        private static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key, IComparer<TKey> comparer, bool isAscOrdering)
        {
            if (comparer == null)
            {
                if (!(typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)) ||
                 typeof(IComparable).IsAssignableFrom(typeof(TKey))))
                {
                    throw new ComparerNotFound($"Default comparer is not found for {nameof(TKey)}");
                }

                comparer = Comparer<TKey>.Default;
            }

            IComparer<KeyValuePair<TKey, TSource>> keyComparer = new ComparerAdapter<TKey, TSource>(comparer);
            if (!isAscOrdering)
            {
                keyComparer = new ComparerDesc<TKey, TSource>(keyComparer);
            }

            IEnumerable<TSource> GetSorted()
            {
                var list = new List<KeyValuePair<TKey, TSource>>();
                foreach (var element in source)
                {
                    list.Add(new KeyValuePair<TKey, TSource>(key(element), element));
                }

                list.Sort(keyComparer);
                foreach (var item in list)
                {
                    yield return item.Value;
                }
            }

            return GetSorted();
        }

        private static void CheckOnNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} can't be null.");
            }
        }

    }
}