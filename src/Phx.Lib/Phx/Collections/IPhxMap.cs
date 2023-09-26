// -----------------------------------------------------------------------------
//  <copyright file="IPhxMap.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using Phx.Lang;

    /// <summary>
    ///     Represents a collection of key value pairs.
    /// </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    public interface IPhxMap<TKey, out TValue> : IPhxContainer {
        /// <summary>
        ///     Gets the element associated with the specified key if one exists, or else throws an
        ///     <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A <typeparamref name="TValue"/> instance retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public TValue this[TKey key] { get; }
        
        /// <summary>
        ///     Gets the set of all key value pairs contained in this
        ///     <see cref="IPhxMap{TKey, TValue}"/> instance.
        /// </summary>
        public IPhxCollection<IPhxPair<TKey, TValue>> Entries { get; }
       
        /// <summary>
        ///     Gets the set of keys contained in this <see cref="IPhxMap{TKey, TValue}"/> instance.
        /// </summary>
        public IPhxSet<TKey> Keys { get; }
        
        /// <summary>
        ///     Gets a collection of all values contained in this
        ///     <see cref="IPhxMap{TKey, TValue}"/> instance.
        /// </summary>
        public IPhxCollection<TValue> Values { get; }

        /// <summary>
        ///    Returns a value indicating whether this <see cref="IPhxMap{TKey, TValue}"/> contains
        ///    a value associated with the given key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> <c>true</c> if a value is associated with the key, otherwise <c>false</c>. </returns>
        public bool ContainsKey(TKey key);
        
        /// <summary>
        ///     Gets the element associated with the specified key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A success result contianing the value associated with the key if found, or a
        ///           failure result if the key is not found. </returns>
        public IResult<TValue, Unit> Get(TKey key);
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of <see cref="IPhxMap{TKey, TValue}"/>.
    /// </summary>
    public static class IPhxMapExtensions {
        /// <summary>
        ///     Gets the element associated with the specified key if one exists, or else returns the value provided by
        ///     the given provider function.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <param name="defaultValue"> The function that provides the alternative value in cases when the key is not
        ///                             found. </param>
        /// <returns> A <typeparamref name="TValue"/> instance retrieved from the map or constructed using the given
        ///           provider function. </returns>
        public static TValue GetOrElse<TKey, TValue>(
                this IPhxMap<TKey, TValue> map,
                TKey key,
                Func<TValue> defaultValue) {
            return map.Get(key).OrElse(defaultValue);
        }

        /// <summary>
        ///     Gets the element associated with the specified key if one exists, or else throws an
        ///     <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
        /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
        /// <param name="map"> The collection to perform the operation on. </param>
        /// <param name="key"> The key. </param>
        /// <returns> A <typeparamref name="TValue"/> instance retrieved from the map. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the key is not found in the map. </exception>
        public static TValue GetOrThrow<TKey, TValue>(this IPhxMap<TKey, TValue> map, TKey key) {
            return map.Get(key).OrThrow(_ => { return new KeyNotFoundException($"Key '{key}' not found."); });
        }
    }
}
