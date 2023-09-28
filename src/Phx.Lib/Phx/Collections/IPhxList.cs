// -----------------------------------------------------------------------------
//  <copyright file="IPhxList.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using Phx.Lang;

    /// <summary> Represents an <see cref="IPhxCollection{T}" /> whose elements are ordered. </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxList{T}" />. </typeparam>
    public interface IPhxList<out T> : IPhxCollection<T> {
        /// <summary> Gets the element contained at the given index. </summary>
        /// <param name="index"> The index to retrieve the item from. </param>
        /// <returns> The element contained at the given index. </returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     thrown when the given index is negative or greater than
        ///     or equal to the <see cref="IPhxList{T}" />'s size.
        /// </exception>
        public T this[int index] { get; }

        /// <summary> Gets the element contained at the given index. </summary>
        /// <param name="index"> The index to retrieve the item from. </param>
        /// <returns> The element contained at the given index. </returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     thrown when the given index is negative or greater than
        ///     or equal to the <see cref="IPhxList{T}" />'s size.
        /// </exception>
        public T Get(int index);

        /// <summary> Gets the index of the first occurrence of the specified item. </summary>
        /// <param name="item"> The item to search for. </param>
        /// <returns>
        ///     An optional value containing the index of the first occurrence of the specified item if
        ///     the item is found.
        /// </returns>
        public IOptional<int> IndexOfFirst(object item);

        /// <summary> Gets the index of the first occurrence of the specified item. </summary>
        /// <param name="predicate"> The predicate that returns <c> true </c> when a desired item is provided. </param>
        /// <returns>
        ///     An optional value containing the index of the first occurrence of the specified item if
        ///     the item is found.
        /// </returns>
        public IOptional<int> IndexOfFirst(Predicate<T> predicate);

        /// <summary> Gets the index of the next occurrence of the specified item starting at the given index. </summary>
        /// <remarks>
        ///     The search will match an item at the given starting index. When using this method in a
        ///     loop, the previously located index should be incremented before passing to the method to
        ///     continue the search.
        /// </remarks>
        /// <param name="item"> The item to search for. </param>
        /// <param name="startingIndex"> The index to begin searching from. </param>
        /// <returns>
        ///     An optional value containing the index of the first occurrence of the specified item if
        ///     the item is found.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     thrown when the given index is negative or greater than
        ///     or equal to the <see cref="IPhxList{T}" />'s size.
        /// </exception>
        public IOptional<int> IndexOfNext(object item, int startingIndex);

        /// <summary> Gets the index of the next occurrence of the specified item starting at the given index. </summary>
        /// <remarks>
        ///     The search will match an item at the given starting index. When using this method in a
        ///     loop, the previously located index should be incremented before passing to the method to
        ///     continue the search.
        /// </remarks>
        /// <param name="predicate"> The predicate that returns <c> true </c> when a desired item is provided. </param>
        /// <param name="startingIndex"> The index to begin searching from. </param>
        /// <returns>
        ///     An optional value containing the index of the first occurrence of the specified item if
        ///     the item is found.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     thrown when the given index is negative or greater than
        ///     or equal to the <see cref="IPhxList{T}" />'s size.
        /// </exception>
        public IOptional<int> IndexOfNext(Predicate<T> predicate, int startingIndex);

        /// <summary> Gets the index of the last occurrence of the specified item. </summary>
        /// <param name="item"> The item to search for. </param>
        /// <returns>
        ///     An optional value containing the index of the first occurrence of the specified item if
        ///     the item is found.
        /// </returns>
        public IOptional<int> IndexOfLast(object item);

        /// <summary> Gets the index of the last occurrence of the specified item. </summary>
        /// <param name="predicate"> The predicate that returns <c> true </c> when a desired item is provided. </param>
        /// <returns>
        ///     An optional value containing the index of the first occurrence of the specified item if
        ///     the item is found.
        /// </returns>
        public IOptional<int> IndexOfLast(Predicate<T> predicate);
    }

    /// <summary> Contains extension methods applied to all implementations of <see cref="IPhxList{T}" />. </summary>
    public static class IPhxListExtensions {
        /// <summary> Gets whether the specified index is in bounds of the given <see cref="IPhxList{T}" />. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxList{T}" />. </typeparam>
        /// <param name="list"> The collection to perform the operation on. </param>
        /// <param name="index"> The index to check. </param>
        /// <returns>
        ///     <c> true </c> if the index is a non-negative value that is less than the size of the
        ///     <see cref="IPhxList{T}" />, otherwise <c> false </c>.
        /// </returns>
        public static bool InBounds<T>(this IPhxList<T> list, int index) {
            return index >= 0 && (index == 0 || index < list.Count);
        }

        /// <summary>
        ///     Throws an <see cref="IndexOutOfRangeException" /> if the specified index is out of bounds
        ///     of the given <see cref="IPhxList{T}" />.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxList{T}" />. </typeparam>
        /// <param name="list"> The collection to perform the operation on. </param>
        /// <param name="index"> The index to check. </param>
        /// <exception cref="IndexOutOfRangeException">
        ///     thrown when the given index is negative or greater than
        ///     or equal to the <see cref="IPhxList{T}" />'s size.
        /// </exception>
        public static void RequireIndexInBounds<T>(this IPhxList<T> list, int index) {
            if (!list.InBounds(index)) {
                throw new IndexOutOfRangeException($"Index {index} is out of the range [0,{list.Count}).");
            }
        }
    }
}
