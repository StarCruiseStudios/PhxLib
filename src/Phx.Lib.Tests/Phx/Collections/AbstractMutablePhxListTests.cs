// -----------------------------------------------------------------------------
//  <copyright file="AbstractMutablePhxListTests.cs" company="DangerDan9631">
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
    public abstract class AbstractMutablePhxListTests : AbstractPhxCollectionsTestBase {
        [TestCase(5, 0, "11")]
        [TestCase(5, 1, "11")]
        public void SetIndexSetsExpectedValue(int numElements, int index, string expectedValue) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IMutablePhxList<string>, string>(elements));

            _ = When($"A value is set by index {index}", () => collection[index] = expectedValue);

            Then("The expected result is set", expectedValue,
                (expected) => Verify.That(collection[index].IsEqualTo(expected)));

            var collection2 = Given("Another collection constructed with those elements.",
                () => GetTestInstance<IMutablePhxList<string>, string>(elements));

            When($"A value is set using Set() {index}",
                () => collection2.Set(index, expectedValue));

            Then("The expected result is set", expectedValue,
                (expected) => Verify.That(collection2[index].IsEqualTo(expected)));
        }

        public void ListModificationOperationTest(
            IEnumerable<string> collectionValues, IPhxList<string> expectedValues,
            string when, Action<IMutablePhxList<string>> action
        ) {
            var collection = Given("A collection.",
                () => GetTestInstance<IMutablePhxList<string>, string>(collectionValues));

            When(when, () => action(collection));

            Then("The expected result is returned", expectedValues,
                (expected) => {
                    for (int i = 0; i < collection.Count; i++) {
                        Verify.That(collection[i].IsEqualTo(expected[i]));
                    }
                });
        }

        public static IEnumerable<TestCaseData> InsertValues() {
            yield return new TestCaseData(ListOf("1", "2", "3"), "4", 1, ListOf("1", "4", "2", "3"));
            yield return new TestCaseData(EmptyList<string>(), "4", 0, ListOf("4"));
            yield return new TestCaseData(ListOf("1", "2", "3"), "4", 0, ListOf("4", "1", "2", "3"));
            yield return new TestCaseData(ListOf("1", "2", "3"), "4", 3, ListOf("1", "2", "3", "4"));
        }

        [Test, TestCaseSource(nameof(InsertValues))]
        public void InsertSetsExpectedValues(
            IEnumerable<string> collectionValues,
            string termToInsert, int indexToInsert,
            IPhxList<string> expectedValues
        ) {
            ListModificationOperationTest(collectionValues, expectedValues,
                $"{termToInsert} is inserted at index {indexToInsert}",
                (collection) => collection.Insert(indexToInsert, termToInsert));
        }

        public static IEnumerable<TestCaseData> InsertAllValues() {
            yield return new TestCaseData(ListOf("1", "2", "3"), ListOf("4", "5"), 1, ListOf("1", "4", "5", "2", "3"));
            yield return new TestCaseData(EmptyList<string>(), ListOf("4", "5"), 0, ListOf("4", "5"));
            yield return new TestCaseData(ListOf("1", "2", "3"), ListOf("4", "5"), 0, ListOf("4", "5", "1", "2", "3"));
            yield return new TestCaseData(ListOf("1", "2", "3"), ListOf("4", "5"), 3, ListOf("1", "2", "3", "4", "5"));
        }

        [Test, TestCaseSource(nameof(InsertAllValues))]
        public void InsertAllSetsExpectedValues(
            IEnumerable<string> collectionValues,
            IEnumerable<string> termsToInsert, int indexToInsert,
            IPhxList<string> expectedValues
        ) {
            ListModificationOperationTest(collectionValues, expectedValues,
                $"{termsToInsert} are inserted at index {indexToInsert}",
                (collection) => collection.InsertAll(indexToInsert, termsToInsert));
        }

        public static IEnumerable<TestCaseData> RemoveAtValues() {
            yield return new TestCaseData(ListOf("1", "2", "3"), 1, ListOf("1", "3"));
            yield return new TestCaseData(ListOf("1", "2", "3"), 0, ListOf("2", "3"));
        }

        [Test, TestCaseSource(nameof(RemoveAtValues))]
        public void RemoveAtSetsExpectedValues(
            IEnumerable<string> collectionValues, int indexToRemove, IPhxList<string> expectedValues
        ) {
            ListModificationOperationTest(collectionValues, expectedValues,
                $"term at index {indexToRemove} is removed",
                (collection) => collection.RemoveAt(indexToRemove));
        }

        public static IEnumerable<TestCaseData> ModificationOperationOutOfBounds() {
            yield return new TestCaseData(
                "Insert",
                (Action<IMutablePhxList<string>, int>) ((collection, index) => collection.Insert(index, "AnyValue")));
            yield return new TestCaseData(
                "InsertAll",
                (Action<IMutablePhxList<string>, int>) ((collection, index) => collection.InsertAll(index, ListOf("AnyValue"))));
            yield return new TestCaseData(
                "RemoveAt",
                (Action<IMutablePhxList<string>, int>) ((collection, index) => collection.RemoveAt(index)));
            yield return new TestCaseData(
                "Set by index",
                (Action<IMutablePhxList<string>, int>) ((collection, index) => collection[index] = "AnyValue"));
            yield return new TestCaseData(
                "Set by Set()",
                (Action<IMutablePhxList<string>, int>) ((collection, index) => collection.Set(index, "AnyValue")));
        }

        [Test, TestCaseSource(nameof(ModificationOperationOutOfBounds))]
        public void ModificationOperationOutOfBoundsThrows(
            string operation, Action<IMutablePhxList<string>, int> operationAction
        ) {
            var elements = Given("A collection of elements", () => CreateElements(5));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IMutablePhxList<string>, string>(elements));

            var action = When($"{operation} is performed with a negative index",
                () => (Action) (() => operationAction(collection, -1)));

            _ = Then("An IndexOutOfRangeException is thrown",
                () => TestUtils.TestForError<IndexOutOfRangeException>(action));

            var action2 = When($"{operation} is performed with a large index",
                () => (Action) (() => operationAction(collection, 100)));

            _ = Then("An IndexOutOfRangeException is thrown",
                () => TestUtils.TestForError<IndexOutOfRangeException>(action2));
        }
    }
}