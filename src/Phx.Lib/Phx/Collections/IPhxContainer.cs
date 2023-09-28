// -----------------------------------------------------------------------------
//  <copyright file="IPhxContainer.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;

    /// <summary> Represents a class that contains multiple elements. </summary>
    public interface IPhxContainer<out T> {
        /// <summary> Gets the number of elements contained in the <see cref="IPhxContainer{T}" />. </summary>
        public int Count { get; }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="IPhxContainer{T}" /> that match
        ///     the given predicate.
        /// </summary>
        /// <param name="predicate"> The predicate that returns <c> true </c> when a desired item is provided. </param>
        /// <returns>
        ///     The number of elements contained in the <see cref="IPhxContainer{T}" /> that match the
        ///     given predicate.
        /// </returns>
        public int CountWhere(Predicate<T> predicate);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of <see cref="IPhxContainer" />
    ///     .
    /// </summary>
    public static class IPhxContainerExtensions {
        /// <summary> Gets a value indicating whether the <see cref="IPhxContainer" /> is empty. </summary>
        /// <param name="container"> The container to perform the operation on. </param>
        /// <returns> <c> true </c> if the <see cref="IPhxContainer" /> is empty, otherwise <c> false </c>. </returns>
        public static bool IsEmpty<T>(this IPhxContainer<T> container) {
            return container.Count == 0;
        }

        /// <summary> Gets a value indicating whether the <see cref="IPhxContainer" /> is not empty. </summary>
        /// <param name="container"> The container to perform the operation on. </param>
        /// <returns>
        ///     <c> true </c> if the <see cref="IPhxContainer" /> contains at least one item, otherwise
        ///     <c> false </c>.
        /// </returns>
        public static bool IsNotEmpty<T>(this IPhxContainer<T> container) {
            return container.Count != 0;
        }

        /// <summary> Gets a value indicating whether the <see cref="IPhxContainer" /> is empty or is null. </summary>
        /// <param name="container"> The container to perform the operation on. </param>
        /// <returns>
        ///     <c> true </c> if the <see cref="IPhxContainer" /> is empty or null, otherwise
        ///     <c> false </c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IPhxContainer<T>? container) {
            return container == null || container.Count == 0;
        }
    }
}
