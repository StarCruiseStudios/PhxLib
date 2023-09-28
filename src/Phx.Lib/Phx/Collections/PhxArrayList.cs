// -----------------------------------------------------------------------------
//  <copyright file="PhxArrayList.cs" company="Star Cruise Studios LLC">
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

    /// <summary> A mutable collection that contains an ordered list of items. </summary>
    /// <typeparam name="T"> The type of item contained in the set. </typeparam>
    public sealed class PhxArrayList<T> : IPhxMutableList<T>, IDebugDisplay {
        private readonly List<T> internalList;

        /// <inheritdoc />
        public T this[int index] {
            get => Get(index);
            set => Set(index, value);
        }

        /// <inheritdoc />
        public int Count => internalList.Count;

        /// <summary> Initializes a new instance of the <see cref="PhxArrayList{T}" /> class. </summary>
        public PhxArrayList() {
            internalList = new List<T>();
        }

        /// <summary> Initializes a new instance of the <see cref="PhxArrayList{T}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxArrayList(IEnumerable<T> elements) {
            internalList = new List<T>(elements);
        }

        /// <summary> Initializes a new instance of the <see cref="PhxArrayList{T}" /> class. </summary>
        /// <param name="elements"> The elements to initialize the collection with. </param>
        public PhxArrayList(params T[] elements) : this(elements.AsEnumerable()) { }

        /// <inheritdoc />
        public int CountWhere(Predicate<T> predicate) {
            return internalList.Count(predicate.Invoke);
        }

        /// <inheritdoc />
        public bool Add(T item) {
            internalList.Add(item);
            return true;
        }

        /// <inheritdoc />
        public int AddAll(IEnumerable<T> items) {
            internalList.AddRange(items);
            return items.Count();
        }

        /// <inheritdoc />
        public void Clear() {
            internalList.Clear();
        }

        /// <inheritdoc />
        public bool Contains(T item) {
            return internalList.Contains(item);
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
        public bool ContainsAll(IEnumerable<T> items) {
            return items.All(internalList.Contains);
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
        public bool ContainsAny(IEnumerable<T> items) {
            return items.Any(internalList.Contains);
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
        public IResult<int, Unit> IndexOfFirst(object item) {
            if (item is not T i) {
                return Result.Failure<int, Unit>(Unit.UNIT);
            }

            var index = internalList.IndexOf(i);
            return index < 0
                    ? Result.Failure<int, Unit>(Unit.UNIT)
                    : Result.Success<int, Unit>(index);
        }

        /// <inheritdoc />
        public IResult<int, Unit> IndexOfFirst(Predicate<T> predicate) {
            var index = internalList.FindIndex(predicate);
            return index < 0
                    ? Result.Failure<int, Unit>(Unit.UNIT)
                    : Result.Success<int, Unit>(index);
        }

        /// <inheritdoc />
        public IResult<int, Unit> IndexOfLast(object item) {
            if (item is not T i) {
                return Result.Failure<int, Unit>(Unit.UNIT);
            }

            var index = internalList.LastIndexOf(i);
            return index < 0
                    ? Result.Failure<int, Unit>(Unit.UNIT)
                    : Result.Success<int, Unit>(index);
        }

        /// <inheritdoc />
        public IResult<int, Unit> IndexOfLast(Predicate<T> predicate) {
            var index = internalList.FindLastIndex(predicate);
            return index < 0
                    ? Result.Failure<int, Unit>(Unit.UNIT)
                    : Result.Success<int, Unit>(index);
        }

        /// <inheritdoc />
        public IResult<int, Unit> IndexOfNext(object item, int startingIndex) {
            this.RequireIndexInBounds(startingIndex);
            if (item is not T i) {
                return Result.Failure<int, Unit>(Unit.UNIT);
            }

            var index = internalList.IndexOf(i, startingIndex);
            return index < 0
                    ? Result.Failure<int, Unit>(Unit.UNIT)
                    : Result.Success<int, Unit>(index);
        }

        /// <inheritdoc />
        public IResult<int, Unit> IndexOfNext(Predicate<T> predicate, int startingIndex) {
            this.RequireIndexInBounds(startingIndex);
            var index = internalList.FindIndex(startingIndex, predicate);
            return index < 0
                    ? Result.Failure<int, Unit>(Unit.UNIT)
                    : Result.Success<int, Unit>(index);
        }

        /// <inheritdoc />
        public void Insert(int index, T item) {
            if (index == Count) {
                internalList.Add(item);
            } else {
                this.RequireIndexInBounds(index);
                internalList.Insert(index, item);
            }
        }

        /// <inheritdoc />
        public void InsertAll(int index, IEnumerable<T> items) {
            if (index == Count) {
                internalList.AddRange(items);
            } else {
                this.RequireIndexInBounds(index);
                internalList.InsertRange(index, items);
            }
        }

        /// <inheritdoc />
        public bool Remove(T item) {
            return internalList.Remove(item);
        }

        /// <inheritdoc />
        public int RemoveAll(Predicate<T> predicate) {
            return internalList.RemoveAll(predicate.Invoke);
        }

        /// <inheritdoc />
        public int RemoveAll(IEnumerable<T> items) {
            return internalList.RemoveAll(items.Contains);
        }

        /// <inheritdoc />
        public void RemoveAt(int index) {
            this.RequireIndexInBounds(index);
            internalList.RemoveAt(index);
        }

        /// <inheritdoc />
        public int RetainOnly(Predicate<T> predicate) {
            return internalList.RemoveAll((item) => !predicate(item));
        }

        /// <inheritdoc />
        public int RetainOnly(IEnumerable<T> items) {
            return internalList.RemoveAll((item) => !items.Contains(item));
        }

        /// <inheritdoc />
        public void Set(int index, T value) {
            this.RequireIndexInBounds(index);
            internalList[index] = value;
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
