// -----------------------------------------------------------------------------
//  <copyright file="StringUtils.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang
{
    using System;
    using System.Text;

    /// <summary> Provides utilities for working with the <see cref="string" /> class. </summary>
    public static class StringUtils
    {
        /// <summary> A string representation of the <c> null </c> value. </summary>
        public const string NullString = "[null]";

        /// <summary> A constant value empty string. </summary>
        /// <remarks>
        ///     <see cref="string.Empty" /> is a static readonly value, not a constant, so it cannot be used in certain
        ///     contexts such as annotation arguments or switch statements.
        /// </remarks>
        public const string EmptyString = "";

        /// <summary> Returns the input string with the first character uppercase. </summary>
        /// <remarks>
        ///     This only affects strings that begin with a letter. Strings that begin with non-letter characters will not be
        ///     affected.
        /// </remarks>
        /// <param name="input"> The input string. </param>
        /// <returns> The input string with the first character uppercase. </returns>
        public static string StartUppercase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary> Returns the input string with the first character lowercase. </summary>
        /// <remarks>
        ///     This only affects strings that begin with a letter. Strings that begin with non-letter characters will not be
        ///     affected.
        /// </remarks>
        /// <param name="input"> The input string. </param>
        /// <returns> The input string with the first character lowercase. </returns>
        public static string StartLowercase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return char.ToLower(input[0]) + input.Substring(1);
        }

        /// <summary> Removes the leading I typically used to prefix an interface typename. </summary>
        /// <remarks>
        ///     This will only remove the "I" as a prefix and will not affect words that simply begin with "I". e.g. ICar =>
        ///     Car, Car => Car, IInput => Input, Input => Input
        /// </remarks>
        /// <param name="input"> The input string. </param>
        /// <returns> The output string with a leading I removed. </returns>
        public static string RemoveLeadingI(string input)
        {
            // If only one "I" is found, check if the second character is
            // capital to avoid false positives on words that start with I.
            bool hasLeadingI = input.StartsWith("II")
                    || (input.StartsWith("I") && input.Length > 2 && char.IsUpper(input[1]));

            return hasLeadingI
                    ? input.Substring(1)
                    : input;
        }

        /// <summary> Escapes a string for use as verbatim string, replacing all " characters with "". </summary>
        /// <param name="verbatimString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeVerbatimString(string verbatimString)
        {
            return verbatimString.Replace("\"", "\"\"");
        }

        /// <summary> Unescapes a string used as verbatim string, replacing all "" characters with ". </summary>
        /// <param name="verbatimString"> The string to unescape. </param>
        /// <returns> The unescaped string. </returns>
        public static string UnescapeVerbatimString(string verbatimString)
        {
            return verbatimString.Replace("\"\"", "\"");
        }

        /// <summary> Escapes the double quotes within a string, replacing all " characters with \". </summary>
        /// <param name="quoteString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeStringQuotes(string quoteString)
        {
            return quoteString.Replace("\"", "\\\"");
        }

        /// <summary> Unescapes the double quotes within a string, replacing all \" characters with ". </summary>
        /// <param name="quoteString"> The string to unescape. </param>
        /// <returns> The unescaped string. </returns>
        public static string UnescapeStringQuotes(string quoteString)
        {
            return quoteString.Replace("\\\"", "\"");
        }

        /// <summary> Builds a new string using the given action. </summary>
        /// <param name="buildString"> The action containing the steps to build the string. </param>
        /// <returns> The newly constructed string. </returns>
        public static string BuildString(Action<StringBuilder> buildString)
        {
            var stringBuilder = new StringBuilder();
            buildString(stringBuilder);
            return stringBuilder.ToString();
        }
    }
}
