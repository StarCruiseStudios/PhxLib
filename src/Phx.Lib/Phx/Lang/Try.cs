// -----------------------------------------------------------------------------
//  <copyright file="Try.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Phx.Lang
{
    public static class Try
    {
        private const string DEFAULT_MESSAGE = "One or more unexpected exceptions occurred.";

        /// <summary>
        ///     Executes the each of the given <paramref name="actions"/>, and throws all resulting exceptions as an
        ///     <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <param name="actions"> The collection of <see cref="Action">s to execute. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All(params Action[] actions)
        {
            All(DEFAULT_MESSAGE, actions.AsEnumerable());
        }

        /// <summary>
        ///     Executes the each of the given <paramref name="actions"/>, and throws all resulting exceptions as an
        ///     <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <param name="message"> The message to use if any of the actions throw an exception. </param>
        /// <param name="actions"> The collection of <see cref="Action">s to execute. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All(string message, params Action[] actions)
        {
            All(message, actions.AsEnumerable());
        }

        /// <summary>
        ///     Executes the each of the given <paramref name="actions"/>, and throws all resulting exceptions as an
        ///     <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <param name="actions"> The collection of <see cref="Action">s to execute. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All(IEnumerable<Action> actions)
        {
            All(DEFAULT_MESSAGE, actions);
        }

        /// <summary>
        ///     Executes the each of the given <paramref name="actions"/>, and throws all resulting exceptions as an
        ///     <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <param name="message"> The message to use if any of the actions throw an exception. </param>
        /// <param name="actions"> The collection of <see cref="Action"/>s to execute. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All(string message, IEnumerable<Action> actions)
        {
            var exceptions = new List<Exception>();
            foreach (var action in actions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(message, exceptions);
            }
        }

        /// <summary>
        ///     Executes the given <paramref name="action"/> on each of the given <paramref name="items", and throws all
        ///     resulting exceptions as an <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <typeparam name="T"> The type of the items the <paramref name="action"/> will be executed on. </typeparam>
        /// <param name="action"> The <see cref="Action"/> to execute. </param>
        /// <param name="items"> The collection of items the <paramref name="action"/> will be executed on. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All<T>(Action<T> action, params T[] items)
        {
            All(DEFAULT_MESSAGE, action, items.AsEnumerable());
        }

        /// <summary>
        ///     Executes the given <paramref name="action"/> on each of the given <paramref name="items", and throws all
        ///     resulting exceptions as an <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <typeparam name="T"> The type of the items the <paramref name="action"/> will be executed on. </typeparam>
        /// <param name="message"> The lazily computed message to use if any of the actions throw an exception. </param>
        /// <param name="action"> The <see cref="Action"/> to execute. </param>
        /// <param name="items"> The collection of items the <paramref name="action"/> will be executed on. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All<T>(string message, Action<T> action, params T[] items)
        {
            All(message, action, items.AsEnumerable());
        }

        /// <summary>
        ///     Executes the given <paramref name="action"/> on each of the given <paramref name="items", and throws all
        ///     resulting exceptions as an <see cref="AggregateException"/>.
        /// </summary>
        /// <remarks>
        ///     All actions are guaranteed to be run.
        /// </remarks>
        /// <typeparam name="T"> The type of the items the <paramref name="action"/> will be executed on. </typeparam>
        /// <param name="action"> The <see cref="Action"/> to execute. </param>
        /// <param name="items"> The collection of items the <paramref name="action"/> will be executed on. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All<T>(Action<T> action, IEnumerable<T> items)
        {
            All(DEFAULT_MESSAGE, action, items);
        }

        /// <summary>
        ///     Executes the given <paramref name="action"/> on each of the given <paramref name="items", and throws all
        ///     resulting exceptions as an <see cref="AggregateException"/>.
        /// </summary>
        /// <typeparam name="T"> The type of the items the <paramref name="action"/> will be executed on. </typeparam>
        /// <param name="message"> The lazily computed message to use if any of the actions throw an exception. </param>
        /// <param name="action"> The <see cref="Action"/> to execute. </param>
        /// <param name="items"> The collection of items the <paramref name="action"/> will be executed on. </param>
        /// <exception cref="AggregateException"> Thrown if any of the actions throw an exception. </exception>
        public static void All<T>(string message, Action<T> action, IEnumerable<T> items)
        {
            var exceptions = new List<Exception>();
            foreach (var item in items)
            {
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(message, exceptions);
            }
        }
    }
}