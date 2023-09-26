// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxCollectionTests.cs" company="DangerDan9631">
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
    public abstract class AbstractPhxSetTests : AbstractPhxCollectionsTestBase {
        private void SetComparisonTestCase(int numElements, int minOtherElement, int numOtherElements,
            string when, Func<IPhxSet<string>, IEnumerable<string>, bool> action,
            bool expectedResult
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                "A collection constructed with those elements.",
                () => GetTestInstance<IPhxSet<string>, string>(elements));
            var other = Given("Another collection of items", () => CreateElements(numOtherElements, minOtherElement));
            var actual = When(when, () => action(collection, other));

            Then("The expected result is returned", expectedResult,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(5, 0, 3, false)]
        [TestCase(5, 0, 6, false)]
        [TestCase(5, 6, 3, false)]
        [TestCase(5, 0, 5, true)]
        public void IsEquivalentReturnsExpectedValue(int num, int min, int numOther, bool shouldBeEquivalent) {
            SetComparisonTestCase(num, min, numOther,
                "The collection is checked for equivalence with the other collection",
                (collection, other) => collection.IsEquivalent(other),
                shouldBeEquivalent);
        }

        [TestCase(5, 0, 3, false)]
        [TestCase(5, 0, 6, true)]
        [TestCase(5, 6, 3, false)]
        [TestCase(5, 0, 5, true)]
        public void IsSubsetOfReturnsExpectedValue(int num, int min, int numOther, bool shouldBeSubset) {
            SetComparisonTestCase(num, min, numOther,
                "The collection is checked if it is a subset of the other collection",
                (collection, other) => collection.IsSubsetOf(other),
                shouldBeSubset);
        }

        [TestCase(5, 0, 3, false)]
        [TestCase(5, 0, 6, true)]
        [TestCase(5, 6, 3, false)]
        [TestCase(5, 0, 5, false)]
        public void IsProperSubsetOfReturnsExpectedValue(int num, int min, int numOther, bool shouldBeSubset) {
            SetComparisonTestCase(num, min, numOther,
                "The collection is checked if it is a proper subset of the other collection",
                (collection, other) => collection.IsProperSubsetOf(other),
                shouldBeSubset);
        }

        [TestCase(5, 0, 3, true)]
        [TestCase(5, 0, 6, false)]
        [TestCase(5, 6, 3, false)]
        [TestCase(5, 0, 5, true)]
        public void IsSupersetOfReturnsExpectedValue(int num, int min, int numOther, bool shouldBeSuperset) {
            SetComparisonTestCase(num, min, numOther,
                "The collection is checked if it is a superset of the other collection",
                (collection, other) => collection.IsSupersetOf(other),
                shouldBeSuperset);
        }

        [TestCase(5, 0, 3, true)]
        [TestCase(5, 0, 6, false)]
        [TestCase(5, 6, 3, false)]
        [TestCase(5, 0, 5, false)]
        public void IsProperSupersetOfReturnsExpectedValue(int num, int min, int numOther, bool shouldBeSuperset) {
            SetComparisonTestCase(num, min, numOther,
                "The collection is checked if it is a proper superset of the other collection",
                (collection, other) => collection.IsProperSupersetOf(other),
                shouldBeSuperset);
        }

        [Test]
        public void GetPowerSet() {
            var collection = Given("A set of elements.", () => SetOf("1", "2", "3"));

            var actual = When("The power set of the collection is created", () => collection.GetPowerSet());

            Verify.That(actual.ContainsAny((item) => item.IsEquivalent(EmptySet<string>())).IsTrue());

            Then("The expected result is returned",
                SetOf(
                    EmptySet<string>(),
                    SetOf("1"),
                    SetOf("2"),
                    SetOf("3"),
                    SetOf("1", "2"),
                    SetOf("1", "3"),
                    SetOf("2", "3"),
                    SetOf("1", "2", "3")
                ),
                (expected) => {
                    Verify.That(actual.Count.IsEqualTo(expected.Count));
                    foreach (var set in expected) {
                        Verify.That(actual.ContainsAny((item) => item.IsEquivalent(set)).IsTrue());
                    }
                });
        }

        public void BinarySetOperationTest(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues,
            string when, Func<IPhxSet<string>, IEnumerable<string>, IPhxSet<string>> action) {
            var collection = Given("A collection.",
                () => GetTestInstance<IPhxSet<string>, string>(collectionValues));
            var other = Given("Another collection", () => otherValues);
            var actual = When(when, () => action(collection, other));

            Then("The expected result is returned", expectedValues,
                (expected) => Verify.That(actual.IsEquivalent(expected).IsTrue()));
        }

        public static IEnumerable<TestCaseData> GetSubtractionValues() {
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                EmptyList<string>(),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                EmptyList<string>(),
                ListOf("1", "2", "3"),
                EmptyList<string>()
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3"),
                EmptyList<string>()
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "3", "4"),
                ListOf("2")
            );
        }

        [Test, TestCaseSource(nameof(GetSubtractionValues))]
        public void GetSubtractionReturnsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is subtracted from the collection",
                (collection, other) => collection.GetSubtraction(other));
        }

        public static IEnumerable<TestCaseData> GetSymmetricSubtractionValues() {
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                EmptyList<string>(),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                EmptyList<string>(),
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3"),
                EmptyList<string>()
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "3", "4"),
                ListOf("2", "4")
            );
        }

        [Test, TestCaseSource(nameof(GetSymmetricSubtractionValues))]
        public void GetSymmetricSubtractionReturnsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is symmetrically subtracted from the collection",
                (collection, other) => collection.GetSymmetricSubtraction(other));
        }

        public static IEnumerable<TestCaseData> GetIntersectionValues() {
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                EmptyList<string>(),
                EmptyList<string>()
            );
            yield return new TestCaseData(
                EmptyList<string>(),
                ListOf("1", "2", "3"),
                EmptyList<string>()
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "3", "4"),
                ListOf("1", "3")
            );
        }

        [Test, TestCaseSource(nameof(GetIntersectionValues))]
        public void GetIntersectionReturnsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is intersected with the collection",
                (collection, other) => collection.GetIntersection(other));
        }

        public static IEnumerable<TestCaseData> GetUnionValues() {
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                EmptyList<string>(),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                EmptyList<string>(),
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3"),
                ListOf("1", "2", "3")
            );
            yield return new TestCaseData(
                ListOf("1", "2", "3"),
                ListOf("1", "3", "4"),
                ListOf("1", "2", "3", "4")
            );
        }

        [Test, TestCaseSource(nameof(GetUnionValues))]
        public void GetUnionReturnsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is unioned with the collection",
                (collection, other) => collection.GetUnion(other));
        }
    }
}