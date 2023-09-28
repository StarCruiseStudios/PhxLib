// -----------------------------------------------------------------------------
//  <copyright file="IPhxKeyValuePair.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    /// <summary> Represents a readonly key value pair. </summary>
    /// <typeparam name="TKey"> The key. </typeparam>
    /// <typeparam name="TValue"> The value. </typeparam>
    public interface IPhxKeyValuePair<out TKey, out TValue> {
        /// <summary> Gets the key. </summary>
        public TKey Key { get; }

        /// <summary> Gets the value. </summary>
        public TValue Value { get; }
    }

    /// <summary> Extension methods for the <see cref="IPhxKeyValuePair{TKey,TValue}" /> type. </summary>
    public static class IPhxPairExtensions {
        /// <summary> Deconstructs the pair into its key and value. </summary>
        /// <param name="keyValuePair"> The pair to deconstruct. </param>
        /// <param name="key"> The returned key. </param>
        /// <param name="value"> The returned value. </param>
        /// <typeparam name="TKey"> The type of the key. </typeparam>
        /// <typeparam name="TValue"> The type of the value. </typeparam>
        public static void Deconstruct<TKey, TValue>(
                this IPhxKeyValuePair<TKey, TValue> keyValuePair,
                out TKey key,
                out TValue value
        ) {
            key = keyValuePair.Key;
            value = keyValuePair.Value;
        }
    }
}
