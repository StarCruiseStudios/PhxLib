// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxListTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Phx.Test;
using Phx.Validation;
using static Phx.Collections.PhxCollections;

namespace Phx.Collections {
    public abstract class AbstractPhxListTests : AbstractPhxCollectionsTestBase {
        [TestCase(5, 0, "0")]
        [TestCase(5, 1, "1")]
        public void GetIndexReturnsExpectedValue(int numElements, int index, string expectedValue) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxList<string>, string>(elements));

            var actual = When($"A value is retrieved from index {index}", () => collection[index]);

            Then("The expected result is returned", expectedValue,
                (expected) => Verify.That(actual.IsEqualTo(expected)));

            var actual2 = When($"A value is retrieved using Get() {index}", () => collection.Get(index));

            Then("The result is the same", actual,
                (expected) => Verify.That(actual2.IsEqualTo(expected)));
        }

        [TestCase(5, -1)]
        [TestCase(5, 5)]
        public void GetIndexOutOfBoundsThrows(int numElements, int index) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxList<string>, string>(elements));

            var action = When($"A value is retrieved from index {index}",
                () => (Action) (() => _ = collection[index]));

            _ = Then("An IndexOutOfRangeException is thrown",
                () => TestUtils.TestForError<IndexOutOfRangeException>(action));

            var action2 = When($"A value is retrieved using Get() {index}",
                () => (Action) (() => _ = collection.Get(index)));

            _ = Then("An IndexOutOfRangeException is also thrown",
                () => TestUtils.TestForError<IndexOutOfRangeException>(action2));
        }

        public static IEnumerable<TestCaseData> IndexOfFirstValues() {
            yield return new TestCaseData(ListOf("1", "2", "3"), "2", 1);
            yield return new TestCaseData(ListOf("1", "2", "2"), "2", 1);
            yield return new TestCaseData(ListOf("1", "3", "4"), "2", -1);
        }

        [Test, TestCaseSource(nameof(IndexOfFirstValues))]
        public void IndexOfFirstReturnsExpectedValue(
            IEnumerable<string> collectionValues, string searchTerm, int expectedIndex
        ) {
            var collection = Given("A collection of elements.",
                () => GetTestInstance<IPhxList<string>, string>(collectionValues));

            var actual = When($"Searching for the first occurrence of {searchTerm}",
                () => collection.IndexOfFirst(searchTerm));

            if (expectedIndex == -1) {
                Then("The search failed", () => Verify.That(actual.IsFailure.IsTrue()));
            } else {
                Then("The search succeeded", () => Verify.That(actual.IsSuccess.IsTrue()));
                Then("The expected result is returned", expectedIndex,
                    (expected) => Verify.That(actual.OrThrow().IsEqualTo(expected)));
            }
        }

        public static IEnumerable<TestCaseData> IndexOfNextValues() {
            yield return new TestCaseData(ListOf("1", "2", "3"), "2", 0, 1);
            yield return new TestCaseData(ListOf("1", "2", "2"), "2", 0, 1);
            yield return new TestCaseData(ListOf("1", "2", "2"), "2", 1, 1);
            yield return new TestCaseData(ListOf("1", "2", "2"), "2", 2, 2);
            yield return new TestCaseData(ListOf("1", "3", "4"), "2", 0, -1);
        }

        [Test, TestCaseSource(nameof(IndexOfNextValues))]
        public void IndexOfNextReturnsExpectedValue(
            IEnumerable<string> collectionValues, string searchTerm, int startIndex, int expectedIndex
        ) {
            var collection = Given("A collection of elements.",
                () => GetTestInstance<IPhxList<string>, string>(collectionValues));

            var actual = When($"Searching for the first occurrence of {searchTerm} after index {startIndex}",
                () => collection.IndexOfNext(searchTerm, startIndex));

            if (expectedIndex == -1) {
                Then("The search failed", () => Verify.That(actual.IsFailure.IsTrue()));
            } else {
                Then("The search succeeded", () => Verify.That(actual.IsSuccess.IsTrue()));
                Then("The expected result is returned", expectedIndex,
                    (expected) => Verify.That(actual.OrThrow().IsEqualTo(expected)));
            }
        }

        [TestCase(5, -1)]
        [TestCase(5, 5)]
        public void IndexOfNextOutOfBoundsThrows(int numElements, int startIndex) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxList<string>, string>(elements));

            var action = When($"Searching from index {startIndex}",
                () => (Action) (() => collection.IndexOfNext("AnyString", startIndex)));

            _ = Then("An IndexOutOfRangeException is thrown",
                () => TestUtils.TestForError<IndexOutOfRangeException>(action));
        }

        public static IEnumerable<TestCaseData> IndexOfLastValues() {
            yield return new TestCaseData(ListOf("1", "2", "3"), "2", 1);
            yield return new TestCaseData(ListOf("1", "2", "2"), "2", 2);
            yield return new TestCaseData(ListOf("1", "3", "4"), "2", -1);
        }

        [Test, TestCaseSource(nameof(IndexOfLastValues))]
        public void IndexOfLastReturnsExpectedValue(
            IEnumerable<string> collectionValues, string searchTerm, int expectedIndex
        ) {
            var collection = Given("A collection of elements.",
                () => GetTestInstance<IPhxList<string>, string>(collectionValues));

            var actual = When($"Searching for the last occurrence of {searchTerm}",
                () => collection.IndexOfLast(searchTerm));

            if (expectedIndex == -1) {
                Then("The search failed", () => Verify.That(actual.IsFailure.IsTrue()));
            } else {
                Then("The search succeeded", () => Verify.That(actual.IsSuccess.IsTrue()));
                Then("The expected result is returned", expectedIndex,
                    (expected) => Verify.That(actual.OrThrow().IsEqualTo(expected)));
            }
        }
    }
}