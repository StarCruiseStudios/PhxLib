// -----------------------------------------------------------------------------
//  <copyright file="PhxArrayListMultiMap.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Phx.Debug;
    using Phx.Lang;

    /// <summary> A mutable collection of mappings from a key to a list of one or more values. </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    [DebuggerDisplay(DebugDisplay.DEBUGGER_DISPLAY_STRING)]
    public sealed class PhxArrayListMultiMap<TKey, TValue> : IPhxMutableMultiMap<TKey, TValue>, IDebugDisplay {
        private readonly IPhxMutableMap<TKey, IPhxMutableList<TValue>> internalMap;

        /// <inheritdoc />
        public IPhxCollection<TValue> this[TKey key] {
            get => this.GetOrThrow(key);
        }

        /// <inheritdoc />
        IPhxMutableCollection<TValue> IPhxMutableMultiMap<TKey, TValue>.this[TKey key] {
            get => this.GetOrThrow(key);
        }

        /// <inheritdoc />
        public IPhxCollection<IPhxKeyValuePair<TKey, IPhxCollection<TValue>>> Entries {
            get => (IPhxSet<IPhxKeyValuePair<TKey, IPhxCollection<TValue>>>)internalMap.Entries;
        }

        /// <inheritdoc />
        IPhxCollection<IPhxKeyValuePair<TKey, IPhxMutableCollection<TValue>>> IPhxMutableMultiMap<TKey, TValue>.
                Entries {
            get => (IPhxSet<IPhxKeyValuePair<TKey, IPhxMutableCollection<TValue>>>)internalMap.Entries;
        }

        /// <inheritdoc />
        public IPhxSet<TKey> Keys {
            get => internalMap.Keys;
        }

        /// <inheritdoc />
        public IPhxCollection<IPhxCollection<TValue>> Values {
            get => internalMap.Values;
        }

        /// <inheritdoc />
        IPhxCollection<IPhxMutableCollection<TValue>> IPhxMutableMultiMap<TKey, TValue>.Values {
            get => internalMap.Values;
        }

        /// <inheritdoc />
        public int Count {
            get { return internalMap.Values.Count(collection => collection.Count > 0); }
        }

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

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        public PhxArrayListMultiMap() {
            internalMap = PhxCollections.MutableMapOf<TKey, IPhxMutableList<TValue>>();
        }

        /// <inheritdoc />
        public int CountWhere(Predicate<IPhxKeyValuePair<TKey, IPhxCollection<TValue>>> predicate) {
            return internalMap.CountWhere(predicate);
        }

        /// <inheritdoc />
        public bool ContainsKey(TKey key) {
            return internalMap.ContainsKey(key);
        }

        /// <inheritdoc />
        public IOptional<IPhxCollection<TValue>> Get(TKey key) {
            return internalMap.Get(key);
        }

        /// <inheritdoc />
        IOptional<IPhxMutableCollection<TValue>> IPhxMutableMultiMap<TKey, TValue>.Get(TKey key) {
            return internalMap.Get(key);
        }

        /// <inheritdoc />
        public bool Add(TKey key, TValue value) {
            IPhxMutableList<TValue> list = internalMap.GetOrInsert(key, () => PhxCollections.MutableListOf<TValue>());
            return list.Add(value);
        }

        /// <inheritdoc />
        public int AddAll(TKey key, IEnumerable<TValue> values) {
            IPhxMutableList<TValue> list = internalMap.GetOrInsert(key, () => PhxCollections.MutableListOf<TValue>());
            return list.AddAll(values);
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
                IPhxMutableList<TValue> list =
                        internalMap.GetOrInsert(entry.Key, () => PhxCollections.MutableListOf<TValue>());
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
            bool removed = false;
            internalMap.Get(key).IfPresent((it) => {
                removed = it.Remove(value);
                if (it.Count == 0) {
                    internalMap.Remove(key);
                }
            });

            return removed;
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
