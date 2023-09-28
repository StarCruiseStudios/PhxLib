// -----------------------------------------------------------------------------
//  <copyright file="IOptional.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;

    /// <summary> An object which can be used to indicate a value that may or may not be present. </summary>
    /// <typeparam name="T"> The type of value contained by the <see cref="IOptional{T}" /> when non-empty. </typeparam>
    public interface IOptional<out T> {
        /// <summary> Gets a value indicating whether the <see cref="IOptional{T}" /> contains a value. </summary>
        /// <value> <c> true </c> when a value is present, otherwise <c> false </c>. </value>
        public bool IsPresent { get; }

        /// <summary> Gets a value indicating whether the <see cref="IOptional{T}" /> does not contain a value. </summary>
        /// <value> <c> true </c> when no value is present, otherwise <c> false </c>. </value>
        public bool IsEmpty { get; }

        /// <summary> Gets the value contained by the <see cref="IOptional{T}" /> if non-empty. </summary>
        /// <value> The value. </value>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if this method is invoked when this
        ///     <see cref="IOptional{T}" /> is empty.
        /// </exception>
        public T Value { get; }
    }

    /// <summary> Provides extension methods on the <see cref="IOptional{T}" /> type. </summary>
    public static class IOptionalExtensions {
        /// <summary>
        ///     InvokesIf a value is present in the <see cref="IOptional{T}" />, the specified action is
        ///     invoked with the contained value passed as an argument.
        /// </summary>
        /// <typeparam name="T"> The type of value contained in the input <see cref="IOptional{T}" />. </typeparam>
        /// <param name="optional"> The input <see cref="IOptional{T}" /> value. </param>
        /// <param name="action"> The action to invoke. </param>
        public static void IfPresent<T>(this IOptional<T> optional, Action<T> action) {
            if (optional.IsPresent) {
                action(optional.Value);
            }
        }

        /// <summary> Invokes the specified action if no value is contained in the <see cref="IOptional{T}" />. </summary>
        /// <param name="optional"> The input <see cref="IOptional{T}" /> value. </param>
        /// <param name="action"> The action to invoke. </param>
        public static void IfEmpty<T>(this IOptional<T> optional, Action action) {
            if (optional.IsEmpty) {
                action();
            }
        }

        /// <summary>
        ///     Applies the given mapping function to the value contained in the
        ///     <see cref="IOptional{T}" /> if one is present. Otherwise, an empty optional is returned.
        /// </summary>
        /// <typeparam name="T"> The type of value contained in the input <see cref="IOptional{T}" />. </typeparam>
        /// <typeparam name="U"> The type of value contained in the output <see cref="Optional{U}" />. </typeparam>
        /// <param name="optional"> The input <see cref="IOptional{T}" /> value. </param>
        /// <param name="mapper">
        ///     The mapping function applied to the value if the <see cref="IOptional{T}" />
        ///     is present.
        /// </param>
        public static IOptional<U> Map<T, U>(this IOptional<T> optional, Func<T, IOptional<U>> mapper)
                where U : notnull {
            if (optional.IsPresent) {
                return mapper(optional.Value);
            }

            return Optional<U>.EMPTY;
        }

        /// <summary>
        ///     Returns the value contained inside of the <see cref="IOptional{T}" /> if it is present.
        ///     Otherwise, the value computed by the given function is returned.
        /// </summary>
        /// <typeparam name="T"> The type of value contained in the input <see cref="IOptional{T}" />. </typeparam>
        /// <param name="optional"> The input <see cref="IOptional{T}" /> value. </param>
        /// <param name="other">
        ///     The function that computes the output value if the <see cref="IOptional{T}" />
        ///     is empty.
        /// </param>
        public static T OrElse<T>(this IOptional<T> optional, Func<T> other) {
            return optional.IsPresent
                    ? optional.Value
                    : other();
        }

        /// <summary>
        ///     Returns the value contained inside of the <see cref="IOptional{T}" /> if it is present.
        ///     Otherwise, the value computed by the given function is returned.
        /// </summary>
        /// <typeparam name="T"> The type of value contained in the input <see cref="IOptional{T}" />. </typeparam>
        /// <param name="optional"> The input <see cref="IOptional{T}" /> value. </param>
        /// <param name="other">
        ///     The function that computes the output value if the <see cref="IOptional{T}" />
        ///     is empty.
        /// </param>
        public static IOptional<T> OrTry<T>(this IOptional<T> optional, Func<IOptional<T>> other) {
            return optional.IsPresent
                    ? optional
                    : other();
        }

        /// <summary>
        ///     Returns the value contained inside of the <see cref="IOptional{T}" /> if it is present.
        ///     Otherwise, the exception returned by the given function is returned.
        /// </summary>
        /// <typeparam name="T"> The type of value contained in the input <see cref="IOptional{T}" />. </typeparam>
        /// <param name="optional"> The input <see cref="IOptional{T}" /> value. </param>
        /// <param name="exception">
        ///     The function that provides the exception to throw if the
        ///     <see cref="IOptional{T}" /> is empty.
        /// </param>
        /// <exception cref="Exception">
        ///     thrown if this method is invoked and the <see cref="IOptional{T}" />
        ///     is empty.
        /// </exception>
        public static T OrThrow<T>(this IOptional<T> optional, Func<Exception> exception) {
            return optional.IsPresent
                    ? optional.Value
                    : throw exception();
        }
    }
}
