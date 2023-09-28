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

    /// <summary> Contains extension methods to convert collections to Phx Collection types. </summary>
    public static class IEnumerableConversionExtensions {
        /// <summary> Converts the given collection to an <see cref="IPhxMutableCollection{T}" /> instance. </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the
        ///     contents to a new <see cref="IPhxMutableCollection{T}" /> instance.
        /// </remarks>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> The collection as an <see cref="IPhxMutableCollection{T}" /> instance. </returns>
        public static IPhxMutableCollection<T> AsMutablePhxCollection<T>(this IEnumerable<T> collection) {
            return collection as IPhxMutableCollection<T> ?? collection.CopyToMutablePhxList();
        }

        /// <summary> Converts the given collection to an <see cref="IPhxMutableList{T}" /> instance. </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the
        ///     contents to a new <see cref="IPhxMutableList{T}" /> instance.
        /// </remarks>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> The collection as an <see cref="IPhxMutableList{T}" /> instance. </returns>
        public static IPhxMutableList<T> AsMutablePhxList<T>(this IEnumerable<T> collection) {
            return collection as IPhxMutableList<T> ?? collection.CopyToMutablePhxList();
        }

        /// <summary> Converts the given collection to an <see cref="IPhxMutableList{T}" /> instance. </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the
        ///     contents to a new <see cref="IPhxMutableList{T}" /> instance.
        /// </remarks>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IEnumerable{T}" />. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="throwOnDuplicates">
        ///     Indicates whether an exception should be thrown if the given
        ///     collection contains duplicates that will be lost on copy.
        /// </param>
        /// <returns> The collection as an <see cref="IPhxMutableList{T}" /> instance. </returns>
        /// <exception cref="ArgumentException">
        ///     thrown if <paramref name="throwOnDuplicates" /> is
        ///     <c> true </c> and the given collection contains duplicate values that were lost when copying to
        ///     a set.
        /// </exception>
        public static IPhxMutableSet<T> AsMutablePhxSet<T>(
                this IEnumerable<T> collection,
                bool throwOnDuplicates = false
        ) {
            return collection as IPhxMutableSet<T> ?? collection.CopyToMutablePhxSet(throwOnDuplicates);
        }

        /// <summary> Converts the given collection to an <see cref="IPhxMutableMap{TKey,TValue}" /> instance. </summary>
        /// <remarks>
        ///     This method will first attempt to cast the collection. If that fails, it will copy the
        ///     contents to a new <see cref="IPhxMutableMap{TKey,TValue}" /> instance.
        /// </remarks>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <returns> The collection as an <see cref="IPhxMutableMap{TKey,TValue}" /> instance. instance. </returns>
        public static IPhxMutableMap<TKey, TValue> AsMutablePhxMap<TKey, TValue>(
                this IPhxMap<TKey, TValue> collection
        ) {
            return collection as IPhxMutableMap<TKey, TValue> ?? collection.CopyToMutablePhxMap();
        }
    }
}
