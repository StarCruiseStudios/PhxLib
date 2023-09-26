// -----------------------------------------------------------------------------
//  <copyright file="Result.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using static Phx.Lang.Unit;

namespace Phx.Lang {
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Phx.Dev;

    /// <summary> Represents the successful or unsuccessful result of an operation. </summary>
    /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
    /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
    public abstract class Result<T, E> : IResult<T, E> {
        /// <inheritdoc />
        public bool IsSuccess { get; }

        /// <inheritdoc />
        public bool IsFailure => !IsSuccess;

        internal Result(bool isSuccess) {
            IsSuccess = isSuccess;
        }

        /// <summary> Indicates whether the <see cref="Result{T, E}" /> is successful, and contains the given successful value. </summary>
        /// <param name="expected"> The expected successful value. </param>
        /// <returns>
        ///     <c> true </c> if the result is successful and contains the given value, or <c> false </c> if the operation
        ///     failed, or contains a different result.
        /// </returns>
        /// <remarks> This method compares the successful result to the given value using <c> Object.Equals() </c>. </remarks>
        [Obsolete("Use Contains(Func<T, bool>) instead.")]
        public bool Contains(T expected) {
            return this is Success<T, E> s
                    && Equals(s.Result, expected);
        }
        
        /// <inheritdoc />
        public bool Contains(Func<T, bool> predicate) {
            return this is Success<T, E> s
                    && predicate(s.Result);
        }

        /// <summary> Indicates whether the <see cref="Result{T, E}" /> failed, and contains the given error value. </summary>
        /// <remarks> This method compares the failure result to the given value using <c> Object.Equals() </c>. </remarks>
        /// <param name="expected"> The expected error value. </param>
        /// <returns>
        ///     <c> true </c> if the result failed and contains the given value, or <c> false </c> if the operation
        ///     succeeded, or contains a different error.
        /// </returns>
        [Obsolete("Use ContainsError(Func<E, bool>) instead.")]
        public bool ContainsError(E expected) {
            return this is Failure<T, E> f
                    && Equals(f.Error, expected);
        }

        /// <inheritdoc />
        public bool ContainsError(Func<E, bool> predicate) {
            return this is Failure<T, E> f
                    && predicate(f.Error);
        }

        
        /// <inheritdoc />
        [SuppressMessage("General", "RCS1079", Justification = "Unreachable default case.")]
        public IResult<T2, E> Map<T2>(Func<T, T2> convert) {
            return this switch {
                Success<T, E> s => Result.Success<T2, E>(convert(s.Result)),
                Failure<T, E> f => Result.Failure<T2, E>(f.Error),
#pragma warning disable RCS1140
                // Add exception to documentation comment: NotSupportedException should never be thrown.
                _ => throw new NotSupportedException(GetType().AssemblyQualifiedName),
#pragma warning restore RCS1140
            };
        }

       
        /// <inheritdoc />
        [SuppressMessage("General", "RCS1079", Justification = "Unreachable default case.")]
        public IResult<T, E2> MapError<E2>(Func<E, E2> convert) {
            return this switch {
                Success<T, E> s => Result.Success<T, E2>(s.Result),
                Failure<T, E> f => Result.Failure<T, E2>(convert(f.Error)),
#pragma warning disable RCS1140
                // Add exception to documentation comment: NotSupportedException should never be thrown.
                _ => throw new NotSupportedException(GetType().AssemblyQualifiedName),
#pragma warning restore RCS1140
            };
        }

        /// <summary>
        ///     Returns the result value contained in the <see cref="Result{T, E}" /> if it was successful, otherwise returns
        ///     the provided default value.
        /// </summary>
        /// <param name="defaultValue"> The alternative value in cases when the <see cref="Result{T, E}" /> is a failure. </param>
        /// <returns> A <typeparamref name="T" /> instance retrieved from the result or the provided default value. </returns>
        [Obsolete("Use OrElse(Func<T>) instead.")]
        public T OrDefault(T defaultValue) {
            return (this is Success<T, E> s)
                    ? s.Result
                    : defaultValue;
        }

        
        /// <inheritdoc />
        [SuppressMessage("General", "RCS1079", Justification = "Unreachable default case.")]
        public T OrThrow(string message = "The operation failed with an unexpected result") {
            return this switch {
                Success<T, E> s => s.Result,
                Failure<T, E> f => throw new InvalidOperationException($"{message}: {f.Error}"),
#pragma warning disable RCS1140
                // Add exception to documentation comment: NotSupportedException should never be thrown.
                _ => throw new NotImplementedException(GetType().AssemblyQualifiedName),
#pragma warning restore RCS1140
            };
        }

        /// <summary>
        ///     Returns the result value contained in the <see cref="Result{T, E}" /> if it was successful, otherwise throws
        ///     the exception provided by the given provider function.
        /// </summary>
        /// <typeparam name="Ex"> The type of exception that will be thrown. </typeparam>
        /// <param name="provideException">
        ///     The function that provides the exception to throw in cases when the
        ///     <see cref="Result{T, E}" /> is a failure.
        /// </param>
        /// <returns> A <typeparamref name="T" /> instance retrieved from the result. </returns>
        /// <exception> <typeparamref name="EX" /> thrown if the result is a failure. </exception>
        [Obsolete("Use OrThrow(Func<E, Ex>) instead.")]
        public T OrThrow<Ex>(Func<Result<T, E>, Ex> provideException) where Ex : Exception {
            return (this is Success<T, E> s)
                    ? s.Result
                    : throw provideException(this);
        }
        
        /// <inheritdoc />
        public T OrThrow<Ex>(Func<E, Ex> provideException) where Ex : Exception {
            return (this is Success<T, E> s)
                    ? s.Result
                    : throw provideException(((Failure<T, E>) this).Error);
        }
    }

    /// <summary> Contains static and extesion functions for the <see cref="Result{T, E}" /> class. </summary>
    public static class Result {
        /// <summary> Constructs a new Success <see cref="Result{T, E}" /> instance containing the given result value. </summary>
        /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
        /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
        /// <param name="result"> The result. </param>
        public static Result<T, E> Success<T, E>(T result) {
            return new Success<T, E>(result);
        }

        /// <summary> Constructs a new Success <see cref="Result{Unit, E}" /> instance. </summary>
        /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
        public static Result<Unit, E> Success<E>() {
            return new Success<Unit, E>(UNIT);
        }

        /// <summary> Constructs a new Success <see cref="Result{Unit, Exception}" /> instance. </summary>
        public static Result<Unit, Exception> Success() {
            return new Success<Unit, Exception>(UNIT);
        }

        /// <summary> Constructs a new Success <see cref="Result{T, Exception}" /> instance containing the given result value. </summary>
        /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
        /// <param name="result"> The result. </param>
        public static Result<T, Exception> Success<T>(T result) {
            return new Success<T, Exception>(result);
        }

        /// <summary> Constructs a new Failure <see cref="Result{T, E}" /> instance containing the given error value. </summary>
        /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
        /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
        /// <param name="error"> The error. </param>
        public static Result<T, E> Failure<T, E>(E error) {
            return new Failure<T, E>(error);
        }

        /// <summary> Constructs a new Failure <see cref="Result{Unit, E}" /> instance containing the given error value. </summary>
        /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
        /// <param name="error"> The error. </param>
        public static Result<Unit, E> Failure<E>(E error) {
            return new Failure<Unit, E>(error);
        }

        /// <summary> Constructs a new Failure <see cref="Result{Unit, Exception}" /> instance containing the given error value. </summary>
        /// <param name="error"> The error. </param>
        public static Result<Unit, Exception> Failure(Exception error) {
            return new Failure<Unit, Exception>(error);
        }

        /// <summary> Constructs a new Failure <see cref="Result{T, Exception}" /> instance containing the given error value. </summary>
        /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
        /// <param name="error"> The error. </param>
        public static Result<T, Exception> Failure<T>(Exception error) {
            return new Failure<T, Exception>(error);
        }
    }

    /// <summary> Represents the successful result of an operation. </summary>
    /// <seealso cref="Result{T, E}" />
    /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
    /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
    public sealed class Success<T, E> : Result<T, E> {
        /// <summary> Gets the result of the successful operation. </summary>
        /// <value> The result value returned by the successful operation. </value>
        public T Result { get; }

        internal Success(T result) : base(true) {
            Result = result;
        }
    }

    /// <summary> Represents the unsuccessful result of an operation. </summary>
    /// <seealso cref="Result{T, E}" />
    /// <typeparam name="T"> The type of the value returned on operation success. </typeparam>
    /// <typeparam name="E"> The type of the value returned on operation failure. </typeparam>
    public sealed class Failure<T, E> : Result<T, E> {
        /// <summary> Gets the error that caused the unsuccessful completion of the operation. </summary>
        /// <remarks>
        ///     Typically, this will be an exception indicating the error that occurred, but could also be an error message
        ///     string, an integer or enum error code, or a more complex type that represents the error details.
        /// </remarks>
        /// <value> The error value returned by the unsuccessful operation. </value>
        public E Error { get; }

        internal Failure(E error) : base(false) {
            Error = error;
        }
    }
}
