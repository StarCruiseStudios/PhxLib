// -----------------------------------------------------------------------------
//  <copyright file="IPhxMutableSet.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;

    /// <summary>
    ///     Represents an <see cref="IPhxMutableCollection{T}" /> whose elements are unique and can
    ///     be mutated.
    /// </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxMutableSet{T}" />. </typeparam>
    public interface IPhxMutableSet<T> : IPhxSet<T>, IPhxMutableCollection<T> {
        /// <summary>
        ///     Mutates this collection to contain all elements that are in this set and not in the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void Subtract(IEnumerable<T> other);

        /// <summary>
        ///     Mutates this collection to contain all elements that are in only one of this set and the
        ///     given collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void SymmetricSubtract(IEnumerable<T> other);

        /// <summary>
        ///     Mutates this collection to contain all elements that are in both this set and the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void Intersect(IEnumerable<T> other);

        /// <summary>
        ///     Mutates this collection to contain all elements that are in either this set or the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void Union(IEnumerable<T> other);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxMutableSet{T}" />.
    /// </summary>
    public static class IPhxMutableSetExtensions {
        /// <summary>
        ///     Mutates this collection to contain all elements that are in this set and not in the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxMutableSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void Subtract<T>(this IPhxMutableSet<T> set, params T[] other) {
            set.Subtract(other);
        }

        /// <summary>
        ///     Mutates this collection to contain all elements that are in only one of this set and the
        ///     given collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxMutableSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void SymmetricSubtract<T>(this IPhxMutableSet<T> set, params T[] other) {
            set.SymmetricSubtract(other);
        }

        /// <summary>
        ///     Mutates this collection to contain all elements that are in both this set and the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxMutableSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void Intersect<T>(this IPhxMutableSet<T> set, params T[] other) {
            set.Intersect(other);
        }

        /// <summary>
        ///     Mutates this collection to contain all elements that are in either this set or the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxMutableSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void Union<T>(this IPhxMutableSet<T> set, params T[] other) {
            set.Union(other);
        }
    }
}
