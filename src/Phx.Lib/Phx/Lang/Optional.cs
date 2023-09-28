// -----------------------------------------------------------------------------
//  <copyright file="Optional.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;
    using System.Text;

    /// <summary> An object which can be used to indicate a value that may or may not be present. </summary>
    /// <typeparam name="T"> The type of value contained by the <see cref="Optional{T}" /> when non-empty. </typeparam>
    public struct Optional<T> : IOptional<T> {
        private readonly T value;

        /// <inheritdoc />
        public bool IsPresent { get; }

        /// <inheritdoc />
        public bool IsEmpty {
            get => !IsPresent;
        }

        /// <inheritdoc />
        public T Value {
            get {
                return IsPresent
                        ? value
                        : throw new InvalidOperationException("Cannot get value of empty optional.");
            }
        }

        private Optional(T value, bool isPresent) {
            this.value = value;
            IsPresent = isPresent;
        }

        /// <inheritdoc />
        public override string ToString() {
            return new StringBuilder(base.ToString())
                    .Append(" { ")
                    .Append(IsPresent
                            ? value.ToStringSafe()
                            : "[Empty]")
                    .Append(" }")
                    .ToString();
        }

        /// <summary> Returns an <see cref="IOptional{T}" /> with the provided value if the condition is true. </summary>
        /// <param name="condition"> The condition to evaluate. </param>
        /// <param name="value"> The value to contain in the <see cref="IOptional{T}" />. </param>
        public static IOptional<T> If(bool condition, T value) {
            return condition
                    ? new Optional<T>(value, true)
                    : EMPTY;
        }

        /// <summary> Returns an <see cref="IOptional{T}" /> with the provided value if the condition is true. </summary>
        /// <param name="condition"> The condition to evaluate. </param>
        /// <param name="getValue">
        ///     A function that returns the value to contain in the
        ///     <see cref="IOptional{T}" />.
        /// </param>
        public static IOptional<T> If(bool condition, Func<T> getValue) {
            return condition
                    ? new Optional<T>(getValue(), true)
                    : EMPTY;
        }

        /// <summary> Returns an <see cref="IOptional{T}" /> with the provided value. </summary>
        /// <param name="value"> The value to contain in the <see cref="IOptional{T}" />. </param>
        public static IOptional<T> Of(T value) {
            return new Optional<T>(value, true);
        }

        /// <summary>
        ///     Returns an <see cref="IOptional{T}" /> with the provided non-null value, or
        ///     <see cref="Optional{T}.EMPTY" /> if the provided value is null.
        /// </summary>
        /// <param name="value"> The value to contain in the <see cref="IOptional{T}" />. </param>
        public static IOptional<T> OfNullable(T? value) {
            return (value == null)
                    ? EMPTY
                    : new Optional<T>(value, true);
        }

        /// <summary> An empty <see cref="IOptional{T}" /> of type <typeparamref name="T" />. </summary>
        public static readonly IOptional<T> EMPTY = new Optional<T>(default!, false);
    }
}
