// -----------------------------------------------------------------------------
//  <copyright file="PhxContainerExtensionTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using NSubstitute;
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class PhxContainerExtensionTests : LoggingTestClass {
        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(10, false)]
        public void IsEmptyReturnsExpectedValue(int numElements, bool shouldBeEmpty) {
            var container = Given($"An collection with {numElements} elements.",
                    () => {
                        var sub = Substitute.For<IPhxContainer<string>>();
                        _ = sub.Count.Returns(numElements);
                        return sub;
                    });

            var actual = When("The collection is checked for emptiness", () => container.IsEmpty());

            Then("The expected result is returned",
                    shouldBeEmpty,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void IsNotEmptyReturnsExpectedValue(int numElements, bool shouldNonBeEmpty) {
            var container = Given($"An collection with {numElements} elements.",
                    () => {
                        var sub = Substitute.For<IPhxContainer<string>>();
                        _ = sub.Count.Returns(numElements);
                        return sub;
                    });

            var actual = When("The collection is checked for non emptiness", () => container.IsNotEmpty());

            Then("The expected result is returned",
                    shouldNonBeEmpty,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(10, false)]
        [TestCase(null, true)]
        public void IsNullOrEmptyReturnsExpectedValue(int? numElements, bool shouldBeEmpty) {
            var container = Given($"An collection with {numElements} elements.",
                    () => {
                        if (numElements is null) {
                            return null;
                        }

                        var sub = Substitute.For<IPhxContainer<string>>();
                        _ = sub.Count.Returns((int)numElements!);

                        return sub;
                    });

            var actual = When("The collection is checked for emptiness or nullness", () => container.IsNullOrEmpty());

            Then("The expected result is returned",
                    shouldBeEmpty,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }
    }
}
