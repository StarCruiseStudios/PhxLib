// -----------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using Phx.Debug;

    /// <summary> Provides extension methods for the <see cref="string" /> class. </summary>
    public static class StringExtensions {
        /// <summary> Returns a string that represents the current object and is safe to call on a null instance. </summary>
        /// <param name="obj"> The object to convert to a string. </param>
        /// <returns>
        ///     A string that represents the current object or the <see cref="StringUtils.NullString" /> if the instance is
        ///     null.
        /// </returns>
        public static string ToStringSafe(this object? obj) {
            return obj?.ToString() ?? StringUtils.NullString;
        }

        /// <summary> Returns a programmer facing debug display string representation of an instance. </summary>
        /// <remarks>
        ///     If the instance implements the <see cref="IDebugDisplay" /> interface, this will use the
        ///     <see cref="IDebugDisplay.ToDebugDisplay()" /> method. Otherwise, it will use the instance's
        ///     <see cref="object.ToString()" /> method.
        /// </remarks>
        /// <param name="obj"> The instance to get the debug display string from. </param>
        /// <returns> The debug display string representation of the instance. </returns>
        public static string ToDebugDisplayString(this object? obj) {
            if (obj is IDebugDisplay debug) {
                return debug.ToDebugDisplay();
            } else {
                return obj.ToStringSafe();
            }
        }

        public static string StartUppercase(this string input)=> StringUtils.StartUppercase(input);

        public static string StartLowercase(this string input)=> StringUtils.StartLowercase(input);

        public static string RemoveLeadingI(this string input) => StringUtils.RemoveLeadingI(input);

        public static StringCasingConverter FromCase(this string input, StringCasingConverter.StringCasing casing) =>
                StringCasingConverter.FromCase(casing, input);
        public static StringCasingConverter FromCamelCase(this string input) =>
                StringCasingConverter.FromCamelCase(input);
        public static StringCasingConverter FromCapsCase(this string input) =>
                StringCasingConverter.FromCapsCase(input);
        public static StringCasingConverter FromKebabCase(this string input) =>
                StringCasingConverter.FromKebabCase(input);
        public static StringCasingConverter FromPascalCase(this string input) =>
                StringCasingConverter.FromPascalCase(input);
        public static StringCasingConverter FromSnakeCase(this string input) =>
                StringCasingConverter.FromSnakeCase(input);

        /// <summary> Escapes a string for use as verbatim string, replacing all " characters with "". </summary>
        /// <param name="verbatimString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeVerbatimString(this string verbatimString) {
            return StringUtils.EscapeVerbatimString(verbatimString);
        }

        /// <summary> Unescapes a string used as verbatim string, replacing all "" characters with ". </summary>
        /// <param name="verbatimString"> The string to unescape. </param>
        /// <returns> The unescaped string. </returns>
        public static string UnescapeVerbatimString(this string verbatimString) {
            return StringUtils.UnescapeVerbatimString(verbatimString);
        }

        /// <summary> Escapes the double quotes within a string, replacing all " characters with \". </summary>
        /// <param name="quoteString"> The string to escape. </param>
        /// <returns> The escaped string. </returns>
        public static string EscapeStringQuotes(this string quoteString) {
            return StringUtils.EscapeStringQuotes(quoteString);
        }
        
        /// <summary> Unescapes the double quotes within a string, replacing all \" characters with ". </summary>
        /// <param name="quoteString"> The string to unescape. </param>
        /// <returns> The unescaped string. </returns>
        public static string UnescapeStringQuotes(this string quoteString) {
            return StringUtils.UnescapeStringQuotes(quoteString);
        }
    }
}
