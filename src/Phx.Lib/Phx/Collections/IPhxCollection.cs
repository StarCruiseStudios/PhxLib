﻿// -----------------------------------------------------------------------------
//  <copyright file="IPhxCollection.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary> Represents a readonly collection of elements that can be queried. </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCollection{T}" />. </typeparam>
    public interface IPhxCollection<out T> : IPhxContainer<T>, IEnumerable<T> {
        /// <summary>
        ///     Returns whether the specified item is contained inside of the
        ///     <see cref="IPhxCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will return greedily when the first occurrence of the specified item is
        ///     found.
        /// </remarks>
        /// <param name="item"> The item to locate in the <see cref="IPhxCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if the element is present in the <see cref="IPhxCollection{T}" />,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool Contains(object item);

        /// <summary>
        ///     Returns whether all items in the <see cref="IPhxCollection{T}" /> match the specified predicate.
        /// </summary>
        /// <remarks> This method will return greedily when the first item that does not match the predicate is found. </remarks>
        /// <param name="predicate"> The predicate that returns <c> true </c> if all items match the predicate. </param>
        /// <returns>
        ///     <c> true </c> if all elements present in the <see cref="IPhxCollection{T}" /> matche the given predicate, otherwise <c> false </c>.
        /// </returns>
        public bool ContainsAll(Predicate<T> predicate);

        /// <summary>
        ///     Returns whether all of the specified items are contained inside of the
        ///     <see cref="IPhxCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will greedily search for only a single occurrence of each of the specified
        ///     items, and will greedily return when one of the elements is found to be missing from the
        ///     <see cref="IPhxCollection{T}" />.
        /// </remarks>
        /// <param name="items"> The items to locate in the <see cref="IPhxCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if all of the elements are present in the <see cref="IPhxCollection{T}" />,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool ContainsAll(IEnumerable items);

        /// <summary>
        ///     Returns whether an item matching the specified predicate is contained inside of the
        ///     <see cref="IPhxCollection{T}" />.
        /// </summary>
        /// <remarks> This method will return greedily when the first item that matches the predicate is found. </remarks>
        /// <param name="predicate"> The predicate that returns <c> true </c> when a desired item is located. </param>
        /// <returns>
        ///     <c> true </c> if an element that matches the given predicate is present in the
        ///     <see cref="IPhxCollection{T}" />, otherwise <c> false </c>.
        /// </returns>
        public bool ContainsAny(Predicate<T> predicate);

        /// <summary>
        ///     Returns whether any of the specified items is contained inside of the
        ///     <see cref="IPhxCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will return greedily when the first occurrence of one of the specified items
        ///     is found.
        /// </remarks>
        /// <param name="items"> The items to locate in the <see cref="IPhxCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if one of the elements is present in the <see cref="IPhxCollection{T}" />,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool ContainsAny(IEnumerable items);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxCollection{T}" />.
    /// </summary>
    public static class IPhxCollectionExtensions {
        /// <summary>
        ///     Returns whether all of the specified items are contained inside of the
        ///     <see cref="IPhxCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will greedily search for only a single occurrence of each of the specified
        ///     items, and will greedily return when one of the elements is found to be missing from the
        ///     <see cref="IPhxCollection{T}" />.
        /// </remarks>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to locate in the <see cref="IPhxCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if all of the elements are present in the <see cref="IPhxCollection{T}" />,
        ///     otherwise <c> false </c>.
        /// </returns>
        public static bool ContainsAll<T>(this IPhxCollection<T> collection, params object[] items) {
            return collection.ContainsAll(items);
        }

        /// <summary>
        ///     Returns whether any of the specified items is contained inside of the
        ///     <see cref="IPhxCollection{T}" />.
        /// </summary>
        /// <remarks>
        ///     This method will return greedily when the first occurrence of one of the specified items
        ///     is found.
        /// </remarks>
        /// <param name="collection"> The collection to perform the operation on. </param>
        /// <param name="items"> The items to locate in the <see cref="IPhxCollection{T}" />. </param>
        /// <returns>
        ///     <c> true </c> if one of the elements is present in the <see cref="IPhxCollection{T}" />,
        ///     otherwise <c> false </c>.
        /// </returns>
        public static bool ContainsAny<T>(this IPhxCollection<T> collection, params object[] items) {
            return collection.ContainsAny(items);
        }
    }
}
