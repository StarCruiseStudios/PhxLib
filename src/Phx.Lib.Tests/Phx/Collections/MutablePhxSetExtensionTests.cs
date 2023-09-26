// -----------------------------------------------------------------------------
//  <copyright file="MutablePhxSetExtensionTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Phx.Test;

namespace Phx.Collections {
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class MutablePhxSetExtensionTests : LoggingTestClass {
        [Test]
        public void SubtractParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxSet<string>>());
            When("Subtract is invoked with params", () => container.Subtract("hello", "there"));

            Then("The right method was invoked",
                () => container.Received().Subtract(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void SymmetricSubtractParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxSet<string>>());
            When("SymmetricSubtract is invoked with params",
                () => container.SymmetricSubtract("hello", "there"));

            Then("The right method was invoked",
                () => container.Received().SymmetricSubtract(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void IntersectParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxSet<string>>());
            When("Intersect is invoked with params", () => container.Intersect("hello", "there"));

            Then("The right method was invoked",
                () => container.Received().Intersect(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void UnionParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxSet<string>>());
            When("Union is invoked with params", () => container.Union("hello", "there"));

            Then("The right method was invoked",
                () => container.Received().Union(Arg.Any<IEnumerable<string>>()));
        }
    }
}