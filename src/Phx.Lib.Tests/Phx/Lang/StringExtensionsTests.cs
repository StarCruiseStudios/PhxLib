// -----------------------------------------------------------------------------
//  <copyright file="StringExtensionsTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class StringExtensionsTests {
        [Test]
        public void TestCapsToPascalCase_EmptyString() {
            const string testStringAllCaps = "";
            const string testStringPascal = "";
            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_SingleWord() {
            const string testStringAllCaps = "HELLO";
            const string testStringPascal = "Hello";
            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_TwoWord() {
            const string testStringAllCaps = "HELLO_THERE";
            const string testStringPascal = "HelloThere";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_ThreeWord() {
            const string testStringAllCaps = "HELLO_THERE_YOU";
            const string testStringPascal = "HelloThereYou";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_TwoUnderscore() {
            const string testStringAllCaps = "HELLO__THERE";
            const string testStringPascal = "HelloThere";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_StartWithUnderscore() {
            const string testStringAllCaps = "_HELLO";
            const string testStringPascal = "Hello";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_StartWithTwoUnderscores() {
            const string testStringAllCaps = "__HELLO";
            const string testStringPascal = "Hello";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_EndWithUnderscore() {
            const string testStringAllCaps = "HELLO_";
            const string testStringPascal = "Hello";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestCapsToPascalCase_EndWithTwoUnderscores() {
            const string testStringAllCaps = "HELLO__";
            const string testStringPascal = "Hello";

            var result = testStringAllCaps.CapsToPascalCase().OrThrow();
            Verify.That(result.IsEqualTo(testStringPascal),
                    "Did not correctly convert string to PascalCase.");
        }

        [Test]
        public void TestEscapeVerbatimString() {
            const string testStringUnescaped = "\"";
            const string testStringEscaped = "\"\"";

            var result = testStringUnescaped.EscapeVerbatimString();
            Verify.That(result.IsEqualTo(testStringEscaped),
                    "Did not correctly escape verbatim string.");
        }

        [Test]
        public void TestEscapeQuoteString() {
            const string testStringUnescaped = "\"";
            const string testStringEscaped = "\\\"";

            var result = testStringUnescaped.EscapeStringQuotes();
            Verify.That(result.IsEqualTo(testStringEscaped),
                    "Did not correctly escape quoted string.");
        }
    }
}
