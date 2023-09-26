// -----------------------------------------------------------------------------
//  <copyright file="AbstractMutablePhxMapTests.cs" company="DangerDan9631">
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
    public abstract class AbstractMutablePhxMapTests : AbstractPhxMapCollectionsTestBase {
        [TestCase(5, "0", "9")]
        [TestCase(5, "1", "9")]
        [TestCase(5, "9", "9")]
        public void SetIndexSetsExpectedValue(int numElements, string key, string expectedValue) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IMutablePhxMap<string, string>>(elements));

            _ = When($"A value is set to '{expectedValue}' using key '{key}'", () => collection[key] = expectedValue);

            Then("The expected value is set", expectedValue,
                (expected) => Verify.That(collection[key].IsEqualTo(expected)));

            var collection2 = Given("Another collection constructed with those elements.",
                () => GetTestInstance<IMutablePhxMap<string, string>>(elements));
            When($"A value is set to '{expectedValue}' using Set({key})",
                () => collection2.Set(key, expectedValue));

            Then("The expected value is set", expectedValue,
                (expected) => Verify.That(collection2[key].IsEqualTo(expected)));
        }

        public void MapModificationOperationTest(
            IEnumerable<(string, string)> collectionValues, IPhxMap<string, string> expectedValues,
            string when, Action<IMutablePhxMap<string, string>> action
        ) {
            var collection = Given("A map.",
                () => GetTestInstance<IMutablePhxMap<string, string>>(collectionValues));

            When(when, () => action(collection));
            Log(collection.ToString()!);

            Then("The expected result is returned", expectedValues,
                (expected) => {
                    Verify.That(collection.Count.IsEqualTo(expected.Count));
                    foreach (var key in expected.Keys) {
                        Verify.That(collection[key].IsEqualTo(expected[key]));
                    }
                });
        }

        [Test]
        public void ClearRemovesAllValues() {
            MapModificationOperationTest(ListOf(("1", "1"), ("2", "2"), ("3", "3")), EmptyMap<string, string>(),
                "The map is cleared", (map) => map.Clear());
        }

        public static IEnumerable<TestCaseData> RemoveValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "2", MapOf(("1", "1"), ("3", "3")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "4", MapOf(("1", "1"), ("2", "2"), ("3", "3")));
            yield return new TestCaseData(
                EmptyList<(string, string)>(), "4", EmptyMap<string, string>());
        }

        [Test, TestCaseSource(nameof(RemoveValues))]
        public void RemoveRemovesExpectedValues(
            IEnumerable<(string, string)> collectionValues, string keyToRemove, IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"Key '{keyToRemove}' is removed from the map", (map) => map.Remove(keyToRemove));
        }

        public static IEnumerable<TestCaseData> RemoveAllValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), ListOf("2", "1"), MapOf(("3", "3")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), ListOf("4", "5"), MapOf(("1", "1"), ("2", "2"), ("3", "3")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), ListOf("1", "5"), MapOf(("2", "2"), ("3", "3")));
            yield return new TestCaseData(
                EmptyList<(string, string)>(), ListOf("1", "5"), EmptyMap<string, string>());
        }

        [Test, TestCaseSource(nameof(RemoveAllValues))]
        public void RemoveAllRemovesExpectedValues(
            IEnumerable<(string, string)> collectionValues, IEnumerable<string> keysToRemove, IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"Keys '{keysToRemove}' are removed from the map", (map) => map.RemoveAll(keysToRemove));
        }

        public static IEnumerable<TestCaseData> RemoveValuesValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "2", MapOf(("1", "1")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "4", MapOf(("1", "1"), ("2", "2"), ("3", "3")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "1", EmptyMap<string, string>());
            yield return new TestCaseData(
                EmptyList<(string, string)>(), "1", EmptyMap<string, string>());
        }

        [Test, TestCaseSource(nameof(RemoveValuesValues))]
        public void RemoveValuesRemovesExpectedValues(
            IEnumerable<(string, string)> collectionValues, string minValue, IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"Values greater than '{minValue}' are removed from the map",
                    (map) => map.RemoveValues((value) => value.CompareTo(minValue) >= 0));
        }

        public static IEnumerable<TestCaseData> RetainOnlyValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), ListOf("2", "1"), MapOf(("1", "1"), ("2", "2")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), ListOf("4", "5"), EmptyMap<string, string>());
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), ListOf("1", "5"), MapOf(("1", "1")));
            yield return new TestCaseData(
                EmptyList<(string, string)>(), ListOf("1", "5"), EmptyMap<string, string>());
        }

        [Test, TestCaseSource(nameof(RetainOnlyValues))]
        public void RetainOnlyRemovesExpectedValues(
            IEnumerable<(string, string)> collectionValues, IEnumerable<string> keysToRemove, IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"Keys '{keysToRemove}' are retained in the map", (map) => map.RetainOnly(keysToRemove));
        }

        public static IEnumerable<TestCaseData> RetainValuesValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "2", MapOf(("2", "2"), ("3", "3")));
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "4", EmptyMap<string, string>());
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")), "1", MapOf(("1", "1"), ("2", "2"), ("3", "3")));
            yield return new TestCaseData(
                EmptyList<(string, string)>(), "1", EmptyMap<string, string>());
        }

        [Test, TestCaseSource(nameof(RetainValuesValues))]
        public void RetainValuesRemovesExpectedValues(
            IEnumerable<(string, string)> collectionValues, string minValue, IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"Values greater than '{minValue}' are retained in the map",
                    (map) => map.RetainValues((value) => value.CompareTo(minValue) >= 0));
        }

        public static IEnumerable<TestCaseData> SetAllMapValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")),
                MapOf(("2", "9"), ("3", "10")),
                MapOf(("1", "1"), ("2", "9"), ("3", "10"))
            );
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")),
                MapOf(("2", "9"), ("4", "10")),
                MapOf(("1", "1"), ("2", "9"), ("3", "3"), ("4", "10"))
            );
            yield return new TestCaseData(
                EmptyList<(string, string)>(),
                MapOf(("1", "1"), ("2", "2")),
                MapOf(("1", "1"), ("2", "2"))
            );
        }

        [Test, TestCaseSource(nameof(SetAllMapValues))]
        public void SetAllWithMapSetsExpectedValues(
            IEnumerable<(string, string)> collectionValues,
            IPhxMap<string, string> valuesToSet,
            IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"{valuesToSet}' are set in the map",
                    (map) => map.SetAll(valuesToSet));
        }

        public static IEnumerable<TestCaseData> SetAllTupleValues() {
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")),
                ListOf(("2", "9"), ("3", "10")),
                MapOf(("1", "1"), ("2", "9"), ("3", "10"))
            );
            yield return new TestCaseData(
                ListOf(("1", "1"), ("2", "2"), ("3", "3")),
                ListOf(("2", "9"), ("4", "10")),
                MapOf(("1", "1"), ("2", "9"), ("3", "3"), ("4", "10"))
            );
            yield return new TestCaseData(
                EmptyList<(string, string)>(),
                ListOf(("1", "1"), ("2", "2")),
                MapOf(("1", "1"), ("2", "2"))
            );
        }

        [Test, TestCaseSource(nameof(SetAllTupleValues))]
        public void SetAllWithMapSetsExpectedValues(
            IEnumerable<(string, string)> collectionValues,
            IEnumerable<(string, string)> valuesToSet,
            IPhxMap<string, string> expectedValues
        ) {
            MapModificationOperationTest(collectionValues, expectedValues,
                $"{valuesToSet}' are set in the map",
                    (map) => map.SetAll(valuesToSet));
        }
    }
}