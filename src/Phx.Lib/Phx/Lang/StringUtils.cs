// -----------------------------------------------------------------------------
//  <copyright file="StringUtils.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using static Phx.Lang.Unit;

namespace Phx.Lang {
    using System.Linq;
    using System.Text;

    /// <summary> Provides utilities for working with the <see cref="string" /> class. </summary>
    public static class StringUtils {
        /// <summary> A string representation of the <c> null </c> value. </summary>
        public const string NullString = "[null]";

        /// <summary> A constant value empty string. </summary>
        /// <remarks>
        ///     <see cref="string.Empty" /> is a static readonly value, not a constant, so it cannot be used in certain
        ///     contexts such as annotation arguments or switch statements.
        /// </remarks>
        public const string EmptyString = "";

        /// <summary> Converts a string from caps case to pascal case. </summary>
        /// <remarks>
        ///     A caps case string, consists of only capital letters, numbers and underscore. A pascal case string, consists
        ///     of only letters and numbers and the first letter of each word is capitalized.
        /// </remarks>
        /// <param name="capsString"> The caps string. </param>
        /// <returns>
        ///     A <see cref="Success{string, Unit}" /> containing the pascal case representation of the given string, or a
        ///     <see cref="Failure{string, Unit}" /> if the input string is not caps case.
        /// </returns>
        public static Result<string, Unit> CapsToPascalCase(string capsString) {
            if (!IsCapsCase(capsString)) {
                return Result.Failure<string, Unit>(UNIT);
            }

            var sb = new StringBuilder();

            bool nextIsCaps = true;
            foreach (char c in capsString) {
                if (c == '_') {
                    nextIsCaps = true;
                } else if (char.IsDigit(c)) {
                    _ = sb.Append(c);
                    nextIsCaps = true;
                } else if (nextIsCaps) {
                    _ = sb.Append(c);
                    nextIsCaps = false;
                } else {
                    _ = sb.Append(char.ToLowerInvariant(c));
                }
            }

            return Result.Success<string, Unit>(sb.ToString());
        }

        /// <summary> Determines whether the specified string is caps case. </summary>
        /// <remarks> A caps case string, consists of only capital letters, numbers and underscore. </remarks>
        /// <param name="capsString"> The caps string. </param>
        /// <returns> <c> true </c> if the specified string is caps case; otherwise, <c> false </c>. </returns>
        public static bool IsCapsCase(string capsString) {
            return capsString.All(
                    c => c == '_'
                            || char.IsDigit(c)
                            || (char.IsLetter(c) && char.IsUpper(c)));
        }

        /// <summary> Escapes a string for use as verbatim string, replacing all " characters with "". </summary>
        /// <param name="verbatimString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeVerbatimString(string verbatimString) {
            return verbatimString.Replace("\"", "\"\"");
        }

        /// <summary> Escapes the double quotes within a string, replacing all characters with \". </summary>
        /// <param name="quoteString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeStringQuotes(string quoteString) {
            return quoteString.Replace("\"", "\\\"");
        }
    }
}
