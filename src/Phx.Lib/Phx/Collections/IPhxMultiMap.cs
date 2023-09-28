// -----------------------------------------------------------------------------
//  <copyright file="IPhxMultiMap.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Linq;
    using Phx.Lang;

    /// <summary> Represents a collection of mappings from keys to one or more values. </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    public interface IPhxMultiMap<TKey, out TValue> : IPhxContainer<IPhxKeyValuePair<TKey, IPhxCollection<TValue>>> {
        /// <summary>
        ///     Gets the elements associated with the specified key if any exist, or else throws an
        ///     <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A collection of <typeparamref name="TValue" /> instances retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public IPhxCollection<TValue> this[TKey key] { get; }

        /// <summary>
        ///     Gets the set of all key value pairs contained in this
        ///     <see cref="IPhxMultiMap{TKey, TValue}" /> instance.
        /// </summary>
        public IPhxCollection<IPhxKeyValuePair<TKey, IPhxCollection<TValue>>> Entries { get; }

        /// <summary>
        ///     Gets the set of keys contained in this <see cref="IPhxMultiMap{TKey, TValue}" />
        ///     instance.
        /// </summary>
        public IPhxSet<TKey> Keys { get; }

        /// <summary>
        ///     Gets a collection of all values contained in this
        ///     <see cref="IPhxMultiMap{TKey, TValue}" /> instance.
        /// </summary>
        public IPhxCollection<IPhxCollection<TValue>> Values { get; }

        /// <summary>
        ///     Gets the number of elements contained in all keys in the
        ///     <see cref="IPhxMultiMap{TKey, TValue}" />.
        /// </summary>
        public int ElementCount { get; }

        /// <summary>
        ///     Returns a value indicating whether this <see cref="IPhxMultiMap{TKey, TValue}" />
        ///     contains any values associated with the given key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> <c> true </c> if any value is associated with the key, otherwise <c> false </c>. </returns>
        public bool ContainsKey(TKey key);

        /// <summary> Gets the collection of elements associated with the specified key. </summary>
        /// <param name="key"> The key. </param>
        /// <returns>
        ///     A success result containing the collection of values associated with the key if found, or
        ///     a failure result if the key is not found.
        /// </returns>
        public IResult<IPhxCollection<TValue>, Unit> Get(TKey key);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of
    ///     <see cref="IPhxMultiMap{TKey, TValue}" />.
    /// </summary>
    public static class IPhxMultiMapExtensions {
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
        public static IPhxCollection<TValue> GetOrElse<TKey, TValue>(
                this IPhxMultiMap<TKey, TValue> map,
                TKey key,
                Func<IPhxCollection<TValue>> defaultValue
        ) {
            return map.Get(key).OrElse(defaultValue);
        }

        /// <summary>
        ///     Gets the first element associated with the specified key if any exist, or else returns
        ///     the value provided by the given provider function.
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
        public static TValue GetFirstOrElse<TKey, TValue>(
                this IPhxMultiMap<TKey, TValue> map,
                TKey key,
                Func<TValue> defaultValue
        ) {
            return (map.Get(key) is Success<IPhxCollection<TValue>, Unit> s && s.Result.IsNotEmpty())
                    ? s.Result.First()
                    : defaultValue();
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
        public static IPhxCollection<TValue> GetOrThrow<TKey, TValue>(this IPhxMultiMap<TKey, TValue> map, TKey key) {
            return map.Get(key).OrThrow();
        }

        /// <summary>
        ///     Gets the first elements associated with the specified key if any exist, or else throws an
        ///     <see cref="InvalidOperationException" />.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <returns> A <typeparamref name="TValue" /> instance retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public static TValue GetFirstOrThrow<TKey, TValue>(this IPhxMultiMap<TKey, TValue> map, TKey key) {
            return (map.Get(key) is Success<IPhxCollection<TValue>, Unit> s && s.Result.IsNotEmpty())
                    ? s.Result.First()
                    : throw new InvalidOperationException($"Key {key} has no value associated with it.");
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
        public static IPhxCollection<TValue> GetOrEmpty<TKey, TValue>(
                this IPhxMultiMap<TKey, TValue> map,
                TKey key
        ) {
            return map.Get(key).OrElse(PhxCollections.EmptyList<TValue>);
        }
    }
}
