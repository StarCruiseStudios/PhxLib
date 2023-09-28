// -----------------------------------------------------------------------------
//  <copyright file="StringCasingConverter.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Phx.Dev;

    /// <summary> An instance used to convert strings from one casing to another. </summary>
    public sealed class StringCasingConverter {
        /// <summary> Gets whether the input string is a valid representation of the input string casing. </summary>
        public bool IsValid { get; }

        private string originalString;
        private StringCasing casing;
        private List<string> words;

        private StringCasingConverter(string originalString, StringCasing casing) {
            this.originalString = originalString;
            this.casing = casing;
            words = new List<string>();
            IsValid = false;
        }

        private StringCasingConverter(string originalString, StringCasing casing, List<string> words) {
            this.originalString = originalString;
            this.casing = casing;
            this.words = words;
            IsValid = true;
        }

        /// <summary> Converts the input string to the given string casing. </summary>
        /// <param name="casing"> The string casing to convert to. </param>
        /// <returns>
        ///     A result containing the converted string, or an InvalidOperationException if the
        ///     conversion could not be performed.
        /// </returns>
        public Result<string, InvalidOperationException> ToCase(StringCasing casing) {
            return casing switch {
                StringCasing.Camel => ToCamelCase(),
                StringCasing.Caps => ToCapsCase(),
                StringCasing.Kebab => ToKebabCase(),
                StringCasing.Pascal => ToPascalCase(),
                StringCasing.Snake => ToSnakeCase(),
                _ => Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException($"Casing {casing} was added without implementing a conversion."))
            };
        }

        /// <summary> Converts the input string to camel casing. </summary>
        /// <returns>
        ///     A result containing the converted string, or an InvalidOperationException if the
        ///     conversion could not be performed.
        /// </returns>
        public Result<string, InvalidOperationException> ToCamelCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            var capitalizedWords = words.Select(word => word.StartUppercase());
            return Result.Success<string, InvalidOperationException>(
                    string.Join("", capitalizedWords).StartLowercase());
        }

        /// <summary> Converts the input string to caps casing. </summary>
        /// <returns>
        ///     A result containing the converted string, or an InvalidOperationException if the
        ///     conversion could not be performed.
        /// </returns>
        public Result<string, InvalidOperationException> ToCapsCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            var capsWords = words.Select(word => word.ToUpperInvariant());
            return Result.Success<string, InvalidOperationException>(string.Join("_", capsWords));
        }

        /// <summary> Converts the input string to kebab casing. </summary>
        /// <returns>
        ///     A result containing the converted string, or an InvalidOperationException if the
        ///     conversion could not be performed.
        /// </returns>
        public Result<string, InvalidOperationException> ToKebabCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            return Result.Success<string, InvalidOperationException>(string.Join("-", words));
        }

        /// <summary> Converts the input string to pascal casing. </summary>
        /// <returns>
        ///     A result containing the converted string, or an InvalidOperationException if the
        ///     conversion could not be performed.
        /// </returns>
        public Result<string, InvalidOperationException> ToPascalCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            var capitalizedWords = words.Select(word => word.StartUppercase());
            return Result.Success<string, InvalidOperationException>(string.Join("", capitalizedWords));
        }

        /// <summary> Converts the input string to snake casing. </summary>
        /// <returns>
        ///     A result containing the converted string, or an InvalidOperationException if the
        ///     conversion could not be performed.
        /// </returns>
        public Result<string, InvalidOperationException> ToSnakeCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            return Result.Success<string, InvalidOperationException>(string.Join("_", words));
        }

        /// <summary> Parses a string as the given casing. </summary>
        /// <param name="casing"> The string casing to parse from. </param>
        /// <param name="originalString"> The original string to parse. </param>
        /// <returns>
        ///     A StringCasingConverter that can be used to convert to another casing or to check if the
        ///     input string is a valid representation of the input casing.
        /// </returns>
        public static StringCasingConverter FromCase(StringCasing casing, string originalString) {
            return casing switch {
                StringCasing.Camel => FromCamelCase(originalString),
                StringCasing.Caps => FromCapsCase(originalString),
                StringCasing.Kebab => FromKebabCase(originalString),
                StringCasing.Pascal => FromPascalCase(originalString),
                StringCasing.Snake => FromSnakeCase(originalString),
                _ => ToDo.NotSupportedYet<StringCasingConverter>(
                        $"Casing {casing} was added without implementing a conversion.")
            };
        }

        /// <summary> Parses a string as a camel case string. </summary>
        /// <param name="originalString"> The original string to parse. </param>
        /// <returns>
        ///     A StringCasingConverter that can be used to convert to another casing or to check if the
        ///     input string is a valid representation of the input casing.
        /// </returns>
        public static StringCasingConverter FromCamelCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (char.IsUpper(c)) {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                        sb.Append(char.ToLowerInvariant(c));
                    } else {
                        return Invalid(originalString, StringCasing.Camel);
                    }
                } else if (char.IsLetterOrDigit(c)) {
                    sb.Append(c);
                } else {
                    return Invalid(originalString, StringCasing.Camel);
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            if (words.Count > 0 && !char.IsLetter(words[0][0])) {
                return Invalid(originalString, StringCasing.Camel);
            }

            return new StringCasingConverter(originalString, StringCasing.Camel, words);
        }

        /// <summary> Parses a string as a caps case string. </summary>
        /// <param name="originalString"> The original string to parse. </param>
        /// <returns>
        ///     A StringCasingConverter that can be used to convert to another casing or to check if the
        ///     input string is a valid representation of the input casing.
        /// </returns>
        public static StringCasingConverter FromCapsCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (c == '_') {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                    } else if (words.Count > 0) {
                        // Ignore prefix underscores
                        return Invalid(originalString, StringCasing.Caps);
                    }
                } else {
                    if (char.IsDigit(c) || (char.IsLetter(c) && char.IsUpper(c))) {
                        sb.Append(char.ToLowerInvariant(c));
                    } else {
                        return Invalid(originalString, StringCasing.Caps);
                    }
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            if (words.Count > 0 && !char.IsLetter(words[0][0])) {
                return Invalid(originalString, StringCasing.Caps);
            }

            return new StringCasingConverter(originalString, StringCasing.Caps, words);
        }

        /// <summary> Parses a string as a kebab case string. </summary>
        /// <param name="originalString"> The original string to parse. </param>
        /// <returns>
        ///     A StringCasingConverter that can be used to convert to another casing or to check if the
        ///     input string is a valid representation of the input casing.
        /// </returns>
        public static StringCasingConverter FromKebabCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (c == '-') {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                    } else {
                        return Invalid(originalString, StringCasing.Kebab);
                    }
                } else {
                    if (char.IsDigit(c) || (char.IsLetter(c) && char.IsLower(c))) {
                        sb.Append(c);
                    } else {
                        return Invalid(originalString, StringCasing.Kebab);
                    }
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            if (words.Count > 0 && !char.IsLetter(words[0][0])) {
                return Invalid(originalString, StringCasing.Kebab);
            }

            return new StringCasingConverter(originalString, StringCasing.Kebab, words);
        }

        /// <summary> Parses a string as a pascal case string. </summary>
        /// <param name="originalString"> The original string to parse. </param>
        /// <returns>
        ///     A StringCasingConverter that can be used to convert to another casing or to check if the
        ///     input string is a valid representation of the input casing.
        /// </returns>
        public static StringCasingConverter FromPascalCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (char.IsUpper(c)) {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                        sb.Append(char.ToLowerInvariant(c));
                    } else if (words.Count == 0) {
                        sb.Append(char.ToLowerInvariant(c));
                    } else {
                        return Invalid(originalString, StringCasing.Pascal);
                    }
                } else if (char.IsLetterOrDigit(c)) {
                    if (words.Count == 0 && sb.Length == 0) {
                        return Invalid(originalString, StringCasing.Pascal);
                    } else {
                        sb.Append(c);
                    }
                } else {
                    return Invalid(originalString, StringCasing.Pascal);
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            if (words.Count > 0 && !char.IsLetter(words[0][0])) {
                return Invalid(originalString, StringCasing.Pascal);
            }

            return new StringCasingConverter(originalString, StringCasing.Pascal, words);
        }

        /// <summary> Parses a string as a snake case string. </summary>
        /// <param name="originalString"> The original string to parse. </param>
        /// <returns>
        ///     A StringCasingConverter that can be used to convert to another casing or to check if the
        ///     input string is a valid representation of the input casing.
        /// </returns>
        public static StringCasingConverter FromSnakeCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (c == '_') {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                    } else if (words.Count > 0) {
                        // Ignore prefix underscores
                        return Invalid(originalString, StringCasing.Snake);
                    }
                } else {
                    if (char.IsDigit(c) || (char.IsLetter(c) && char.IsLower(c))) {
                        sb.Append(c);
                    } else {
                        return Invalid(originalString, StringCasing.Snake);
                    }
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            if (words.Count > 0 && !char.IsLetter(words[0][0])) {
                return Invalid(originalString, StringCasing.Snake);
            }

            return new StringCasingConverter(originalString, StringCasing.Snake, words);
        }

        private static StringCasingConverter Invalid(string originalString, StringCasing casing) {
            return new StringCasingConverter(originalString, casing);
        }
    }
}
