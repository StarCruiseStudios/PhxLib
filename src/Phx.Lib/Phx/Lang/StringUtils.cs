// -----------------------------------------------------------------------------
//  <copyright file="StringUtils.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using static Phx.Lang.Unit;

namespace Phx.Lang {
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
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

        public static string StartUppercase(string input) {
            return char.ToUpper(input[0]) + input[1..];
        }

        public static string StartLowercase(string input) {
            return char.ToLower(input[0]) + input[1..];
        }

        public static string RemoveLeadingI(string input) {
            // If only one "I" is found, check if the second character is
            // capital to avoid false positives on words that start with I. We
            // only want to remove the "I" interface prefix.
            // e.g. ICar => Car, Car => Car, IInput => Input, Input => Input
            bool hasLeadingI = input.StartsWith("II") 
                    || (input.StartsWith("I") && input.Length > 2 && char.IsUpper(input[1]));
            
            return hasLeadingI
                    ? input[1..]
                    : input;
        }
        
        /// <summary> Escapes a string for use as verbatim string, replacing all " characters with "". </summary>
        /// <param name="verbatimString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeVerbatimString(string verbatimString) {
            return verbatimString.Replace("\"", "\"\"");
        }

        /// <summary> Unescapes a string used as verbatim string, replacing all "" characters with ". </summary>
        /// <param name="verbatimString"> The string to unescape. </param>
        /// <returns> The unescaped string. </returns>
        public static string UnescapeVerbatimString(string verbatimString) {
            return verbatimString.Replace("\"\"", "\"");
        }

        /// <summary> Escapes the double quotes within a string, replacing all " characters with \". </summary>
        /// <param name="quoteString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeStringQuotes(string quoteString) {
            return quoteString.Replace("\"", "\\\"");
        }

        /// <summary> Unescapes the double quotes within a string, replacing all \" characters with ". </summary>
        /// <param name="quoteString"> The string to unescape. </param>
        /// <returns> The unescaped string. </returns>
        public static string UnescapeStringQuotes(string quoteString) {
            return quoteString.Replace("\\\"", "\"");
        }

        /// <summary> Builds a new string using the given action. </summary>
        /// <param name="buildString"> The action containing the steps to build the string. </param>
        /// <returns> The newly constructed string. </returns>
        public static string BuildString(Action<StringBuilder> buildString) {
            var stringBuilder = new StringBuilder();
            buildString(stringBuilder);
            return stringBuilder.ToString();
        }
    }
}
