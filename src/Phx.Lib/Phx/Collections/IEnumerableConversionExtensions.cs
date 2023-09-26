// -----------------------------------------------------------------------------
//  <copyright file="IEnumerableConversionExtensions.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Contains extension methods to convert collections to Phx Collection types.
    /// </summary>
    public static class IEnumerableConversionExtensions {
        /// <summary>
        ///     Converts the given collection to an <see cref="IMutablePhxCollection{T}"/> instance.
        /// </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the contents to a new
        ///     <see cref="IMutablePhxCollection{T}"/> instance.
        /// </remarks>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> The collection as an <see cref="IMutablePhxCollection{T}"/> instance. </returns>
        public static IMutablePhxCollection<T> AsMutablePhxCollection<T>(this IEnumerable<T> collection) {
            return collection as IMutablePhxCollection<T> ?? collection.CopyToMutablePhxList();
        }

        /// <summary>
        ///     Converts the given collection to an <see cref="IMutablePhxList{T}"/> instance.
        /// </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the contents to a new
        ///     <see cref="IMutablePhxList{T}"/> instance.
        /// </remarks>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> The collection as an <see cref="IMutablePhxList{T}"/> instance. </returns>
        public static IMutablePhxList<T> AsMutablePhxList<T>(this IEnumerable<T> collection) {
            return collection as IMutablePhxList<T> ?? collection.CopyToMutablePhxList();
        }

        /// <summary>
        ///     Converts the given collection to an <see cref="IMutablePhxList{T}"/> instance.
        /// </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the contents to a new
        ///     <see cref="IMutablePhxList{T}"/> instance.
        /// </remarks>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="throwOnDuplicates"> Indicates whether an exception should be thrown if the given collection
        ///                                  contains duplicates that will be lost on copy. </param>
        /// <returns> The collection as an <see cref="IMutablePhxList{T}"/> instance. </returns>
        /// <exception cref="ArgumentException"> thrown if <paramref name="throwOnDuplicates"/> is <c>true</c> and the
        ///                                      given collection contains duplicate values that were lost when copying
        ///                                      to a set. </exception>
        public static IMutablePhxSet<T> AsMutablePhxSet<T>(
            this IEnumerable<T> collection,
            bool throwOnDuplicates = false
        ) {
            return collection as IMutablePhxSet<T> ?? collection.CopyToMutablePhxSet(throwOnDuplicates);
        }

        /// <summary>
        ///     Converts the given collection to an <see cref="IMutablePhxMap{TKey, TValue}"/> instance.
        /// </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the contents to a new
        ///     <see cref="IMutablePhxMap{TKey, TValue}"/> instance.
        /// </remarks>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> The collection as an <see cref="IMutablePhxMap{TKey, TValue}"/> instance. instance. </returns>
        public static IMutablePhxMap<TKey, TValue> AsMutablePhxMap<TKey, TValue>(
            this IPhxMap<TKey, TValue> collection
        ) {
            return collection as IMutablePhxMap<TKey, TValue> ?? collection.CopyToMutablePhxMap();
        }
    }
}
