// -----------------------------------------------------------------------------
//  <copyright file="PhxCollections.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Provides methods for initializing Phx collection instances.
    /// </summary>
    public static class PhxCollections {
        /// <summary>
        ///     Gets an immutable empty list.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the list. </typeparam>
        /// <returns> An immutable empty list. </returns>
        public static IPhxList<T> EmptyList<T>() {
            return new ImmutablePhxList<T>();
        }

        /// <summary>
        ///     Gets an immutable list containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the list. </typeparam>
        /// <param name="elements"> The elements contained in the list. </param>
        /// <returns> An immutable list containing the given elements. </returns>
        public static IPhxList<T> ListOf<T>(params T[] elements) {
            return new ImmutablePhxList<T>(elements.AsEnumerable());
        }

        /// <summary>
        ///     Gets an immutable list containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the list. </typeparam>
        /// <param name="elements"> The elements contained in the list. </param>
        /// <returns> An immutable list containing the given elements. </returns>
        public static IPhxList<T> ListOf<T>(IEnumerable<T> elements) {
            return new ImmutablePhxList<T>(elements);
        }

        /// <summary>
        ///     Gets a mutable list containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the list. </typeparam>
        /// <param name="elements"> The elements contained in the list. </param>
        /// <returns> A mutable list containing the given elements. </returns>
        public static IMutablePhxList<T> MutableListOf<T>(params T[] elements) {
            return new PhxArrayList<T>(elements.AsEnumerable());
        }

        /// <summary>
        ///     Gets a mutable list containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the list. </typeparam>
        /// <param name="elements"> The elements contained in the list. </param>
        /// <returns> A mutable list containing the given elements. </returns>
        public static IMutablePhxList<T> MutableListOf<T>(IEnumerable<T> elements) {
            return new PhxArrayList<T>(elements);
        }

        /// <summary>
        ///     Gets an immutable empty set.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the set. </typeparam>
        /// <returns> An immutable empty set. </returns>
        public static IPhxSet<T> EmptySet<T>() {
            return new ImmutablePhxSet<T>();
        }

        /// <summary>
        ///     Gets an immutable set containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the set. </typeparam>
        /// <param name="elements"> The elements contained in the set. </param>
        /// <returns> An immutable set containing the given elements. </returns>
        public static IPhxSet<T> SetOf<T>(params T[] elements) {
            return new ImmutablePhxSet<T>(elements.AsEnumerable());
        }

        /// <summary>
        ///     Gets an immutable set containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the set. </typeparam>
        /// <param name="elements"> The elements contained in the set. </param>
        /// <returns> An immutable set containing the given elements. </returns>
        public static IPhxSet<T> SetOf<T>(IEnumerable<T> elements) {
            return new ImmutablePhxSet<T>(elements);
        }

        /// <summary>
        ///     Gets a mutable set containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the set. </typeparam>
        /// <param name="elements"> The elements contained in the set. </param>
        /// <returns> A mutable set containing the given elements. </returns>
        public static IMutablePhxSet<T> MutableSetOf<T>(params T[] elements) {
            return new PhxHashSet<T>(elements.AsEnumerable());
        }

        /// <summary>
        ///     Gets a mutable set containing the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the set. </typeparam>
        /// <param name="elements"> The elements contained in the set. </param>
        /// <returns> A mutable set containing the given elements. </returns>
        public static IMutablePhxSet<T> MutableSetOf<T>(IEnumerable<T> elements) {
            return new PhxHashSet<T>(elements);
        }

        /// <summary>
        ///     Gets an immutable empty map.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <returns> An immutable empty map. </returns>
        public static IPhxMap<TKey, TValue> EmptyMap<TKey, TValue>() {
            return new ImmutablePhxMap<TKey, TValue>();
        }

        /// <summary>
        ///     Gets an immutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IPhxMap<TKey, TValue> MapOf<TKey, TValue>(IPhxMap<TKey, TValue> elements) {
            return new ImmutablePhxMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets an immutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IPhxMap<TKey, TValue> MapOf<TKey, TValue>(IDictionary<TKey, TValue> elements) {
            return new ImmutablePhxMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets an immutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IPhxMap<TKey, TValue> MapOf<TKey, TValue>(IEnumerable<IPhxPair<TKey, TValue>> elements) {
            return new ImmutablePhxMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets an immutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IPhxMap<TKey, TValue> MapOf<TKey, TValue>(params (TKey, TValue)[] elements) {
            return new ImmutablePhxMap<TKey, TValue>(elements.AsEnumerable());
        }

        /// <summary>
        ///     Gets an immutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IPhxMap<TKey, TValue> MapOf<TKey, TValue>(IEnumerable<(TKey, TValue)> elements) {
            return new ImmutablePhxMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets a mutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IMutablePhxMap<TKey, TValue> MutableMapOf<TKey, TValue>(IPhxMap<TKey, TValue> elements) {
            return new PhxHashMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets a mutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IMutablePhxMap<TKey, TValue> MutableMapOf<TKey, TValue>(IDictionary<TKey, TValue> elements) {
            return new PhxHashMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets a mutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IMutablePhxMap<TKey, TValue> MutableMapOf<TKey, TValue>(
            IEnumerable<IPhxPair<TKey, TValue>> elements
        ) {
            return new PhxHashMap<TKey, TValue>(elements);
        }

        /// <summary>
        ///     Gets a mutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IMutablePhxMap<TKey, TValue> MutableMapOf<TKey, TValue>(params (TKey, TValue)[] elements) {
            return new PhxHashMap<TKey, TValue>(elements.AsEnumerable());
        }

        /// <summary>
        ///     Gets a mutable map containing the given elements.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="elements"> The elements contained in the map. </param>
        /// <returns> An immutable map containing the given elements. </returns>
        public static IMutablePhxMap<TKey, TValue> MutableMapOf<TKey, TValue>(IEnumerable<(TKey, TValue)> elements) {
            return new PhxHashMap<TKey, TValue>(elements);
        }
    }
}
