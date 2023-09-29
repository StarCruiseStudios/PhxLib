// -----------------------------------------------------------------------------
//  <copyright file="PhxHashMap.cs" company="Star Cruise Studios LLC">
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
    using static PhxCollections;

    /// <summary> A mutable collection of key value pairs. </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    [DebuggerDisplay(DebugDisplay.DEBUGGER_DISPLAY_STRING)]
    public sealed class PhxHashMap<TKey, TValue> : IPhxMutableMap<TKey, TValue>, IDebugDisplay {
        private readonly IDictionary<TKey, TValue> internalMap;

        /// <inheritdoc cref="IPhxMutableMap{TKey,TValue}.this" />
        public TValue this[TKey key] {
            get => this.GetOrThrow(key);
            set => Set(key, value);
        }

        /// <inheritdoc />
        public int Count => internalMap.Count;

        /// <inheritdoc />
        public IPhxCollection<IPhxKeyValuePair<TKey, TValue>> Entries => SetOf(
                from kvp in internalMap.CopyToPhxSet()
                select (IPhxKeyValuePair<TKey, TValue>)new PhxKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));

        /// <inheritdoc />
        public IPhxSet<TKey> Keys => internalMap.Keys.CopyToPhxSet();

        /// <inheritdoc />
        public IPhxCollection<TValue> Values => internalMap.Values.CopyToPhxList();

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        public PhxHashMap() {
            internalMap = new Dictionary<TKey, TValue>();
        }

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxHashMap(IEnumerable<IPhxKeyValuePair<TKey, TValue>> elements) {
            IEnumerable<IPhxKeyValuePair<TKey, TValue>> phxKeyValuePairs =
                    elements as IPhxKeyValuePair<TKey, TValue>[] ?? elements.ToArray();
            internalMap = new Dictionary<TKey, TValue>(phxKeyValuePairs.Count());
            foreach (var element in phxKeyValuePairs) {
                internalMap.Add(element.Key, element.Value);
            }
        }

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxHashMap(IPhxMap<TKey, TValue> elements) {
            internalMap = elements.CopyToDictionary();
        }

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxHashMap(IDictionary<TKey, TValue> elements) {
            internalMap = new Dictionary<TKey, TValue>(elements);
        }

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxHashMap(params IPhxKeyValuePair<TKey, TValue>[] elements) : this(elements.AsEnumerable()) { }

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxHashMap(IEnumerable<(TKey, TValue)> elements) {
            internalMap = new Dictionary<TKey, TValue>();
            foreach (var element in elements) {
                internalMap.Add(element.Item1, element.Item2);
            }
        }

        /// <summary> Initializes a new instance of the <see cref="PhxHashMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxHashMap(params (TKey, TValue)[] elements) : this(elements.AsEnumerable()) { }

        /// <inheritdoc />
        public int CountWhere(Predicate<IPhxKeyValuePair<TKey, TValue>> predicate) {
            return internalMap.Count((kvp) => predicate(kvp.CopyToKeyValuePair()));
        }

        /// <inheritdoc />
        public void Clear() {
            internalMap.Clear();
        }

        /// <inheritdoc />
        public bool ContainsKey(TKey key) {
            return internalMap.ContainsKey(key);
        }

        /// <inheritdoc />
        public IOptional<TValue> Get(TKey key) {
            return Optional.If(internalMap.TryGetValue(key, out var val), val);
        }

        /// <inheritdoc />
        public bool Remove(TKey key) {
            return internalMap.Remove(key);
        }

        /// <inheritdoc />
        public int RemoveAll(IEnumerable<TKey> keys) {
            int numRemoved = 0;
            foreach (var key in keys) {
                if (internalMap.Remove(key)) {
                    numRemoved++;
                }
            }

            return numRemoved;
        }

        /// <inheritdoc />
        public int RetainOnly(IEnumerable<TKey> keys) {
            var keysToRemove = MutableSetOf<TKey>();
            IEnumerable<TKey> enumerable = keys as TKey[] ?? keys.ToArray();
            foreach (var entry in internalMap) {
                if (!enumerable.Contains(entry.Key)) {
                    _ = keysToRemove.Add(entry.Key);
                }
            }

            return RemoveAll(keysToRemove);
        }

        /// <inheritdoc />
        public void Set(TKey key, TValue value) {
            internalMap[key] = value;
        }

        /// <inheritdoc />
        public void SetAll(IPhxMap<TKey, TValue> values) {
            foreach (var entry in values.Entries) {
                internalMap[entry.Key] = entry.Value;
            }
        }

        /// <inheritdoc />
        public void SetAll(IEnumerable<(TKey, TValue)> values) {
            foreach (var (key, value) in values) {
                internalMap[key] = value;
            }
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
