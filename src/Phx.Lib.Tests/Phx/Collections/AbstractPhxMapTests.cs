// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxMapTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using NUnit.Framework;
using Phx.Test;
using Phx.Validation;
using static Phx.Collections.PhxCollections;

namespace Phx.Collections {
    public abstract class AbstractPhxMapTests : AbstractPhxMapCollectionsTestBase {
        [TestCase(5, "0", "0")]
        [TestCase(5, "1", "1")]
        public void GetIndexReturnsExpectedValue(int numElements, string key, string expectedValue) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxMap<string, string>>(elements));

            var actual = When($"A value is retrieved from key {key}", () => collection[key]);

            Then("The expected result is returned", expectedValue,
                (expected) => Verify.That(actual.IsEqualTo(expected)));

            var actual2 = When($"A value is retrieved using Get() {key}", () => collection.Get(key));

            Then("The value was found", () => Verify.That(actual2.IsSuccess.IsTrue()));
            Then("The result is the same", actual,
                (expected) => Verify.That(actual2.OrThrow().IsEqualTo(expected)));
        }

        [Test]
        public void GetEntriesReturnsExpectedValues() {
            var elements = Given("A collection of elements", () => CreateElements(3));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxMap<string, string>>(elements));

            var actual = When("The entries are retrieved from the collection", () => collection.Entries);

            Then("The expected result is returned",
                SetOf(
                    new PhxPair<string, string>("0", "0"),
                    new PhxPair<string, string>("1", "1"),
                    new PhxPair<string, string>("2", "2")
                ),
                (expected) => {
                    Verify.That(actual.Count.IsEqualTo(expected.Count));
                    Verify.That(actual.ContainsAll(expected).IsTrue());
                    Verify.That(expected.ContainsAll(actual).IsTrue());
                });
        }

        [Test]
        public void GetKeysReturnsExpectedValues() {
            var elements = Given("A collection of elements", () => CreateElements(3));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxMap<string, string>>(elements));

            var actual = When("The keys are retrieved from the collection", () => collection.Keys);

            Then("The expected result is returned",
                SetOf("0", "1", "2"),
                (expected) => Verify.That(actual.IsEquivalent(expected).IsTrue()));
        }

        [Test]
        public void GetValuesReturnsExpectedValues() {
            const int numElements = 3;
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxMap<string, string>>(elements));

            var actual = When("The values are retrieved from the collection", () => collection.Values);

            Then("The expected result is returned",
                ListOf("0", "1", "2"),
                (expected) => {
                    Verify.That(actual.Count.IsEqualTo(expected.Count));
                    Verify.That(actual.ContainsAll(expected).IsTrue());
                });
        }

        [TestCase(5, "0", true)]
        [TestCase(5, "1", true)]
        [TestCase(5, "6", false)]
        public void ContainsKeyReturnsExpectedValue(int numElements, string key, bool expectedValue) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given("A collection constructed with those elements.",
                () => GetTestInstance<IPhxMap<string, string>>(elements));

            var actual = When($"A key {key} is searched for", () => collection.ContainsKey(key));

            Then("The expected result is returned", expectedValue,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }
    }
}