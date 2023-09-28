// -----------------------------------------------------------------------------
//  <copyright file="IPhxMutableMultiMap.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Phx.Lang;

    /// <summary> Represents a mutable collection of mappings from a key to one or more values. </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    public interface IPhxMutableMultiMap<TKey, TValue> : IPhxMultiMap<TKey, TValue> {
        /// <summary>
        ///     Gets the elements associated with the specified key if any exist, or else throws an
        ///     <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A collection of <typeparamref name="TValue" /> instances retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public new IPhxMutableCollection<TValue> this[TKey key] { get; }

        /// <summary>
        ///     Gets the set of all key value pairs contained in this
        ///     <see cref="IPhxMutableMultiMap{TKey, TValue}" /> instance.
        /// </summary>
        public new IPhxCollection<IPhxKeyValuePair<TKey, IPhxMutableCollection<TValue>>> Entries { get; }

        /// <summary>
        ///     Gets a collection of all values contained in this
        ///     <see cref="IPhxMutableMultiMap{TKey, TValue}" /> instance.
        /// </summary>
        public new IPhxCollection<IPhxMutableCollection<TValue>> Values { get; }

        /// <summary> Gets the collection of elements associated with the specified key. </summary>
        /// <param name="key"> The key. </param>
        /// <returns> An optional containing the collection of values associated with the key if found. </returns>
        public new IOptional<IPhxMutableCollection<TValue>> Get(TKey key);

        /// <summary> Adds the value to the collection of values associated with the specified key. </summary>
        /// <param name="key"> The key. </param>
        /// <param name="value"> The value. </param>
        public bool Add(TKey key, TValue value);

        /// <summary> Adds the values associated with each of the specified keys. </summary>
        /// <param name="values"> A map containing the key value pairs to set. </param>
        public int AddAll(IPhxMap<TKey, TValue> values);

        /// <summary> Adds the values associated with each of the specified keys. </summary>
        /// <param name="values"> A multimap containing the key value pairs to set. </param>
        public int AddAll(IPhxMultiMap<TKey, TValue> values);

        /// <summary> Adds the values associated with each of the specified keys. </summary>
        /// <param name="values"> A collection of key value pairs to set. </param>
        public int AddAll(IEnumerable<(TKey, TValue)> values);

        /// <summary> Removes all elements from the <see cref="IPhxMutableMultiMap{TKey,TValue}" />. </summary>
        public void Clear();

        /// <summary>
        ///     Removes a single occurrence of the specified item associated with the given key from the
        ///     <see cref="IPhxMutableMultiMap{TKey,TValue}" />.
        /// </summary>
        /// <param name="key"> The key associated with the value to remove. </param>
        /// <param name="value"> The item to remove. </param>
        /// <returns>
        ///     <c> true </c> if the item was removed, or <c> false </c> if the item is not contained in
        ///     the <see cref="IPhxMutableMultiMap{TKey,TValue}" />.
        /// </returns>
        public bool Remove(TKey key, TValue value);

        /// <summary> Removes all values associated with the specified key from the map. </summary>
        /// <param name="key"> The key to remove. </param>
        /// <returns>
        ///     <c> true </c> if the key was removed, or <c> false </c> if the key is not contained in
        ///     the <see cref="IPhxMutableMultiMap{TKey,TValue}" />.
        /// </returns>
        public bool RemoveAll(TKey key);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxMutableMultiMap{TKey,TValue}" />.
    /// </summary>
    public static class IPhxMutableMultiMapExtensions {
        /// <summary>
        ///     Gets the elements associated with the specified key if any exist, or else returns the
        ///     value provided by the given provider function.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <param name="defaultValue">
        ///     The function that provides the alternative value in cases when the key
        ///     is not found.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TValue" /> instance retrieved from the map or constructed using the
        ///     given provider function.
        /// </returns>
        public static IPhxMutableCollection<TValue> GetOrElse<TKey, TValue>(
                this IPhxMutableMultiMap<TKey, TValue> map,
                TKey key,
                Func<IPhxMutableCollection<TValue>> defaultValue
        ) {
            return map.Get(key).OrElse(defaultValue);
        }

        /// <summary>
        ///     Gets the elements associated with the specified key if any exist, or else returns an
        ///     empty collection.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <returns> A <typeparamref name="TValue" /> instance retrieved from the map or an empty collection. </returns>
        public static IPhxMutableCollection<TValue> GetOrEmpty<TKey, TValue>(
                this IPhxMutableMultiMap<TKey, TValue> map,
                TKey key
        ) {
            return map.Get(key).OrElse(() => PhxCollections.MutableListOf<TValue>());
        }

        /// <summary>
        ///     Gets the elements associated with the specified key if any exist, or else throws an
        ///     <see cref="InvalidOperationException" />.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <returns> A <typeparamref name="TValue" /> instance retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public static IPhxMutableCollection<TValue> GetOrThrow<TKey, TValue>(
                this IPhxMutableMultiMap<TKey, TValue> map,
                TKey key) {
            return map.Get(key).OrThrow(() => new InvalidOperationException($"Key {key} not found in map."));
        }

        /// <summary> Adds the values associated with each of the specified keys. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="values"> A collection of the key value pairs to set. </param>
        public static int AddAll<TKey, TValue>(
                this IPhxMutableMultiMap<TKey, TValue> map,
                params (TKey, TValue)[] values) {
            return map.AddAll(values.AsEnumerable());
        }

        /// <summary>
        ///     Gets the collection of elements associated with the specified key if any exist, or else
        ///     inserts and returns the provided default value.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <param name="defaultValue"> The alternative value in cases when the key is not found. </param>
        /// <returns>
        ///     A <typeparamref name="TValue" /> instance retrieved from the map or the provided default
        ///     value.
        /// </returns>
        public static IPhxMutableCollection<TValue> GetOrInsert<TKey, TValue>(
                this IPhxMutableMultiMap<TKey, TValue> map,
                TKey key,
                TValue defaultValue
        ) {
            return map.Get(key).OrElse(() => {
                _ = map.Add(key, defaultValue);
                return map[key];
            });
        }

        /// <summary>
        ///     Gets the collection of elements associated with the specified key if any exist, or else
        ///     inserts and returns the value provided by the given provider function.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <param name="defaultValue">
        ///     The function that provides the alternative value in cases when the key
        ///     is not found.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TValue" /> instance retrieved from the map or constructed using the
        ///     given provider function.
        /// </returns>
        public static IPhxMutableCollection<TValue> GetOrInsert<TKey, TValue>(
                this IPhxMutableMultiMap<TKey, TValue> map,
                TKey key,
                Func<TValue> defaultValue
        ) {
            return map.Get(key).OrElse(() => {
                _ = map.Add(key, defaultValue());
                return map[key];
            });
        }
    }
}
