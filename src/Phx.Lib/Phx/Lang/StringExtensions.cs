// -----------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang
{
    /// <summary>
    ///     Provides extension methods for the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Returns a string that represents the current object and is safe to call on a null instance.
        /// </summary>
        /// <param name="obj"> The object to convert to a string. </param>
        /// <returns> A string that represents the current object or the <see cref="StringUtils.NullString"/> if the
        ///           instance is null. </returns>
        public static string ToStringSafe(this object? obj)
        {
            return obj?.ToString() ?? StringUtils.NullString;
        }

        /// <summary>
        ///     Returns a user facing display string representation of an instance.
        /// </summary>
        /// <remarks>
        ///     If the instance implements the <see cref="IDisplay"/> interface, this will use the
        ///     <see cref="IDisplay.ToDisplay()"/> method. Otherwise, it will use the instance's
        ///     <see cref="object.ToString()"/> method.
        /// </remarks>
        /// <param name="obj"> The instance to get the display string from. </param>
        /// <returns> The display string representation of the instance. </returns>
        public static string ToDisplayString(this object? obj)
        {
            return (obj is IDisplay d)
                ? d.ToDisplay()
                : obj.ToStringSafe();
        }

        /// <summary>
        ///     Returns a programmer facing debug display string representation of an instance.
        /// </summary>
        /// <remarks>
        ///     If the instance implements the <see cref="IDebugDisplay"/> interface, this will use the
        ///     <see cref="IDebugDisplay.ToDebugDisplay()"/> method. Otherwise, If the instance implements the
        ///     <see cref="IDisplay"/> interface, this will use the <see cref="IDisplay.ToDisplay()"/> method.
        ///     Otherwise, it will use the instance's <see cref="object.ToString()"/> method.
        /// </remarks>
        /// <param name="obj"> The instance to get the debug display string from. </param>
        /// <returns> The debug display string representation of the instance. </returns>
        public static string ToDebugDisplayString(this object? obj)
        {
            if (obj is IDebugDisplay debug)
            {
                return debug.ToDebugDisplay();
            }
            else if (obj is IDisplay d)
            {
                return d.ToDisplay();
            }
            else
            {
                return obj.ToStringSafe();
            }
        }

        /// <summary>
        ///     Converts a string from a caps case string to Pascal case.
        /// </summary>
        /// <remarks>
        ///     A caps case string, consists of only capital letters, numbers and underscore. A pascal case string,
        ///     consists of only letters and numbers and the first letter of each word is capitalized.
        /// </remarks>
        /// <param name="capsString"> The caps string. </param>
        /// <returns> A <see cref="Success{string, Unit}"/> containing the pascal case representation of the given
        ///           string, or a <see cref="Failure{string, Unit}"/> if the input string is not caps case. </returns>
        public static Result<string, Unit> CapsToPascalCase(this string capsString)
        {
            return StringUtils.CapsToPascalCase(capsString);
        }

        /// <summary>
        ///     Determines whether the specified string is caps case.
        /// </summary>
        /// <remarks>
        ///     A caps case string, consists of only capital letters, numbers and underscore.
        /// </remarks>
        /// <param name="capsString"> The caps string. </param>
        /// <returns> <c> true </c> if the specified string is caps case; otherwise, <c> false </c>. </returns>
        public static bool IsCapsCase(this string capsString)
        {
            return StringUtils.IsCapsCase(capsString);
        }

        /// <summary>
        ///     Escapes a string for use as verbatim string, replacing all " characters with "".
        /// </summary>
        /// <param name="verbatimString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeVerbatimString(this string verbatimString)
        {
            return StringUtils.EscapeVerbatimString(verbatimString);
        }

        /// <summary>
        ///     Escapes the double quotes within a string, replacing all " characters with \".
        /// </summary>
        /// <param name="quoteString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeStringQuotes(this string quoteString)
        {
            return StringUtils.EscapeStringQuotes(quoteString);
        }
    }
}