// -----------------------------------------------------------------------------
//  <copyright file="IPhxMutableCollection.cs" company="Star Cruise Studios LLC">
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
    ///     <see cref="IPhxMutableCollection{T}" />.
    /// </typeparam>
    public interface IPhxMutableCollection<T> : IPhxCollection<T> {
        /// <summary> Adds the given item to the <see cref="IPhxMutableCollection{T}" />. </summary>
        /// <remarks>
        ///     The location where the item is added is determined by the implementation of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </remarks>
        /// <param name="item"> The item to add. </param>
        /// <returns>
        ///     <c> true </c> if the item was added, or <c> false </c> if the
        ///     <see cref="IPhxMutableCollection{T}" /> does not support duplicates and the item is already
        ///     contained in the <see cref="IPhxMutableCollection{T}" />.
        /// </returns>
        public bool Add(T item);

        /// <summary> Adds all of the given items to the <see cref="IPhxMutableCollection{T}" />. </summary>
        /// <remarks>
        ///     The location where the item is added is determined by the implementation of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </remarks>
        /// <param name="items"> The items to add. </param>
        /// <returns>
        ///     The number of items that were added. An item may not be added if the
        ///     <see cref="IPhxMutableCollection{T}" /> does not support duplicates and the item is already
        ///     contained in the <see cref="IPhxMutableCollection{T}" />.
        /// </returns>
        public int AddAll(IEnumerable<T> items);

        /// <summary> Removes all elements from the <see cref="IPhxMutableCollection{T}" />. </summary>
        public void Clear();

        /// <summary>
        ///     Returns whether the specified item is contained inside of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will return greedily when the first occurrence of the specified item is
        ///     found.
        /// </remarks>
        /// <param name="item"> The item to locate in the <see cref="IPhxMutableCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if the element is present in the <see cref="IPhxMutableCollection{T}" />,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool Contains(T item);

        /// <summary>
        ///     Returns whether all of the specified items are contained inside of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will greedily search for only a single occurrence of each of the specified
        ///     items, and will greedily return when one of the elements is found to be missing from the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </remarks>
        /// <param name="items"> The items to locate in the <see cref="IPhxMutableCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if all of the elements are present in the
        ///     <see cref="IPhxMutableCollection{T}" />, otherwise <c> false </c>.
        /// </returns>
        public bool ContainsAll(IEnumerable<T> items);

        /// <summary>
        ///     Returns whether any of the specified items is contained inside of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will return greedily when the first occurrence of one of the specified items
        ///     is found.
        /// </remarks>
        /// <param name="items"> The items to locate in the <see cref="IPhxMutableCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if one of the elements is present in the
        ///     <see cref="IPhxMutableCollection{T}" />, otherwise <c> false </c>.
        /// </returns>
        public bool ContainsAny(IEnumerable<T> items);

        /// <summary>
        ///     Removes a single occurrence of the specified item from the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <param name="item"> The item to remove. </param>
        /// <returns>
        ///     <c> true </c> if the item was removed, or <c> false </c> if the item is not contained in
        ///     the <see cref="IPhxMutableCollection{T}" />.
        /// </returns>
        public bool Remove(T item);

        /// <summary>
        ///     Removes all elements from the <see cref="IPhxMutableCollection{T}" /> that match the
        ///     given predicate.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate that returns <c> true </c> for all elements that should be
        ///     removed.
        /// </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RemoveAll(Predicate<T> predicate);

        /// <summary>
        ///     Removes all occurrences of each of the specified items from the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <param name="items"> The items to remove. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RemoveAll(IEnumerable<T> items);

        /// <summary>
        ///     Removes all elements from the <see cref="IPhxMutableCollection{T}" /> except for those
        ///     that match the given predicate.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate that returns <c> true </c> for all elements that should be
        ///     retained.
        /// </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RetainOnly(Predicate<T> predicate);

        /// <summary>
        ///     Removes all elements from the <see cref="IPhxMutableCollection{T}" /> except for those
        ///     contained in the given collection.
        /// </summary>
        /// <param name="items"> The items to retain. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public int RetainOnly(IEnumerable<T> items);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxMutableCollection{T}" />.
    /// </summary>
    public static class IPhxMutableCollectionExtensions {
        /// <summary> Adds all of the given items to the <see cref="IPhxMutableCollection{T}" />. </summary>
        /// <remarks>
        ///     The location where the item is added is determined by the implementation of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </remarks>
        /// <typeparam name="T">
        ///     The type of the elements contained in the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to add. </param>
        /// <returns>
        ///     The number of items that were added. An item may not be added if the
        ///     <see cref="IPhxMutableCollection{T}" /> does not support duplicates and the item is already
        ///     contained in the <see cref="IPhxMutableCollection{T}" />.
        /// </returns>
        public static int AddAll<T>(this IPhxMutableCollection<T> collection, params T[] items) {
            return collection.AddAll(items);
        }

        /// <summary>
        ///     Returns whether all of the specified items are contained inside of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will greedily search for only a single occurrence of each of the specified
        ///     items, and will greedily return when one of the elements is found to be missing from the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </remarks>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to locate in the <see cref="IPhxMutableCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if all of the elements are present in the
        ///     <see cref="IPhxMutableCollection{T}" />, otherwise <c> false </c>.
        /// </returns>
        public static bool ContainsAll<T>(this IPhxMutableCollection<T> collection, params T[] items) {
            return collection.ContainsAll(items);
        }

        /// <summary>
        ///     Returns whether any of the specified items is contained inside of the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will return greedily when the first occurrence of one of the specified items
        ///     is found.
        /// </remarks>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to locate in the <see cref="IPhxMutableCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if one of the elements is present in the
        ///     <see cref="IPhxMutableCollection{T}" />, otherwise <c> false </c>.
        /// </returns>
        public static bool ContainsAny<T>(this IPhxMutableCollection<T> collection, params T[] items) {
            return collection.ContainsAny(items);
        }

        /// <summary>
        ///     Removes all occurrences of each of the specified items from the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the elements contained in the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to remove. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public static int RemoveAll<T>(this IPhxMutableCollection<T> collection, params T[] items) {
            return collection.RemoveAll(items);
        }

        /// <summary>
        ///     Removes all elements from the <see cref="IPhxMutableCollection{T}" /> except for those
        ///     contained in the given collection.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the elements contained in the
        ///     <see cref="IPhxMutableCollection{T}" />.
        /// </typeparam>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to retain. </param>
        /// <returns> The number of elements that were removed by the operation. </returns>
        public static int RetainOnly<T>(this IPhxMutableCollection<T> collection, params T[] items) {
            return collection.RetainOnly(items);
        }
    }
}
