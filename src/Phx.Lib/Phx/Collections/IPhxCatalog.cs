// -----------------------------------------------------------------------------
//  <copyright file="IPhxCatalog.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections;

    /// <summary> Represents an <see cref="IPhxCollection{T}" /> whose elements are unique. </summary>
    /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCatalog{T}" />. </typeparam>
    public interface IPhxCatalog<out T> : IPhxCollection<T> {
        /// <summary>
        ///     Gets a value indicating if this set of items is equivalent to the set of items contained
        ///     in the given collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if the set of items is equivalent to the given set, otherwise
        ///     <c> false </c>.
        /// </returns>
        public bool IsEquivalent(IEnumerable other);

        /// <summary> Gets a value indicating if this set of items is contained in the given collection. </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool IsSubsetOf(IEnumerable other);

        /// <summary>
        ///     Gets a value indicating if this set of items is contained in the given collection, and
        ///     the sets are not equivalent.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public bool IsProperSubsetOf(IEnumerable other);

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public bool IsSupersetOf(IEnumerable other);

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection, and the sets are not equivalent.
        /// </summary>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public bool IsProperSupersetOf(IEnumerable other);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxCatalog{T}" />.
    /// </summary>
    public static class IPhxCatalogExtensions {
        /// <summary>
        ///     Gets a value indicating if this set of items is equivalent to the set of items contained
        ///     in the given collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCatalog{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if the set of items is equivalent to the given set, otherwise
        ///     <c> false </c>.
        /// </returns>
        public static bool IsEquivalent<T>(this IPhxCatalog<T> set, params T[] other) {
            return set.IsEquivalent(other);
        }

        /// <summary> Gets a value indicating if this set of items is contained in the given collection. </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCatalog{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public static bool IsSubsetOf<T>(this IPhxCatalog<T> set, params T[] other) {
            return set.IsSubsetOf(other);
        }

        /// <summary>
        ///     Gets a value indicating if this set of items is contained in the given collection, and
        ///     the sets are not equivalent.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCatalog{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in this set are contained in the given collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public static bool IsProperSubsetOf<T>(this IPhxCatalog<T> set, params T[] other) {
            return set.IsProperSubsetOf(other);
        }

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCatalog{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection,
        ///     otherwise <c> false </c>.
        /// </returns>
        public static bool IsSupersetOf<T>(this IPhxCatalog<T> set, params T[] other) {
            return set.IsSupersetOf(other);
        }

        /// <summary>
        ///     Gets a value indicating if this set of items contains all of the items in the given
        ///     collection, and the sets are not equivalent.
        /// </summary>
        /// <typeparam name="T"> The type of the elements contained in the <see cref="IPhxCatalog{T}" />. </typeparam>
        /// <param name="set"> The collection to perform the operation on. </param>
        /// <param name="other"> The collection of items to compare to this one. </param>
        /// <returns>
        ///     <c> true </c> if all of the items in the given set are contained in this collection and
        ///     the collections are not equivalent, otherwise <c> false </c>.
        /// </returns>
        public static bool IsProperSupersetOf<T>(this IPhxCatalog<T> set, params T[] other) {
            return set.IsProperSupersetOf(other);
        }
    }
}
