// -----------------------------------------------------------------------------
//  <copyright file="PhxContainerExtensionTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using NSubstitute;
using NUnit.Framework;
using Phx.Test;
using Phx.Validation;

namespace Phx.Collections {
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
                    var sub = Substitute.For<IPhxContainer>();
                    _ = sub.Count.Returns(numElements);
                    return sub;
                });

            var actual = When("The collection is checked for emptiness", () => container.IsEmpty());

            Then("The expected result is returned", shouldBeEmpty,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void IsNotEmptyReturnsExpectedValue(int numElements, bool shouldNonBeEmpty) {
            var container = Given($"An collection with {numElements} elements.",
                () => {
                    var sub = Substitute.For<IPhxContainer>();
                    _ = sub.Count.Returns(numElements);
                    return sub;
                });

            var actual = When("The collection is checked for non emptiness", () => container.IsNotEmpty());

            Then("The expected result is returned", shouldNonBeEmpty,
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

                    var sub = Substitute.For<IPhxContainer>();
                    _ = sub.Count.Returns((int) numElements!);

                    return sub;
                });

            var actual = When("The collection is checked for emptiness or nullness", () => container.IsNullOrEmpty());

            Then("The expected result is returned", shouldBeEmpty,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }
    }
}