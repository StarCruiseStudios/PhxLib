// -----------------------------------------------------------------------------
//  <copyright file="IEnumerableCopyExtensions.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using static Phx.Collections.PhxCollections;

    /// <summary>
    ///     Contains extension methods to copy values to different collection types.
    /// </summary>
    public static class IEnumerableCopyExtensions {
        /// <summary>
        ///     Copies the given collection to an <see cref="IReadOnlyCollection{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IReadOnlyCollection{T}"/> instance. </returns>
        public static IReadOnlyCollection<T> CopyToReadOnlyCollection<T>(this IEnumerable<T> collection) {
            return CopyToReadOnlyList(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IReadOnlyList{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IReadOnlyList{T}"/> instance. </returns>
        public static IReadOnlyList<T> CopyToReadOnlyList<T>(this IEnumerable<T> collection) {
            return new ReadOnlyCollection<T>(new List<T>(collection));
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="ICollection{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="ICollection{T}"/> instance. </returns>
        public static ICollection<T> CopyToCollection<T>(this IEnumerable<T> collection) {
            return CopyToList(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IList{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IList{T}"/> instance. </returns>
        public static IList<T> CopyToList<T>(this IEnumerable<T> collection) {
            return new List<T>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="ISet{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="ISet{T}"/> instance. </returns>
        public static ISet<T> CopyToSet<T>(this IEnumerable<T> collection) {
            return new HashSet<T>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IDictionary{TKey, TValue}"/> instance.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IDictionary{TKey, TValue}"/> instance. </returns>
        public static IDictionary<TKey, TValue> CopyToDictionary<TKey, TValue>(this IPhxMap<TKey, TValue> collection) {
            var dict = new Dictionary<TKey, TValue>();
            foreach (var entry in collection.Entries) {
                dict.Add(entry.Key, entry.Value);
            }

            return dict;
        }

        public static KeyValuePair<TKey, TValue> CopyToKeyValuePair<TKey, TValue>(this IPhxPair<TKey, TValue> pair) {
            return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IPhxCollection{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxCollection{T}"/> instance. </returns>
        public static IPhxCollection<T> CopyToPhxCollection<T>(this IEnumerable<T> collection) {
            return CopyToPhxList(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IMutablePhxCollection{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IMutablePhxCollection{T}"/> instance. </returns>
        public static IMutablePhxCollection<T> CopyToMutablePhxCollection<T>(this IEnumerable<T> collection) {
            return CopyToMutablePhxList(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IPhxList{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxList{T}"/> instance. </returns>
        public static IPhxList<T> CopyToPhxList<T>(this IEnumerable<T> collection) {
            return new ImmutablePhxList<T>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IMutablePhxList{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IMutablePhxList{T}"/> instance. </returns>
        public static IMutablePhxList<T> CopyToMutablePhxList<T>(this IEnumerable<T> collection) {
            return MutableListOf<T>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IPhxSet{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="throwOnDuplicates"> Indicates whether an exception should be thrown if the given collection
        ///                                  contains duplicates that will be lost on copy. </param>
        /// <returns> A newly constructed <see cref="IPhxSet{T}"/> instance. </returns>
        /// <exception cref="ArgumentException"> thrown if <paramref name="throwOnDuplicates"/> is <c>true</c> and the
        ///                                      given collection contains duplicate values that were lost when copying
        ///                                      to a set. </exception>
        public static IPhxSet<T> CopyToPhxSet<T>(this IEnumerable<T> collection, bool throwOnDuplicates = false) {
            var newSet = SetOf(collection);
            if (throwOnDuplicates && newSet.Count != collection.Count()) {
                throw new ArgumentException("Given collection contained duplicates that were lost on copy.");
            }
            return newSet;
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IMutablePhxSet{T}"/> instance.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="throwOnDuplicates"> Indicates whether an exception should be thrown if the given collection
        ///                                  contains duplicates that will be lost on copy. </param>
        /// <returns> A newly constructed <see cref="IMutablePhxSet{T}"/> instance. </returns>
        /// <exception cref="ArgumentException"> thrown if <paramref name="throwOnDuplicates"/> is <c>true</c> and the
        ///                                      given collection contains duplicate values that were lost when copying
        ///                                      to a set. </exception>
        public static IMutablePhxSet<T> CopyToMutablePhxSet<T>(
            this IEnumerable<T> collection,
            bool throwOnDuplicates = false
        ) {
            var newSet = MutableSetOf(collection);
            if (throwOnDuplicates && newSet.Count != collection.Count()) {
                throw new ArgumentException("Given collection contained duplicates that were lost on copy.");
            }
            return newSet;
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IPhxMap{TKey, TValue}"/> instance.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMap{TKey, TValue}"/> instance. </returns>
        public static IPhxMap<TKey, TValue> CopyToPhxMap<TKey, TValue>(this IPhxMap<TKey, TValue> collection) {
            return new ImmutablePhxMap<TKey, TValue>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IPhxMap{TKey, TValue}"/> instance.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMap{TKey, TValue}"/> instance. </returns>
        public static IPhxMap<TKey, TValue> CopyToPhxMap<TKey, TValue>(this IDictionary<TKey, TValue> collection) {
            return new ImmutablePhxMap<TKey, TValue>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IMutablePhxMap{TKey, TValue}"/> instance.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IMutablePhxMap{TKey, TValue}"/> instance. </returns>
        public static IMutablePhxMap<TKey, TValue> CopyToMutablePhxMap<TKey, TValue>(
            this IPhxMap<TKey, TValue> collection
        ) {
            return new PhxHashMap<TKey, TValue>(collection);
        }

        /// <summary>
        ///     Copies the given collection to an <see cref="IMutablePhxMap{TKey, TValue}"/> instance.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IMutablePhxMap{TKey, TValue}"/> instance. </returns>
        public static IMutablePhxMap<TKey, TValue> CopyToMutablePhxMap<TKey, TValue>(
            this IDictionary<TKey, TValue> collection
        ) {
            return new PhxHashMap<TKey, TValue>(collection);
        }
    }
}
