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

    public class StringCasingConverter {
        public enum StringCasing {
            Camel,
            Caps,
            Kebab,
            Pascal,
            Snake
        }
        public bool IsValid { get; }
        
        private string originalString;
        private StringCasing casing;
        private List<string> words;

        private StringCasingConverter(string originalString, StringCasing casing) {
            this.originalString = originalString;
            this.casing = casing;
            words = new();
            IsValid = false;
        }
        private StringCasingConverter(string originalString, StringCasing casing, List<string> words) {
            this.originalString = originalString;
            this.casing = casing;
            this.words = words;
            IsValid = true;
        }

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

        public Result<string, InvalidOperationException> ToCamelCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }
            
            var capitalizedWords = words.Select(word => word.StartUppercase());
            return Result.Success<string, InvalidOperationException>(
                    string.Join("", capitalizedWords) .StartLowercase());
        }

        public Result<string, InvalidOperationException> ToCapsCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            var capsWords = words.Select(word => word.ToUpperInvariant());
            return Result.Success<string, InvalidOperationException>(string.Join("_", capsWords));
        }

        public Result<string, InvalidOperationException> ToKebabCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            return Result.Success<string, InvalidOperationException>(string.Join("-", words));
        }

        public Result<string, InvalidOperationException> ToPascalCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            var capitalizedWords = words.Select(word => word.StartUppercase());
            return Result.Success<string, InvalidOperationException>(string.Join("", capitalizedWords));
        }

        public Result<string, InvalidOperationException> ToSnakeCase() {
            if (!IsValid) {
                return Result.Failure<string, InvalidOperationException>(
                        new InvalidOperationException(
                                $"Input string {originalString} was not a valid string in the input format {casing}."));
            }

            return Result.Success<string, InvalidOperationException>(string.Join("_", words));
        }

        public static StringCasingConverter FromCase(StringCasing casing, string originalString) {
            return casing switch {
                StringCasing.Camel => FromCamelCase(originalString),
                StringCasing.Caps => FromCapsCase(originalString),
                StringCasing.Kebab => FromKebabCase(originalString),
                StringCasing.Pascal => FromPascalCase(originalString),
                StringCasing.Snake => FromSnakeCase(originalString),
                _ => ToDo.NotSupportedYet<StringCasingConverter>($"Casing {casing} was added without implementing a conversion.")
            };
        }

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
                } else if (char.IsLetter(c) || char.IsDigit(c)) {
                    sb.Append(c);
                } else {
                    return Invalid(originalString, StringCasing.Camel);
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            return new StringCasingConverter(originalString, StringCasing.Camel, words);
        }
        
        public static StringCasingConverter FromCapsCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (c == '_') {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                    } else if (words.Count > 0) { // Ignore prefix underscores
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

            return new StringCasingConverter(originalString, StringCasing.Caps, words);
        }

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

            return new StringCasingConverter(originalString, StringCasing.Kebab, words);
        }

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
                } else if (char.IsLetter(c) || char.IsDigit(c)) {
                    sb.Append(c);
                } else {
                    return Invalid(originalString, StringCasing.Pascal);
                }
            }

            if (sb.Length > 0) {
                words.Add(sb.ToString());
            }

            return new StringCasingConverter(originalString, StringCasing.Pascal, words);
        }

        public static StringCasingConverter FromSnakeCase(string originalString) {
            var words = new List<string>();
            var sb = new StringBuilder();

            foreach (char c in originalString) {
                if (c == '_') {
                    if (sb.Length > 0) {
                        words.Add(sb.ToString());
                        sb.Clear();
                    } else if (words.Count > 0) { // Ignore prefix underscores
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

            return new StringCasingConverter(originalString, StringCasing.Snake, words);
        }

        private static StringCasingConverter Invalid(string originalString, StringCasing casing) {
            return new StringCasingConverter(originalString, casing);
        }
    }
}
