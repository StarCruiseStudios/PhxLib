// -----------------------------------------------------------------------------
//  <copyright file="IMutablePhxSet.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Phx.Collections {
    /// <summary>
    ///     Represents an <see cref="IMutablePhxCollection{T}"/> whose elements are unique and can be mutated.
    /// </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IMutablePhxSet{T}"/>. </typeparam>
    public interface IMutablePhxSet<T> : IPhxSet<T>, IMutablePhxCollection<T> {
        /// <summary>
        ///     Mutates this collection to contain all elements that are in this set and not in the given collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void Subtract(IEnumerable<T> other);

        /// <summary>
        ///     Mutates this collection to contain all elements that are in only one of this set and the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void SymmetricSubtract(IEnumerable<T> other);

        /// <summary>
        ///     Mutates this collection to contain all elements that are in both this set and the given collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void Intersect(IEnumerable<T> other);

        /// <summary>
        ///     Mutates this collection to contain all elements that are in either this set or the given collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public void Union(IEnumerable<T> other);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of <see cref="IMutablePhxSet{T}"/>.
    /// </summary>
    public static class IMutablePhxSetExtensions {
        /// <summary>
        ///     Mutates this collection to contain all elements that are in this set and not in the given collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IMutablePhxSet{T}"/>. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void Subtract<T>(this IMutablePhxSet<T> set, params T[] other) {
            set.Subtract(other);
        }

        /// <summary>
        ///     Mutates this collection to contain all elements that are in only one of this set and the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IMutablePhxSet{T}"/>. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void SymmetricSubtract<T>(this IMutablePhxSet<T> set, params T[] other) {
            set.SymmetricSubtract(other);
        }

        /// <summary>
        ///     Mutates this collection to contain all elements that are in both this set and the given collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IMutablePhxSet{T}"/>. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void Intersect<T>(this IMutablePhxSet<T> set, params T[] other) {
            set.Intersect(other);
        }

        /// <summary>
        ///     Mutates this collection to contain all elements that are in either this set or the given collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IMutablePhxSet{T}"/>. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        public static void Union<T>(this IMutablePhxSet<T> set, params T[] other) {
            set.Union(other);
        }
    }
}