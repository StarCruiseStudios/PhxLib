// -----------------------------------------------------------------------------
//  <copyright file="ImmutablePhxList.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Phx.Debug;
    using Phx.Lang;

    /// <summary> An immutable collection that contains an ordered list of items. </summary>
    /// <typeparam name="T"> The type of item contained in the collection. </typeparam>
    public sealed class ImmutablePhxList<T> : IPhxList<T>, IDebugDisplay {
        private readonly List<T> internalList;

        /// <inheritdoc />
        public T this[int index] {
            get => Get(index);
        }

        /// <inheritdoc />
        public int Count => internalList.Count;

        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxList{T}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxList(IEnumerable<T> elements) {
            internalList = new List<T>(elements);
        }
        /// <summary> Initializes a new instance of the <see cref="ImmutablePhxList{T}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxList(params T[] elements) : this(elements.AsEnumerable()) { }

        /// <inheritdoc />
        public int CountWhere(Predicate<T> predicate) {
            return internalList.Count(predicate.Invoke);
        }

        /// <inheritdoc />
        public bool Contains(object item) {
            return item is T t && internalList.Contains(t);
        }

        /// <inheritdoc />
        public bool ContainsAll(IEnumerable items) {
            foreach (var item in items) {
                if (item is not T t || !internalList.Contains(t)) {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public bool ContainsAny(Predicate<T> predicate) {
            return internalList.Any(predicate.Invoke);
        }

        /// <inheritdoc />
        public bool ContainsAny(IEnumerable items) {
            foreach (var item in items) {
                if (item is T t && internalList.Contains(t)) {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public T Get(int index) {
            this.RequireIndexInBounds(index);
            return internalList[index];
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() {
            return internalList.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() {
            return internalList.GetEnumerator();
        }

        /// <inheritdoc />
        public IOptional<int> IndexOfFirst(object item) {
            if (item is not T i) {
                return Optional<int>.EMPTY;
            }

            var index = internalList.IndexOf(i);
            return Optional<int>.If(index >= 0, index);
        }

        /// <inheritdoc />
        public IOptional<int> IndexOfFirst(Predicate<T> predicate) {
            var index = internalList.FindIndex(predicate);
            return Optional<int>.If(index >= 0, index);
        }

        /// <inheritdoc />
        public IOptional<int> IndexOfLast(object item) {
            if (item is not T i) {
                return Optional<int>.EMPTY;
            }

            var index = internalList.LastIndexOf(i);
            return Optional<int>.If(index >= 0, index);
        }

        /// <inheritdoc />
        public IOptional<int> IndexOfLast(Predicate<T> predicate) {
            var index = internalList.FindLastIndex(predicate);
            return Optional<int>.If(index >= 0, index);
        }

        /// <inheritdoc />
        public IOptional<int> IndexOfNext(object item, int startingIndex) {
            this.RequireIndexInBounds(startingIndex);
            if (item is not T i) {
                return Optional<int>.EMPTY;
            }

            var index = internalList.IndexOf(i, startingIndex);
            return Optional<int>.If(index >= 0, index);
        }

        /// <inheritdoc />
        public IOptional<int> IndexOfNext(Predicate<T> predicate, int startingIndex) {
            this.RequireIndexInBounds(startingIndex);
            var index = internalList.FindIndex(startingIndex, predicate);
            return Optional<int>.If(index >= 0, index);
        }

        /// <inheritdoc />
        public string ToDebugDisplay() {
            StringBuilder builder = new StringBuilder(GetType().Name).Append(" [ ");
            foreach (var element in this) {
                _ = builder.Append(element).Append(" ");
            }

            return builder.Append("]").ToString();
        }

        /// <inheritdoc />
        public override string ToString() {
            return ToDebugDisplay();
        }
    }
}
