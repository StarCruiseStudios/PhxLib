// -----------------------------------------------------------------------------
//  <copyright file="AbstractMutablePhxSetTests.cs" company="DangerDan9631">
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
    public abstract class AbstractMutablePhxSetTests : AbstractPhxCollectionsTestBase {
        public void BinarySetOperationTest(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues,
            string when, Action<IMutablePhxSet<string>, IEnumerable<string>> action) {
            var collection = Given("A collection.",
                () => GetTestInstance<IMutablePhxSet<string>, string>(collectionValues));
            var other = Given("Another collection", () => otherValues);
            When(when, () => action(collection, other));

            Then("The expected result is returned", expectedValues,
                (expected) => Verify.That(collection.IsEquivalent(expected).IsTrue()));
        }

        public static IEnumerable<TestCaseData> SubtractValues() {
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

        [Test, TestCaseSource(nameof(SubtractValues))]
        public void SubtractSetsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is subtracted from the collection",
                (collection, other) => collection.Subtract(other));
        }

        public static IEnumerable<TestCaseData> SymmetricSubtractValues() {
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

        [Test, TestCaseSource(nameof(SymmetricSubtractValues))]
        public void SymmetricSubtractSetsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is symmetrically subtracted from the collection",
                (collection, other) => collection.SymmetricSubtract(other));
        }

        public static IEnumerable<TestCaseData> IntersectValues() {
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

        [Test, TestCaseSource(nameof(IntersectValues))]
        public void IntersectSetsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is intersected with the collection",
                (collection, other) => collection.Intersect(other));
        }

        public static IEnumerable<TestCaseData> UnionValues() {
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

        [Test, TestCaseSource(nameof(UnionValues))]
        public void UnionSetsExpectedValue(
            IEnumerable<string> collectionValues, IEnumerable<string> otherValues, IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues, otherValues, expectedValues,
                "The other collection is unioned with the collection",
                (collection, other) => collection.Union(other));
        }
    }
}