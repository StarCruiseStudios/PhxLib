// -----------------------------------------------------------------------------
//  <copyright file="StringExtensionsTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;
    using static StringUtils;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class StringExtensionsTests : LoggingTestClass {
        [TestCase("some string", "some string")]
        [TestCase(EmptyString, EmptyString)]
        [TestCase(null, NullString)]
        public void ToStringSafe(string inputString, string expectedOutput) {
            var input = Given("An input string", () => inputString);

            var result = When("Converted using ToStringSafe", () => inputString.ToStringSafe());

            Then("The expected string is returned.",
                    expectedOutput,
                    (expected) => Verify.That(result.IsEqualTo(expected)));
        }

        [TestCase("startLowercase", "StartLowercase")]
        [TestCase("StartUppercase", "StartUppercase")]
        [TestCase("_StartNonAlpha", "_StartNonAlpha")]
        [TestCase(EmptyString, EmptyString)]
        public void StartUpperCase(string inputString, string expectedOutput) {
            var input = Given("An input string", () => inputString);

            var result = When("Getting the string with uppercase first letter", () => inputString.StartUppercase());

            Then("The expected string is returned.",
                    expectedOutput,
                    (expected) => Verify.That(result.IsEqualTo(expected)));
        }

        [TestCase("startLowercase", "startLowercase")]
        [TestCase("StartUppercase", "startUppercase")]
        [TestCase("_StartNonAlpha", "_StartNonAlpha")]
        [TestCase(EmptyString, EmptyString)]
        public void StartLowercase(string inputString, string expectedOutput) {
            var input = Given("An input string", () => inputString);

            var result = When("Getting the string with lowercase first letter", () => inputString.StartLowercase());

            Then("The expected string is returned.",
                    expectedOutput,
                    (expected) => Verify.That(result.IsEqualTo(expected)));
        }

        [TestCase("Car", "Car")]
        [TestCase("ICar", "Car")]
        [TestCase("IInput", "Input")]
        [TestCase("Input", "Input")]
        [TestCase("_StartNonAlpha", "_StartNonAlpha")]
        [TestCase(EmptyString, EmptyString)]
        public void RemoveLeadingI(string inputString, string expectedOutput) {
            var input = Given("An input string", () => inputString);

            var result = When("Removing the leading I", () => inputString.RemoveLeadingI());

            Then("The expected string is returned.",
                    expectedOutput,
                    (expected) => Verify.That(result.IsEqualTo(expected)));
        }

        [TestCase(EmptyString, StringCasing.Camel, true)]
        [TestCase("containsNonAlpha!", StringCasing.Camel, false)]
        [TestCase("StartsWithCapital", StringCasing.Camel, false)]
        [TestCase("7StartsWithNumber", StringCasing.Camel, false)]
        [TestCase("validCamelCase", StringCasing.Camel, true)]
        [TestCase("validCamelCaseNumeric9", StringCasing.Camel, true)]
        [TestCase(EmptyString, StringCasing.Caps, true)]
        [TestCase("CONTAINS_NON_ALPHA!", StringCasing.Caps, false)]
        [TestCase("CONTAINS_Lowercase", StringCasing.Caps, false)]
        [TestCase("DOUBLE__UNDERSCORE", StringCasing.Caps, false)]
        [TestCase("7STARTS_WITH_NUMBER", StringCasing.Caps, false)]
        [TestCase("VALID_CAPS_CASE", StringCasing.Caps, true)]
        [TestCase("VALID_CAPS_CASE_NUMERIC9", StringCasing.Caps, true)]
        [TestCase("_UNDERSCORE_PREFIX", StringCasing.Caps, true)]
        [TestCase("__DOUBLE_UNDERSCORE_PREFIX", StringCasing.Caps, true)]
        [TestCase("UNDERSCORE_SUFFIX_", StringCasing.Caps, true)]
        [TestCase("DOUBLE_UNDERSCORE_SUFFIX__", StringCasing.Caps, false)]
        [TestCase(EmptyString, StringCasing.Kebab, true)]
        [TestCase("contains-non-alpha!", StringCasing.Kebab, false)]
        [TestCase("contains-Uppercase", StringCasing.Kebab, false)]
        [TestCase("double--kebab", StringCasing.Kebab, false)]
        [TestCase("7starts-with-number", StringCasing.Kebab, false)]
        [TestCase("valid-kebab-case", StringCasing.Kebab, true)]
        [TestCase("valid-kebab-case-numeric9", StringCasing.Kebab, true)]
        [TestCase(EmptyString, StringCasing.Pascal, true)]
        [TestCase("ContainsNonAlpha!", StringCasing.Pascal, false)]
        [TestCase("startsWithLower", StringCasing.Pascal, false)]
        [TestCase("7StartsWithNumber", StringCasing.Pascal, false)]
        [TestCase("ValidPascalCase", StringCasing.Pascal, true)]
        [TestCase("ValidPascalCaseNumeric9", StringCasing.Pascal, true)]
        [TestCase(EmptyString, StringCasing.Snake, true)]
        [TestCase("contains_non_alpha!", StringCasing.Snake, false)]
        [TestCase("contains_Uppercase", StringCasing.Snake, false)]
        [TestCase("double__underscore", StringCasing.Snake, false)]
        [TestCase("7starts_with_number", StringCasing.Snake, false)]
        [TestCase("valid_snake_case", StringCasing.Snake, true)]
        [TestCase("valid_snake_case_numeric", StringCasing.Snake, true)]
        [TestCase("_underscore_prefix", StringCasing.Snake, true)]
        [TestCase("__double_underscore_prefix", StringCasing.Snake, true)]
        [TestCase("underscore_suffix_", StringCasing.Snake, true)]
        [TestCase("double_underscore_suffix__", StringCasing.Snake, false)]
        public void ValidatingCase(
                string inputString,
                StringCasing inputCasing,
                bool isValid) {
            var input = Given("An input string", () => inputString);

            var result = When($"Validating case {inputCasing}",
                    () => inputString.FromCase(inputCasing));

            Then("The expected validation result was returned.",
                    isValid,
                    (expected) => Verify.That(result.IsValid.IsEqualTo(expected)));
        }

        [TestCase(EmptyString, StringCasing.Camel, EmptyString)]
        [TestCase("single", StringCasing.Camel, "SINGLE")]
        [TestCase("twoWords", StringCasing.Camel, "TWO_WORDS")]
        [TestCase("alphaNumeric3", StringCasing.Camel, "ALPHA_NUMERIC3")]
        [TestCase(EmptyString, StringCasing.Caps, EmptyString)]
        [TestCase("SINGLE", StringCasing.Caps, "SINGLE")]
        [TestCase("TWO_WORDS", StringCasing.Caps, "TWO_WORDS")]
        [TestCase("ALPHA_NUMERIC3", StringCasing.Caps, "ALPHA_NUMERIC3")]
        [TestCase("ALPHA_NUMERIC_4", StringCasing.Caps, "ALPHA_NUMERIC_4")]
        [TestCase("_UNDERSCORE_PREFIX", StringCasing.Caps, "UNDERSCORE_PREFIX")]
        [TestCase("__DOUBLE_UNDERSCORE_PREFIX", StringCasing.Caps, "DOUBLE_UNDERSCORE_PREFIX")]
        [TestCase("UNDERSCORE_SUFFIX_", StringCasing.Caps, "UNDERSCORE_SUFFIX")]
        [TestCase(EmptyString, StringCasing.Kebab, EmptyString)]
        [TestCase("single", StringCasing.Kebab, "SINGLE")]
        [TestCase("two-words", StringCasing.Kebab, "TWO_WORDS")]
        [TestCase("alpha-numeric3", StringCasing.Kebab, "ALPHA_NUMERIC3")]
        [TestCase("alpha-numeric-4", StringCasing.Kebab, "ALPHA_NUMERIC_4")]
        [TestCase(EmptyString, StringCasing.Pascal, EmptyString)]
        [TestCase("Single", StringCasing.Pascal, "SINGLE")]
        [TestCase("TwoWords", StringCasing.Pascal, "TWO_WORDS")]
        [TestCase("AlphaNumeric3", StringCasing.Pascal, "ALPHA_NUMERIC3")]
        [TestCase(EmptyString, StringCasing.Snake, EmptyString)]
        [TestCase("single", StringCasing.Snake, "SINGLE")]
        [TestCase("two_words", StringCasing.Snake, "TWO_WORDS")]
        [TestCase("alpha_numeric3", StringCasing.Snake, "ALPHA_NUMERIC3")]
        [TestCase("alpha_numeric_4", StringCasing.Snake, "ALPHA_NUMERIC_4")]
        [TestCase("_underscore_prefix", StringCasing.Snake, "UNDERSCORE_PREFIX")]
        [TestCase("__double_underscore_prefix", StringCasing.Snake, "DOUBLE_UNDERSCORE_PREFIX")]
        [TestCase("underscore_suffix_", StringCasing.Snake, "UNDERSCORE_SUFFIX")]
        public void ConvertingFromCase(
                string inputString,
                StringCasing inputCasing,
                string expectedOutput) {
            var input = Given("An input string", () => inputString);

            var result = When($"Converting from {inputCasing}",
                    () => inputString.FromCase(inputCasing).ToCase(StringCasing.Caps));

            var resultString = Then("The conversion was successful", () => result.OrThrow());
            Then("The expected string is returned.",
                    expectedOutput,
                    (expected) => Verify.That(resultString.IsEqualTo(expected)));
        }

        [TestCase(EmptyString, StringCasing.Camel, EmptyString)]
        [TestCase("SINGLE", StringCasing.Camel, "single")]
        [TestCase("TWO_WORDS", StringCasing.Camel, "twoWords")]
        [TestCase("ALPHA_NUMERIC3", StringCasing.Camel, "alphaNumeric3")]
        [TestCase("ALPHA_NUMERIC_4", StringCasing.Camel, "alphaNumeric4")]
        [TestCase(EmptyString, StringCasing.Caps, EmptyString)]
        [TestCase("SINGLE", StringCasing.Caps, "SINGLE")]
        [TestCase("TWO_WORDS", StringCasing.Caps, "TWO_WORDS")]
        [TestCase("ALPHA_NUMERIC3", StringCasing.Caps, "ALPHA_NUMERIC3")]
        [TestCase("ALPHA_NUMERIC_4", StringCasing.Caps, "ALPHA_NUMERIC_4")]
        [TestCase(EmptyString, StringCasing.Kebab, EmptyString)]
        [TestCase("SINGLE", StringCasing.Kebab, "single")]
        [TestCase("TWO_WORDS", StringCasing.Kebab, "two-words")]
        [TestCase("ALPHA_NUMERIC3", StringCasing.Kebab, "alpha-numeric3")]
        [TestCase("ALPHA_NUMERIC_4", StringCasing.Kebab, "alpha-numeric-4")]
        [TestCase(EmptyString, StringCasing.Pascal, EmptyString)]
        [TestCase("SINGLE", StringCasing.Pascal, "Single")]
        [TestCase("TWO_WORDS", StringCasing.Pascal, "TwoWords")]
        [TestCase("ALPHA_NUMERIC3", StringCasing.Pascal, "AlphaNumeric3")]
        [TestCase("ALPHA_NUMERIC_4", StringCasing.Pascal, "AlphaNumeric4")]
        [TestCase(EmptyString, StringCasing.Snake, EmptyString)]
        [TestCase("SINGLE", StringCasing.Snake, "single")]
        [TestCase("TWO_WORDS", StringCasing.Snake, "two_words")]
        [TestCase("ALPHA_NUMERIC3", StringCasing.Snake, "alpha_numeric3")]
        [TestCase("ALPHA_NUMERIC_4", StringCasing.Snake, "alpha_numeric_4")]
        public void ConvertingToCase(
                string inputString,
                StringCasing outputCasing,
                string expectedOutput) {
            var input = Given("An input string", () => inputString);

            var result = When($"Converting to {outputCasing}",
                    () => inputString.FromCase(StringCasing.Caps).ToCase(outputCasing));

            var resultString = Then("The conversion was successful", () => result.OrThrow());
            Then("The expected string is returned.",
                    expectedOutput,
                    (expected) => Verify.That(resultString.IsEqualTo(expected)));
        }

        [TestCase("\"", "\"\"")]
        [TestCase(EmptyString, EmptyString)]
        [TestCase("asdf", "asdf")]
        public void VerbatimStringEscape(string testStringUnescaped, string testStringEscaped) {
            Given("An unescaped input string.", () => testStringUnescaped);

            var result = When("The string is escaped.", testStringUnescaped.EscapeVerbatimString);
            Then("The string is escaped correctly.",
                    testStringEscaped,
                    (expected) => Verify.That(result.IsEqualTo(expected)));

            var unescapedResult = When("The result is unescaped.", result.UnescapeVerbatimString);
            Then("The string is unescaped correctly.",
                    testStringUnescaped,
                    (expected) => unescapedResult.IsEqualTo(expected));
        }

        [TestCase("\"", "\\\"")]
        [TestCase(EmptyString, EmptyString)]
        [TestCase("asdf", "asdf")]
        public void QuoteStringEscape(string testStringUnescaped, string testStringEscaped) {
            Given("An unescaped input string.", () => testStringUnescaped);

            var result = When("The string is escaped.", testStringUnescaped.EscapeStringQuotes);
            Then("The string is escaped correctly.",
                    testStringEscaped,
                    (expected) => Verify.That(result.IsEqualTo(expected)));

            var unescapedResult = When("The result is unescaped.", result.UnescapeStringQuotes);
            Then("The string is unescaped correctly.",
                    testStringUnescaped,
                    (expected) => unescapedResult.IsEqualTo(expected));
        }

        [Test]
        public void BuildStringBuildsAString() {
            var result = When("A string is built using BuildString.",
                    () => BuildString(sb => {
                        sb.Append("123");
                        sb.Append("456");
                    }));
            Then("The expected string was built.",
                    "123456",
                    (expected) => Verify.That(result.IsEqualTo(expected)));
        }

        [Test]
        public void BuildStringBuildsAnEmptyString() {
            var result = When("A string is built using BuildString.",
                    () => BuildString(sb => { }));
            Then("The expected string was built.",
                    EmptyString,
                    (expected) => Verify.That(result.IsEqualTo(expected)));
        }
    }
}
