// -----------------------------------------------------------------------------
//  <copyright file="PhxListExtensionTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using NSubstitute;
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class PhxListExtensionTests : LoggingTestClass {
        [TestCase(5, -1, false)]
        [TestCase(5, 0, true)]
        [TestCase(5, 2, true)]
        [TestCase(5, 4, true)]
        [TestCase(5, 5, false)]
        [TestCase(0, 0, true)]
        public void InBoundsReturnsCorrectValue(int numElements, int index, bool expectedValue) {
            var collection = Given($"An collection with {numElements} elements.",
                    () => {
                        var sub = Substitute.For<IPhxList<string>>();
                        _ = sub.Count.Returns(numElements);
                        return sub;
                    });
            _ = Given("An index to check", () => index);

            var actual = When("Checking if the index is in bounds for the collection",
                    () => collection.InBounds(index));

            Then("The expected result is returned",
                    expectedValue,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));

            var action = When("Requiring the index is in bounds for the collection",
                    () => (Action)(() => collection.RequireIndexInBounds(index)));

            if (expectedValue) {
                Then("No exception is thrown", action);
            } else {
                _ = Then("An IndexOutOfRangeException is thrown",
                        () => TestUtils.TestForError<IndexOutOfRangeException>(action));
            }
        }
    }
}
