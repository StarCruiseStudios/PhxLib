// -----------------------------------------------------------------------------
//  <copyright file="ImmutablePhxSet.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phx.Lang;
using static Phx.Collections.PhxCollections;

namespace Phx.Collections {
    using Phx.Debug;

    /// <summary>
    ///     An immutable collection that contains an unordered set of unique items.
    /// </summary>
    /// <typeparam name="T"> The type of item contained in the set. </typeparam>
    public sealed class ImmutablePhxSet<T> : IPhxSet<T>, IDebugDisplay {
        private readonly HashSet<T> internalSet;

        /// <inheritdoc />
        public int Count => internalSet.Count;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImmutablePhxSet{T}"/> class.
        /// </summary>
        public ImmutablePhxSet() {
            internalSet = new HashSet<T>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImmutablePhxSet{T}"/> class.
        /// </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxSet(IEnumerable<T> elements) {
            internalSet = new HashSet<T>(elements);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImmutablePhxSet{T}"/> class.
        /// </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public ImmutablePhxSet(params T[] elements) : this(elements.AsEnumerable()) { }

        /// <inheritdoc />
        public bool Contains(object item) {
            return item is T t && internalSet.Contains(t);
        }

        /// <inheritdoc />
        public bool ContainsAll(IEnumerable items) {
            foreach (var item in items) {
                if (item is not T t || !internalSet.Contains(t)) {
                    return false;
                }
            }
            return true;
        }

        /// <inheritdoc />
        public bool ContainsAny(Predicate<T> predicate) {
            return internalSet.Any(predicate.Invoke);
        }

        /// <inheritdoc />
        public bool ContainsAny(IEnumerable items) {
            foreach (var item in items) {
                if (item is T t && internalSet.Contains(t)) {
                    return true;
                }
            }
            return false;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() {
            return internalSet.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IPhxSet<T> GetIntersection(IEnumerable<T> other) {
            var newSet = internalSet.CopyToMutablePhxSet();
            newSet.Intersect(other);
            return newSet;
        }

        /// <inheritdoc />
        public IPhxSet<IPhxSet<T>> GetPowerSet() {
            var powerSet = MutableSetOf<IPhxSet<T>>();
            _ = powerSet.Add(internalSet.CopyToPhxSet());

            var setsToAdd = MutableSetOf<IPhxSet<T>>();
            foreach (var item in this) {
                setsToAdd.Clear();
                foreach (var set in powerSet) {
                    var subset = set.GetSubtraction(item);
                    _ = setsToAdd.Add(subset);
                }
                _ = powerSet.AddAll(setsToAdd);
            }

            return powerSet;
        }

        /// <inheritdoc />
        public IPhxSet<T> GetSubtraction(IEnumerable<T> other) {
            var newSet = internalSet.CopyToMutablePhxSet();
            newSet.Subtract(other);
            return newSet;
        }

        /// <inheritdoc />
        public IPhxSet<T> GetSymmetricSubtraction(IEnumerable<T> other) {
            var newSet = internalSet.CopyToMutablePhxSet();
            newSet.SymmetricSubtract(other);
            return newSet;
        }

        /// <inheritdoc />
        public IPhxSet<T> GetUnion(IEnumerable<T> other) {
            var newSet = internalSet.CopyToMutablePhxSet();
            newSet.Union(other);
            return newSet;
        }

        /// <inheritdoc />
        public bool IsEquivalent(IEnumerable<T> other) {
            return Count == other.Count() && ContainsAll(other);
        }

        /// <inheritdoc />
        public bool IsProperSubsetOf(IEnumerable<T> other) {
            return internalSet.IsProperSubsetOf(other);
        }

        /// <inheritdoc />
        public bool IsProperSupersetOf(IEnumerable<T> other) {
            return internalSet.IsProperSupersetOf(other);
        }

        /// <inheritdoc />
        public bool IsSubsetOf(IEnumerable<T> other) {
            return internalSet.IsSubsetOf(other);
        }

        /// <inheritdoc />
        public bool IsSupersetOf(IEnumerable<T> other) {
            return internalSet.IsSupersetOf(other);
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