﻿// -----------------------------------------------------------------------------
//  <copyright file="PhxMultiMap.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;
    using System.Text;
    using Phx.Debug;
    using Phx.Lang;

    /// <summary>
    ///     A mutable collection of mappings from a key to a list of one or more values.
    /// </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    public sealed class PhxArrayListMultiMap<TKey, TValue> : IMutablePhxMultiMap<TKey, TValue>, IDebugDisplay {
        private readonly IMutablePhxMap<TKey, IMutablePhxList<TValue>> internalMap;

        /// <inheritdoc />
        public IPhxCollection<TValue> this[TKey key] { get => this.GetOrThrow(key); }

        /// <inheritdoc />
        public IPhxCollection<IPhxPair<TKey, IPhxCollection<TValue>>> Entries {
            get => (IPhxSet<IPhxPair<TKey, IPhxCollection<TValue>>>) internalMap.Entries;
        }

        /// <inheritdoc />
        public IPhxSet<TKey> Keys { get => internalMap.Keys; }

        /// <inheritdoc />
        public IPhxCollection<IPhxCollection<TValue>> Values { get => internalMap.Values; }

        /// <inheritdoc />
        public int KeyCount { get { return internalMap.Count; } }

        /// <inheritdoc />
        public int ElementCount {
            get {
                int count = 0;
                foreach (var valueCollection in internalMap.Values) {
                    count += valueCollection.Count;
                }

                return count;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}"/> class.
        /// </summary>
        public PhxArrayListMultiMap() {
            internalMap = PhxCollections.MutableMapOf<TKey, IMutablePhxList<TValue>>();
        }

        /// <inheritdoc />
        public bool ContainsKey(TKey key) {
            return internalMap.ContainsKey(key);
        }

        /// <inheritdoc />
        public IResult<IPhxCollection<TValue>, Unit> Get(TKey key) {
            return internalMap.Get(key).Map<IPhxCollection<TValue>>(val => val);
        }

        /// <inheritdoc />
        public bool Add(TKey key, TValue value) {
            IMutablePhxList<TValue> list = internalMap.GetOrInsert(key, () => PhxCollections.MutableListOf<TValue>());
            return list.Add(value);
        }

        /// <inheritdoc />
        public int AddAll(IPhxMap<TKey, TValue> values) {
            int count = 0;
            foreach (var entry in values.Entries) {
                if (Add(entry.Key, entry.Value)) {
                    count++;
                }
            }
            return count;
        }

        /// <inheritdoc />
        public int AddAll(IPhxMultiMap<TKey, TValue> values) {
            int count = 0;
            foreach (var entry in values.Entries) {
                IMutablePhxList<TValue> list = internalMap.GetOrInsert(entry.Key, () => PhxCollections.MutableListOf<TValue>());
                count += list.AddAll(entry.Value);
            }

            return count;
        }

        /// <inheritdoc />
        public int AddAll(IEnumerable<(TKey, TValue)> values) {
            int count = 0;
            foreach (var entry in values) {
                if (Add(entry.Item1, entry.Item2)) {
                    count++;
                }
            }
            return count;
        }

        /// <inheritdoc />
        public void Clear() {
            internalMap.Clear();
        }

        /// <inheritdoc />
        public bool Remove(TKey key, TValue value) {
            if (internalMap.Get(key) is Success<IMutablePhxList<TValue>, Unit> success) {
                return success.Result.Remove(value);
            }
            return false;
        }

        /// <inheritdoc />
        public bool RemoveAll(TKey key) {
            return internalMap.Remove(key);
        }

        /// <inheritdoc />
        public string ToDebugDisplay() {
            StringBuilder builder = new StringBuilder(GetType().Name).Append(" [ ");
            foreach (var entry in Entries) {
                _ = builder.Append("{ ").Append(entry.Key.ToDebugDisplayString())
                        .Append(":").Append(entry.Value.ToDebugDisplayString())
                        .Append(" } ");
            }
            return builder.Append("]").ToString();
        }

        /// <inheritdoc />
        public override string ToString() {
            return ToDebugDisplay();
        }
    }
}
