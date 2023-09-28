// -----------------------------------------------------------------------------
//  <copyright file="ImmutablePhxMap.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Phx.Debug;
    using Phx.Lang;

    /// <summary> An immutable collection of key value pairs. </summary>
    /// <typeparam name="TKey"> The type of object used as a key. </typeparam>
    /// <typeparam name="TValue"> The type of object used as a value. </typeparam>
    public sealed class ImmutablePhxMap<TKey, TValue> : IPhxMap<TKey, TValue>, IDebugDisplay {
        private readonly IDictionary<TKey, TValue> internalMap;

        /// <inheritdoc />
        public TValue this[TKey key] {
            get => this.GetOrThrow(key);
        }

        /// <inheritdoc />
        public int Count => internalMap.Count;

        /// <inheritdoc />
        public IPhxCollection<IPhxKeyValuePair<TKey, TValue>> Entries => new PhxHashSet<IPhxKeyValuePair<TKey, TValue>>(
                from kvp in internalMap.CopyToPhxSet()
                select new PhxKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));

        /// <inheritdoc />
        public IPhxSet<TKey> Keys => internalMap.Keys.CopyToPhxSet();

        /// <inheritdoc />
        public IPhxCollection<TValue> Values => internalMap.Values.CopyToPhxList();

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        public ImmutablePhxMap() {
            internalMap = new Dictionary<TKey, TValue>();
        }

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxMap(IPhxMap<TKey, TValue> elements) {
            internalMap = elements.CopyToDictionary();
        }

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxMap(IDictionary<TKey, TValue> elements) {
            internalMap = new Dictionary<TKey, TValue>(elements);
        }

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxMap(IEnumerable<IPhxKeyValuePair<TKey, TValue>> elements) {
            internalMap = new Dictionary<TKey, TValue>(elements.Count());
            foreach (var element in elements) {
                internalMap.Add(element.Key, element.Value);
            }
        }

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxMap(params IPhxKeyValuePair<TKey, TValue>[] elements) : this(elements.AsEnumerable()) { }

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxMap(IEnumerable<(TKey, TValue)> elements) {
            internalMap = new Dictionary<TKey, TValue>();
            foreach (var element in elements) {
                internalMap.Add(element.Item1, element.Item2);
            }
        }

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxMap{TKey, TValue}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxMap(params (TKey, TValue)[] elements) : this(elements.AsEnumerable()) { }

        /// <inheritdoc />
        public int CountWhere(Predicate<IPhxKeyValuePair<TKey, TValue>> predicate) {
            return internalMap.Count((kvp) => predicate(kvp.CopyToKeyValuePair()));
        }

        /// <inheritdoc />
        public bool ContainsKey(TKey key) {
            return internalMap.ContainsKey(key);
        }

        /// <inheritdoc />
        public IResult<TValue, Unit> Get(TKey key) {
            if (internalMap.TryGetValue(key, out var val)) {
                return Result.Success<TValue, Unit>(val);
            }

            return Result.Failure<TValue, Unit>(Unit.UNIT);
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
