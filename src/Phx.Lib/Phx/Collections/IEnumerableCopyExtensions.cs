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
    using Phx.Lang;
    using static PhxCollections;

    /// <summary> Contains extension methods to copy values to different collection types. </summary>
    public static class IEnumerableCopyExtensions {
        /// <summary> Copies the given collection to an <see cref="IReadOnlyCollection{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IReadOnlyCollection{T}" /> instance. </returns>
        public static IReadOnlyCollection<T> CopyToReadOnlyCollection<T>(this IEnumerable<T> collection) {
            return CopyToReadOnlyList(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IReadOnlyList{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IReadOnlyList{T}" /> instance. </returns>
        public static IReadOnlyList<T> CopyToReadOnlyList<T>(this IEnumerable<T> collection) {
            return new ReadOnlyCollection<T>(new List<T>(collection));
        }

        /// <summary> Copies the given collection to an <see cref="ICollection{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="ICollection{T}" /> instance. </returns>
        public static ICollection<T> CopyToCollection<T>(this IEnumerable<T> collection) {
            return CopyToList(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IList{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IList{T}" /> instance. </returns>
        public static IList<T> CopyToList<T>(this IEnumerable<T> collection) {
            return new List<T>(collection);
        }

        /// <summary> Copies the given collection to an <see cref="ISet{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="ISet{T}" /> instance. </returns>
        public static ISet<T> CopyToSet<T>(this IEnumerable<T> collection) {
            return new HashSet<T>(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IDictionary{TKey, TValue}" /> instance. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IDictionary{TKey, TValue}" /> instance. </returns>
        public static IDictionary<TKey, TValue> CopyToDictionary<TKey, TValue>(this IPhxMap<TKey, TValue> collection) {
            var dict = new Dictionary<TKey, TValue>();
            foreach (var entry in collection.Entries) {
                dict.Add(entry.Key, entry.Value);
            }

            return dict;
        }

        /// <summary> Copies the given key value pair to an <see cref="KeyValuePair{TKey, TValue}" /> instance. </summary>
        /// <typeparam name="TKey"> The type of the key. </typeparam>
        /// <typeparam name="TValue"> The type of the Value. </typeparam>
        /// <param name="keyValuePair"> The pair to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="KeyValuePair{TKey, TValue}" /> instance. </returns>
        public static KeyValuePair<TKey, TValue> CopyToKeyValuePair<TKey, TValue>(
                this IPhxKeyValuePair<TKey, TValue> keyValuePair) {
            return new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
        }

        /// <summary>
        ///     Copies the given key value pair to an <see cref="IPhxKeyValuePair{TKey, TValue}" />
        ///     instance.
        /// </summary>
        /// <typeparam name="TKey"> The type of the key. </typeparam>
        /// <typeparam name="TValue"> The type of the Value. </typeparam>
        /// <param name="keyValuePair"> The pair to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxKeyValuePair{TKey, TValue}" /> instance. </returns>
        public static IPhxKeyValuePair<TKey, TValue> CopyToKeyValuePair<TKey, TValue>(
                this KeyValuePair<TKey, TValue> keyValuePair) {
            return new PhxKeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxCollection{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxCollection{T}" /> instance. </returns>
        public static IPhxCollection<T> CopyToPhxCollection<T>(this IEnumerable<T> collection) {
            return CopyToPhxList(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMutableCollection{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMutableCollection{T}" /> instance. </returns>
        public static IPhxMutableCollection<T> CopyToMutablePhxCollection<T>(this IEnumerable<T> collection) {
            return CopyToMutablePhxList(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxList{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxList{T}" /> instance. </returns>
        public static IPhxList<T> CopyToPhxList<T>(this IEnumerable<T> collection) {
            return new ImmutablePhxList<T>(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMutableList{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMutableList{T}" /> instance. </returns>
        public static IPhxMutableList<T> CopyToMutablePhxList<T>(this IEnumerable<T> collection) {
            return MutableListOf<T>(collection);
        }
        
        /// <summary> Returns a new list that is composed of the elements in original list and the given new elements. </summary>
        /// <typeparam name="T"> The type of elements contained in the list. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="elements"> The new elements to add to the list. </param>
        /// <returns> A mutable list containing all of the given elements. </returns>
        public static IPhxMutableList<T> JoinedTo<T>(this IEnumerable<T> collection, params T[] elements) {
            var newList = collection.CopyToMutablePhxList();
            newList.AddAll(elements);
            return newList;
        }

        /// <summary> Copies the given collection to an <see cref="IPhxSet{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="throwOnDuplicates">
        ///     Indicates whether an exception should be thrown if the given
        ///     collection contains duplicates that will be lost on copy.
        /// </param>
        /// <returns> A newly constructed <see cref="IPhxSet{T}" /> instance. </returns>
        /// <exception cref="ArgumentException">
        ///     thrown if <paramref name="throwOnDuplicates" /> is
        ///     <c> true </c> and the given collection contains duplicate values that were lost when copying to
        ///     a set.
        /// </exception>
        public static IPhxSet<T> CopyToPhxSet<T>(this IEnumerable<T> collection, bool throwOnDuplicates = false) {
            var newSet = SetOf(collection);
            if (throwOnDuplicates && newSet.Count != collection.Count()) {
                throw new ArgumentException("Given collection contained duplicates that were lost on copy.");
            }

            return newSet;
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMutableSet{T}" /> instance. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="throwOnDuplicates">
        ///     Indicates whether an exception should be thrown if the given
        ///     collection contains duplicates that will be lost on copy.
        /// </param>
        /// <returns> A newly constructed <see cref="IPhxMutableSet{T}" /> instance. </returns>
        /// <exception cref="ArgumentException">
        ///     thrown if <paramref name="throwOnDuplicates" /> is
        ///     <c> true </c> and the given collection contains duplicate values that were lost when copying to
        ///     a set.
        /// </exception>
        public static IPhxMutableSet<T> CopyToMutablePhxSet<T>(
                this IEnumerable<T> collection,
                bool throwOnDuplicates = false
        ) {
            var newSet = MutableSetOf(collection);
            if (throwOnDuplicates && newSet.Count != collection.Count()) {
                throw new ArgumentException("Given collection contained duplicates that were lost on copy.");
            }

            return newSet;
        }
        
        /// <summary> Returns a new set that is composed of the elements in original set and the given new elements. </summary>
        /// <typeparam name="T"> The type of elements contained in the set. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="elements"> The new elements to add to the set. </param>
        /// <returns> A mutable set containing all of the given elements. </returns>
        public static IPhxMutableSet<T> UnionedWith<T>(this IEnumerable<T> collection, params T[] elements) {
            var newSet = collection.CopyToMutablePhxSet();
            newSet.AddAll(elements);
            return newSet;
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMap{TKey, TValue}" /> instance. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMap{TKey, TValue}" /> instance. </returns>
        public static IPhxMap<TKey, TValue> CopyToPhxMap<TKey, TValue>(this IPhxMap<TKey, TValue> collection) {
            return new ImmutablePhxMap<TKey, TValue>(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMap{TKey, TValue}" /> instance. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMap{TKey, TValue}" /> instance. </returns>
        public static IPhxMap<TKey, TValue> CopyToPhxMap<TKey, TValue>(this IDictionary<TKey, TValue> collection) {
            return new ImmutablePhxMap<TKey, TValue>(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMutableMap{TKey,TValue}" /> instance. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMutableMap{TKey,TValue}" /> instance. </returns>
        public static IPhxMutableMap<TKey, TValue> CopyToMutablePhxMap<TKey, TValue>(
                this IPhxMap<TKey, TValue> collection
        ) {
            return new PhxHashMap<TKey, TValue>(collection);
        }

        /// <summary> Copies the given collection to an <see cref="IPhxMutableMap{TKey,TValue}" /> instance. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> A newly constructed <see cref="IPhxMutableMap{TKey,TValue}" /> instance. </returns>
        public static IPhxMutableMap<TKey, TValue> CopyToMutablePhxMap<TKey, TValue>(
                this IDictionary<TKey, TValue> collection
        ) {
            return new PhxHashMap<TKey, TValue>(collection);
        }
        
        public static IPhxMultiMap<TKey, TValue> CopyToPhxMultiMap<TKey, TValue>(
                this IPhxMultiMap<TKey, TValue> collection
        ) {
            return new PhxArrayListMultiMap<TKey, TValue>().Also(it => {
                it.AddAll(collection);
            });
        }
        
        public static IPhxMutableMultiMap<TKey, TValue> CopyToMutablePhxMultiMap<TKey, TValue>(
                this IPhxMultiMap<TKey, TValue> collection
        ) {
            return new PhxArrayListMultiMap<TKey, TValue>().Also(it => {
                it.AddAll(collection);
            });
        }
        
    }
}
