// -----------------------------------------------------------------------------
//  <copyright file="IPhxSet.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;

    /// <summary> Represents an <see cref="IPhxCollection{T}" /> whose elements are unique. </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
    public interface IPhxSet<T> : IPhxCatalog<T> {
        /// <summary>
        ///     Gets a value indicating if this set of items is equivalent to the set of items contained
        ///     in the given collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if the set of items is equivalent to the given set, otherwise
        ///     <c> false </c>.
        /// </returns>
        public bool IsEquivalent(IEnumerable<T> other);

        /// <summary> Gets a value indicating if this set of items is contained in the given collection. </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool IsSubsetOf(IEnumerable<T> other);

        /// <summary>
        ///     Gets a value indicating if this set of items is contained in the given collection, and
        ///     the sets are not equivalent.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public bool IsProperSubsetOf(IEnumerable<T> other);

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool IsSupersetOf(IEnumerable<T> other);

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection, and the sets are not equivalent.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public bool IsProperSupersetOf(IEnumerable<T> other);

        /// <summary> Gets a set containing all subsets of this set. </summary>
        /// <returns> The powerset of all subsets of this set. </returns>
        public IPhxSet<IPhxSet<T>> GetPowerSet();

        /// <summary>
        ///     Gets a new set that contains all elements that are in this set and not in the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the subtraction of two sets. </returns>
        public IPhxSet<T> GetSubtraction(IEnumerable<T> other);

        /// <summary>
        ///     Gets a new set that contains all elements that are in only one of this set and the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the symmetric subtraction of two sets. </returns>
        public IPhxSet<T> GetSymmetricSubtraction(IEnumerable<T> other);

        /// <summary>
        ///     Gets a new set that contains all elements that are in both this set and the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the intersection of two sets. </returns>
        public IPhxSet<T> GetIntersection(IEnumerable<T> other);

        /// <summary>
        ///     Gets a new set that contains all elements that are in either this set or the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the union of two sets. </returns>
        public IPhxSet<T> GetUnion(IEnumerable<T> other);
    }

    /// <summary> Contains extension methods applied to all implementations of <see cref="IPhxSet{T}" />. </summary>
    public static class IPhxSetExtensions {
        /// <summary>
        ///     Gets a value indicating if this set of items is equivalent to the set of items contained
        ///     in the given collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if the set of items is equivalent to the given set, otherwise
        ///     <c> false </c>.
        /// </returns>
        public static bool IsEquivalent<T>(this IPhxSet<T> set, params T[] other) {
            return set.IsEquivalent(other);
        }

        /// <summary> Gets a value indicating if this set of items is contained in the given collection. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public static bool IsSubsetOf<T>(this IPhxSet<T> set, params T[] other) {
            return set.IsSubsetOf(other);
        }

        /// <summary>
        ///     Gets a value indicating if this set of items is contained in the given collection, and
        ///     the sets are not equivalent.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public static bool IsProperSubsetOf<T>(this IPhxSet<T> set, params T[] other) {
            return set.IsProperSubsetOf(other);
        }

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public static bool IsSupersetOf<T>(this IPhxSet<T> set, params T[] other) {
            return set.IsSupersetOf(other);
        }

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection, and the sets are not equivalent.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public static bool IsProperSupersetOf<T>(this IPhxSet<T> set, params T[] other) {
            return set.IsProperSupersetOf(other);
        }

        /// <summary>
        ///     Gets a new set that contains all elements that are in this set and not in the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the subtraction of two sets. </returns>
        public static IPhxSet<T> GetSubtraction<T>(this IPhxSet<T> set, params T[] other) {
            return set.GetSubtraction(other);
        }

        /// <summary>
        ///     Gets a new set that contains all elements that are in only one of this set and the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the symmetric subtraction of two sets. </returns>
        public static IPhxSet<T> GetSymmetricSubtraction<T>(this IPhxSet<T> set, params T[] other) {
            return set.GetSymmetricSubtraction(other);
        }

        /// <summary>
        ///     Gets a new set that contains all elements that are in both this set and the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the intersection of two sets. </returns>
        public static IPhxSet<T> GetIntersection<T>(this IPhxSet<T> set, params T[] other) {
            return set.GetIntersection(other);
        }

        /// <summary>
        ///     Gets a new set that contains all elements that are in either this set or the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxSet{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns> A new <see cref="IPhxSet{T}" /> contianing the union of two sets. </returns>
        public static IPhxSet<T> GetUnion<T>(this IPhxSet<T> set, params T[] other) {
            return set.GetUnion(other);
        }
    }
}
