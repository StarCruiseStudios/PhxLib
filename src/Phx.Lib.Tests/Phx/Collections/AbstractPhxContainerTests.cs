// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxContainerTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    public abstract class AbstractPhxContainerTests : AbstractPhxCollectionsTestBase {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void CountReturnsExpectedValue(int numElements) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var container = Given("An collection constructed with those elements.",
                    () => GetTestInstance<IPhxContainer<string>, string>(elements));

            var actual = When("The collection count is retrieved", () => container.Count);

            Then("The expected result is returned",
                    numElements,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(10, false)]
        public void IsEmptyReturnsExpectedValue(int numElements, bool shouldBeEmpty) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var container = Given("An collection constructed with those elements.",
                    () => GetTestInstance<IPhxContainer<string>, string>(elements));

            var actual = When("The collection is checked for emptiness", () => container.IsEmpty());

            Then("The expected result is returned",
                    shouldBeEmpty,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }
    }
}
