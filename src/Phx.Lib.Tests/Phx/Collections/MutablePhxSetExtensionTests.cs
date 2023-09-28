// -----------------------------------------------------------------------------
//  <copyright file="MutablePhxSetExtensionTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using Phx.Test;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class MutablePhxSetExtensionTests : LoggingTestClass {
        [Test]
        public void SubtractParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableSet<string>>());
            When("Subtract is invoked with params", () => container.Subtract("hello", "there"));

            Then("The right method was invoked",
                    () => container.Received().Subtract(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void SymmetricSubtractParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableSet<string>>());
            When("SymmetricSubtract is invoked with params",
                    () => container.SymmetricSubtract("hello", "there"));

            Then("The right method was invoked",
                    () => container.Received().SymmetricSubtract(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void IntersectParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableSet<string>>());
            When("Intersect is invoked with params", () => container.Intersect("hello", "there"));

            Then("The right method was invoked",
                    () => container.Received().Intersect(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void UnionParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableSet<string>>());
            When("Union is invoked with params", () => container.Union("hello", "there"));

            Then("The right method was invoked",
                    () => container.Received().Union(Arg.Any<IEnumerable<string>>()));
        }
    }
}
