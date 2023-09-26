// -----------------------------------------------------------------------------
//  <copyright file="IMutablePhxList.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Represents an <see cref="IMutablePhxCollection{T}"/> whose elements are ordered and can be mutated.
    /// </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IMutablePhxList{T}"/>. </typeparam>
    public interface IMutablePhxList<T> : IPhxList<T>, IMutablePhxCollection<T> {
        /// <summary>
        ///     Gets or sets the element contained at the given index.
        /// </summary>
        /// <param name="index"> The index of the item. </param>
        /// <returns> The element contained at the given index. </returns>
        /// <exception cref="IndexOutOfRangeException"> thrown when the given index is negative or greater than or equal
        ///                                             to the <see cref="IPhxList{T}"/>'s size. </exception>
        public new T this[int index] { get; set; }

        /// <summary>
        ///     Inserts the given item into the <see cref="IMutablePhxList{T}"/> at the given index.
        /// </summary>
        /// <param name="index"> The index to insert the item. </param>
        /// <param name="item"> The item to insert. </param>
        /// <exception cref="IndexOutOfRangeException"> thrown when the given index is negative or greater than or equal
        ///                                             to the <see cref="IPhxList{T}"/>'s size. </exception>
        public void Insert(int index, T item);

        /// <summary>
        ///     Inserts the given items into the <see cref="IMutablePhxList{T}"/> at the given index.
        /// </summary>
        /// <param name="index"> The index to insert the items. </param>
        /// <param name="items"> The items to insert. </param>
        /// <exception cref="IndexOutOfRangeException"> thrown when the given index is negative or greater than or equal
        ///                                             to the <see cref="IPhxList{T}"/>'s size. </exception>
        public void InsertAll(int index, IEnumerable<T> items);

        /// <summary>
        ///     Removes the element at the given index.
        /// </summary>
        /// <param name="index"> The index of the element to remove. </param>
        /// <exception cref="IndexOutOfRangeException"> thrown when the given index is negative or greater than or equal
        ///                                             to the <see cref="IPhxList{T}"/>'s size. </exception>
        public void RemoveAt(int index);

        /// <summary>
        ///     Sets the element contained at the given index.
        /// </summary>
        /// <param name="index"> The index of the item. </param>
        /// <param name="value"> The value to set. </param>
        /// <exception cref="IndexOutOfRangeException"> thrown when the given index is negative or greater than or equal
        ///                                             to the <see cref="IPhxList{T}"/>'s size. </exception>
        public void Set(int index, T value);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of <see cref="IMutablePhxList{T}"/>.
    /// </summary>
    public static class IMutablePhxListExtensions {
        /// <summary>
        ///     Inserts the given items into the <see cref="IMutablePhxList{T}"/> at the given index.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxList{T}"/>. </typeparam>
        /// <param name="list"> The collection to perform the operation on. </param>
        /// <param name="index"> The index to insert the items. </param>
        /// <param name="items"> The items to insert. </param>
        /// <exception cref="IndexOutOfRangeException"> thrown when the given index is negative or greater than or equal
        ///                                             to the <see cref="IPhxList{T}"/>'s size. </exception>
        public static void InsertAll<T>(this IMutablePhxList<T> list, int index, params T[] items) {
            list.InsertAll(index, items);
        }
    }
}
