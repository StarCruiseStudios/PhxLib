// -----------------------------------------------------------------------------
//  <copyright file="Optional.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Text;

namespace Phx.Lang
{
    /// <summary>
    ///     An object which can be used to indicate a value that may or may not be present.
    /// </summary>
    /// <typeparam name="T"> The type of value contained by the <see cref="Optional{T}"/> when non-empty. </typeparam>
    public struct Optional<T> where T : notnull
    {
        private readonly T value;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="Optional{T}"/> contains a value.
        /// </summary>
        /// <value> <c>true</c> when a value is present, otherwise <c>false</c>. </value>
        public bool IsPresent { get; }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="Optional{T}"/> does not contain a value.
        /// </summary>
        /// <value> <c>true</c> when no value is present, otherwise <c>false</c>. </value>
        public bool IsEmpty { get => !IsPresent; }

        /// <summary>
        ///     Gets the value contained by the <see cref="Optional{T}"/> if non-empty.
        /// </summary>
        /// <value> The value. </value>
        /// <exception cref="InvalidOperationException"> Thrown if this method is invoked when this
        ///                                              <see cref="Optional{T}"/> is empty. </exception>
        public T Value
        {
            get
            {
                return IsPresent
                    ? value
                    : throw new InvalidOperationException("Cannot get value of empty optional.");
            }
        }

        private Optional(T value, bool isPresent)
        {
            this.value = value;
            IsPresent = isPresent;
        }

        /// <summary>
        ///     InvokesIf a value is present in the <see cref="Optional{T}"/>, the specified action is invoked with the
        ///     contained value passed as an argument.
        /// </summary>
        /// <param name="action"> The action to invoke. </param>
        public void IfPresent(Action<T> action)
        {
            if (IsPresent)
            {
                action(value);
            }
        }

        /// <summary>
        ///     Invokes the specified action if no value is contained in the <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="action"> The action to invoke. </param>
        public void IfEmpty(Action action)
        {
            if (IsEmpty)
            {
                action();
            }
        }

        /// <summary>
        ///     Applies the given mapping function to the value contained in the <see cref="Optional{T}"/> if one is
        ///     present. Otherwise, an empty optional is returned.
        /// </summary>
        /// <typeparam name="U"> The type of value contained in the output <see cref="Optional{U}"/>. </typeparam>
        /// <param name="mapper"> The mapping function applied to the value if the <see cref="Optional{T}"/> is present.
        ///                       </param>
        public Optional<U> Map<U>(Func<T, Optional<U>> mapper) where U : notnull
        {
            if (IsPresent)
            {
                return mapper(value);
            }

            return Optional<U>.EMPTY;
        }

        /// <summary>
        ///     Returns the value contained inside of the <see cref="Optional{T}"/> if it is present. Otherwise, the
        ///     value computed by the given function is returned.
        /// </summary>
        /// <param name="other"> The function that computes the output value if the <see cref="Optional{T}"/> is empty.
        ///                      </param>
        public T OrElse(Func<T> other)
        {
            return IsPresent ? value : other();
        }

        /// <summary>
        ///     Returns the value contained inside of the <see cref="Optional{T}"/> if it is present. Otherwise, the
        ///     value computed by the given function is returned.
        /// </summary>
        /// <param name="other"> The function that computes the output value if the <see cref="Optional{T}"/> is empty.
        ///                      </param>
        public Optional<T> OrTry(Func<Optional<T>> other)
        {
            return IsPresent ? this : other();
        }

        /// <summary>
        ///     Returns the value contained inside of the <see cref="Optional{T}"/> if it is present. Otherwise, the
        ///     exception returned by the given function is returned.
        /// </summary>
        /// <param name="exception"> The function that provides the exception to throw if the <see cref="Optional{T}"/>
        ///                          is empty. </param>
        /// <exception cref="Exception"> thrown if this method is invoked and the <see cref="Optional{T}"/> is empty.
        ///                              </exception>
        public T OrElseThrow(Func<Exception> exception)
        {
            return IsPresent ? value : throw exception();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return new StringBuilder(base.ToString())
                .Append(" { ")
                .Append(IsPresent ? value.ToStringSafe() : "[Empty]")
                .Append(" }")
                .ToString();
        }

        /// <summary>
        ///     Returns an <see cref="Optional{T}"/> with the provided value.
        /// </summary>
        /// <param name="value"> The value to contain in the <see cref="Optional{T}"/>. </param>
        public static Optional<T> Of(T value)
        {
            return new Optional<T>(value, true);
        }

        /// <summary>
        ///     Returns an <see cref="Optional{T}"/> with the provided non-null value, or
        ///     <see cref="Optional{T}.EMPTY"/> if the provided value is null.
        /// </summary>
        /// <param name="value"> The value to contain in the <see cref="Optional{T}"/>. </param>
        public static Optional<T> OfNullable(T? value)
        {
            return (value == null)
                ? EMPTY
                : new Optional<T>(value, true);
        }

        /// <summary>
        ///     An empty <see cref="Optional{T}"/> of type <typeparamref name="T"/>.
        /// </summary>
        public static readonly Optional<T> EMPTY = new Optional<T>(default!, false);
    }
}