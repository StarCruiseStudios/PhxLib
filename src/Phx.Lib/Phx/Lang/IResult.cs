// -----------------------------------------------------------------------------
//  <copyright file="IResult.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;

    /// <summary> Represents the successful or unsuccessful result of an operation. </summary>
    /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
    /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
    public interface IResult<out T, out E> {
        /// <summary> Gets a value indicating whether the operation was successful. </summary>
        /// <value> <c> true </c> if the operation was successful, otherwise false. </value>
        bool IsSuccess { get; }

        /// <summary> Gets a value indicating whether the operation failed. </summary>
        /// <value> <c> true </c> if the operation failed, otherwise false. </value>
        bool IsFailure { get; }

        /// <summary>
        ///     Indicates whether the <see cref="IResult{T, E}" /> is successful, and contains the given
        ///     successful value.
        /// </summary>
        /// <param name="predicate"> The predicate that defines the expected successful value. </param>
        /// <returns>
        ///     <c> true </c> if the result is successful and the given predicate succeeds, otherwise
        ///     <c> false </c>.
        /// </returns>
        bool Contains(Func<T, bool> predicate);

        /// <summary>
        ///     Indicates whether the <see cref="IResult{T, E}" /> failed, and contains the given error
        ///     value.
        /// </summary>
        /// <remarks>
        ///     This method compares the failure result to the given value using <c> Object.Equals() </c>
        ///     .
        /// </remarks>
        /// <param name="predicate"> The predicate that defines the expected error value. </param>
        /// <returns>
        ///     <c> true </c> if the result failed and the given predicate succeeds, otherwise
        ///     <c> false </c>.
        /// </returns>
        bool ContainsError(Func<E, bool> predicate);

        /// <summary>
        ///     Maps a <see cref="IResult{T, E}" /> instance to an instance of type
        ///     <see cref="IResult{T2, E}" /> using the provided converter function on a successful value.
        /// </summary>
        /// <remarks>
        ///     The converter function is only used if the <see cref="IResult{T, E}" /> is a successful
        ///     value. A failed result will pass the error type unchanged.
        /// </remarks>
        /// <param name="convert"> The converter function. </param>
        /// <typeparam name="T2"> The new type of the success result. </typeparam>
        /// <returns> A <see cref="IResult{T2, E}" /> instance constructed using the given converter function. </returns>
        IResult<T2, E> Map<T2>(Func<T, T2> convert);

        /// <summary>
        ///     Maps a <see cref="IResult{T, E}" /> instance to an instance of type
        ///     <see cref="IResult{T, E2}" /> using the provided converter function on a failure value.
        /// </summary>
        /// <remarks>
        ///     The converter function is only used if the <see cref="IResult{T, E}" /> is a failure
        ///     value. A successful result will pass the result type unchanged.
        /// </remarks>
        /// <param name="convert"> The converter function. </param>
        /// <typeparam name="E2"> The new type of the failure result. </typeparam>
        /// <returns> A <see cref="IResult{T, E2}" /> instance constructed using the given converter function. </returns>
        IResult<T, E2> MapError<E2>(Func<E, E2> convert);

        /// <summary>
        ///     Returns the result value contained in the <see cref="IResult{T, E}" /> if it was
        ///     successful, otherwise throws an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="message"> The message to provide to the new exception. </param>
        /// <returns> A <see cref="T" /> instance retrieved from the result. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the result is a failure. </exception>
        T OrThrow(string message = "The operation failed with an unexpected result");

        /// <summary>
        ///     Returns the result value contained in the <see cref="IResult{T, E}" /> if it was
        ///     successful, otherwise throws the exception provided by the given provider function.
        /// </summary>
        /// <typeparam name="Ex"> The type of exception that will be thrown. </typeparam>
        /// <param name="provideException">
        ///     The function that provides the exception to throw in cases when the
        ///     <see cref="IResult{T, E}" /> is a failure.
        /// </param>
        /// <returns> A <typeparamref name="T" /> instance retrieved from the result. </returns>
        /// <exception> <typeparamref name="Ex" /> thrown if the result is a failure. </exception>
        T OrThrow<Ex>(Func<E, Ex> provideException) where Ex : Exception;
    }

    /// <summary> Defines extension methods for the <see cref="IResult{T,E}" /> type. </summary>
    public static class IResultExtensions {
        /// <summary>
        ///     Returns the result value contained in the <see cref="IResult{T, E}" /> if it was
        ///     successful, otherwise returns the value provided by the given provider function.
        /// </summary>
        /// <param name="result"> The <see cref="IResult{T,E}" /> isntance. </param>
        /// <param name="provideAlternative">
        ///     The function that provides the alternative value in cases when
        ///     the <see cref="IResult{T, E}" /> is a failure.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="T" /> instance retrieved from the result or constructed using the
        ///     given provider function.
        /// </returns>
        public static T OrElse<T, E>(this IResult<T, E> result, Func<T> provideAlternative) {
            return (result is Success<T, E> s)
                    ? s.Result
                    : provideAlternative();
        }
    }
}
