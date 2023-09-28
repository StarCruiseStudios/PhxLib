// -----------------------------------------------------------------------------
//  <copyright file="AbstractMutablePhxSetTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using static Phx.Collections.PhxCollections;

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    public abstract class AbstractMutablePhxSetTests : AbstractPhxCollectionsTestBase {
        public void BinarySetOperationTest(
                IEnumerable<string> collectionValues,
                IEnumerable<string> otherValues,
                IEnumerable<string> expectedValues,
                string when,
                Action<IPhxMutableSet<string>, IEnumerable<string>> action) {
            var collection = Given("A collection.",
                    () => GetTestInstance<IPhxMutableSet<string>, string>(collectionValues));
            var other = Given("Another collection", () => otherValues);
            When(when, () => action(collection, other));

            Then("The expected result is returned",
                    expectedValues,
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

        [Test] [TestCaseSource(nameof(SubtractValues))]
        public void SubtractSetsExpectedValue(
                IEnumerable<string> collectionValues,
                IEnumerable<string> otherValues,
                IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues,
                    otherValues,
                    expectedValues,
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

        [Test] [TestCaseSource(nameof(SymmetricSubtractValues))]
        public void SymmetricSubtractSetsExpectedValue(
                IEnumerable<string> collectionValues,
                IEnumerable<string> otherValues,
                IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues,
                    otherValues,
                    expectedValues,
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

        [Test] [TestCaseSource(nameof(IntersectValues))]
        public void IntersectSetsExpectedValue(
                IEnumerable<string> collectionValues,
                IEnumerable<string> otherValues,
                IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues,
                    otherValues,
                    expectedValues,
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

        [Test] [TestCaseSource(nameof(UnionValues))]
        public void UnionSetsExpectedValue(
                IEnumerable<string> collectionValues,
                IEnumerable<string> otherValues,
                IEnumerable<string> expectedValues
        ) {
            BinarySetOperationTest(collectionValues,
                    otherValues,
                    expectedValues,
                    "The other collection is unioned with the collection",
                    (collection, other) => collection.Union(other));
        }
    }
}
