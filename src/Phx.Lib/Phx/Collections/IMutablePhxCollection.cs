// -----------------------------------------------------------------------------
//  <copyright file="IMutablePhxCollection.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;

    /// <summary> Represents a mutable collection of elements that can be queried or mutated. </summary>
    /// <typeparam name="T">
    ///     The type of the elements contained in the
    ///     <see cref="IMutablePhxCollection{T}" />.
    /// </typeparam>
    public interface IMutablePhxCollection<T> : IPhxCollection<T> {
        public bool Contains(T item);
        public bool ContainsAny(IEnumerable<T> items);
        public bool ContainsAll(IEnumerable<T> items);

        /// <summary> Adds the given item to the <see cref="IMutablePhxCollection{T}" />. </summary>
        /// <remarks>
        ///     The location where the item is added is determined by the implementation of the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </remarks>
        /// <param name="item"> The item to add. </param>
        /// <returns>
        ///     <c> true </c> if the item was added, or <c> false </c> if the
        ///     <see cref="IMutablePhxCollection{T}" /> does not support duplicates and the item is already
        ///     contained in the <see cref="IMutablePhxCollection{T}" />.
        /// </returns>
        public bool Add(T item);

        /// <summary> Adds all of the given items to the <see cref="IMutablePhxCollection{T}" />. </summary>
        /// <remarks>
        ///     The location where the item is added is determined by the implementation of the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </remarks>
        /// <param name="items"> The items to add. </param>
        /// <returns>
        ///     The number of items that were added. An item may not be added if the
        ///     <see cref="IMutablePhxCollection{T}" /> does not support duplicates and the item is already
        ///     contained in the <see cref="IMutablePhxCollection{T}" />.
        /// </returns>
        public int AddAll(IEnumerable<T> items);

        /// <summary>
        ///     Removes a single occurrence of the specified item from the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </summary>
        /// <param name="item"> The item to remove. </param>
        /// <returns>
        ///     <c> true </c> if the item was removed, or <c> false </c> if the item is not contained in
        ///     the <see cref="IMutablePhxCollection{T}" />.
        /// </returns>
        public bool Remove(T item);

        /// <summary>
        ///     Removes all occurrences of each of the specified items from the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </summary>
        /// <param name="items"> The items to remove. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RemoveAll(IEnumerable<T> items);

        /// <summary>
        ///     Removes all elements from the <see cref="IMutablePhxCollection{T}" /> that match the
        ///     given predicate.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate that returns <c> true </c> for all elements that should be
        ///     removed.
        /// </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RemoveAll(Predicate<T> predicate);

        /// <summary>
        ///     Removes all elements from the <see cref="IMutablePhxCollection{T}" /> except for those
        ///     contained in the given collection.
        /// </summary>
        /// <param name="items"> The items to retain. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RetainOnly(IEnumerable<T> items);

        /// <summary>
        ///     Removes all elements from the <see cref="IMutablePhxCollection{T}" /> except for those
        ///     that match the given predicate.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate that returns <c> true </c> for all elements that should be
        ///     retained.
        /// </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RetainOnly(Predicate<T> predicate);

        /// <summary> Removes all elements from the <see cref="IMutablePhxCollection{T}" />. </summary>
        public void Clear();
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IMutablePhxCollection{T}" />.
    /// </summary>
    public static class IMutablePhxCollectionExtensions {
        /// <summary> Adds all of the given items to the <see cref="IMutablePhxCollection{T}" />. </summary>
        /// <remarks>
        ///     The location where the item is added is determined by the implementation of the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </remarks>
        /// <typeparam name="T">
        ///     The type of the elements contained in the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to add. </param>
        /// <returns>
        ///     The number of items that were added. An item may not be added if the
        ///     <see cref="IMutablePhxCollection{T}" /> does not support duplicates and the item is already
        ///     contained in the <see cref="IMutablePhxCollection{T}" />.
        /// </returns>
        public static int AddAll<T>(this IMutablePhxCollection<T> collection, params T[] items) {
            return collection.AddAll(items);
        }

        /// <summary>
        ///     Removes all occurrences of each of the specified items from the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the elements contained in the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to remove. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public static int RemoveAll<T>(this IMutablePhxCollection<T> collection, params T[] items) {
            return collection.RemoveAll(items);
        }

        /// <summary>
        ///     Removes all elements from the <see cref="IMutablePhxCollection{T}" /> except for those
        ///     contained in the given collection.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the elements contained in the
        ///     <see cref="IMutablePhxCollection{T}" />.
        /// </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to retain. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public static int RetainOnly<T>(this IMutablePhxCollection<T> collection, params T[] items) {
            return collection.RetainOnly(items);
        }
    }
}
