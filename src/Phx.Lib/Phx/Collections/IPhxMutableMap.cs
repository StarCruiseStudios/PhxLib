// -----------------------------------------------------------------------------
//  <copyright file="IPhxMutableMap.cs" company="Star Cruise Studios LLC">
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

    /// <summary> Represents a mutable collection of key value pairs. </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    public interface IPhxMutableMap<TKey, TValue> : IPhxMap<TKey, TValue> {
        /// <summary>
        ///     Adds or updates the value associated with the specified key, gets the value if one
        ///     exists, or else throws an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A <typeparamref name="TValue" /> instance retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public new TValue this[TKey key] { get; set; }

        /// <summary> Removes all elements from the <see cref="IPhxMutableMap{TKey,TValue}" />. </summary>
        public void Clear();

        /// <summary> Removes the specified key from the map. </summary>
        /// <param name="key"> The key to remove. </param>
        /// <returns>
        ///     <c> true </c> if the key was removed, or <c> false </c> if the key is not contained in
        ///     the <see cref="IPhxMutableMap{TKey,TValue}" />.
        /// </returns>
        public bool Remove(TKey key);

        /// <summary> Removes all of the specified keys from the <see cref="IPhxMutableMap{TKey,TValue}" />. </summary>
        /// <param name="keys"> The keys to remove. </param>
        /// <returns> The number of keys that were removed by the operation. </returns>
        public int RemoveAll(IEnumerable<TKey> keys);

        /// <summary>
        ///     Removes all values from the <see cref="IPhxMutableMap{TKey,TValue}" /> except for the
        ///     keys contained in the specified collection.
        /// </summary>
        /// <param name="keys"> The keys to retain. </param>
        /// <returns> The number of keys that were removed by the operation. </returns>
        public int RetainOnly(IEnumerable<TKey> keys);

        /// <summary> Adds or updates the value associated with the specified key. </summary>
        /// <param name="key"> The key. </param>
        /// <param name="value"> The value. </param>
        public void Set(TKey key, TValue value);

        /// <summary> Adds or updates the value associated with each of the specified keys. </summary>
        /// <param name="values"> A map containing the key value pairs to set. </param>
        public void SetAll(IPhxMap<TKey, TValue> values);

        /// <summary> Adds or updates the value associated with each of the specified keys. </summary>
        /// <param name="values"> A collection of the key value pairs to set. </param>
        public void SetAll(IEnumerable<(TKey, TValue)> values);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxMap{TKey, TValue}" />.
    /// </summary>
    public static class IPhxMutableMapExtensions {
        /// <summary>
        ///     Gets the element associated with the specified key if one exists, or else inserts and
        ///     returns the provided default value.
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
        public static TValue GetOrInsert<TKey, TValue>(
                this IPhxMutableMap<TKey, TValue> map,
                TKey key,
                TValue defaultValue) {
            return map.Get(key).OrElse(() => {
                map.Set(key, defaultValue);
                return defaultValue;
            });
        }

        /// <summary>
        ///     Gets the element associated with the specified key if one exists, or else inserts and
        ///     returns the value provided by the given provider function.
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
        public static TValue GetOrInsert<TKey, TValue>(
                this IPhxMutableMap<TKey, TValue> map,
                TKey key,
                Func<TValue> defaultValue
        ) {
            return map.Get(key).OrElse(() => {
                var d = defaultValue();
                map.Set(key, d);
                return d;
            });
        }

        /// <summary> Removes all of the specified keys from the <see cref="IPhxMutableMap{TKey,TValue}" />. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="keys"> The keys to remove. </param>
        /// <returns> The number of keys that were removed by the operation. </returns>
        public static int RemoveAll<TKey, TValue>(this IPhxMutableMap<TKey, TValue> map, params TKey[] keys) {
            return map.RemoveAll(keys.AsEnumerable());
        }

        /// <summary>
        ///     Removes all values from the <see cref="IPhxMutableMap{TKey,TValue}" /> except for the
        ///     keys contained in the specified collection.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="keys"> The keys to retain. </param>
        /// <returns> The number of keys that were removed by the operation. </returns>
        public static int RetainOnly<TKey, TValue>(this IPhxMutableMap<TKey, TValue> map, params TKey[] keys) {
            return map.RetainOnly(keys.AsEnumerable());
        }

        /// <summary> Adds or updates the value associated with each of the specified keys. </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="values"> A collection of the key value pairs to set. </param>
        public static void SetAll<TKey, TValue>(this IPhxMutableMap<TKey, TValue> map, params (TKey, TValue)[] values) {
            map.SetAll(values.AsEnumerable());
        }
    }
}
