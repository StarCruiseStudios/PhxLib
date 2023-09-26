// -----------------------------------------------------------------------------
//  <copyright file="PhxSetExtensionTests.cs" company="DangerDan9631">
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
    public class PhxSetExtensionTests : LoggingTestClass {
        [Test]
        public void IsEquivalentParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("IsEquivalent is invoked with params", () => container.IsEquivalent("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().IsEquivalent(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void IsSubsetOfParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("IsSubsetOf is invoked with params", () => container.IsSubsetOf("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().IsSubsetOf(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void IsProperSubsetOfParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("IsProperSubsetOf is invoked with params", () => container.IsProperSubsetOf("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().IsProperSubsetOf(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void IsSupersetOfParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("IsSupersetOf is invoked with params", () => container.IsSupersetOf("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().IsSupersetOf(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void IsProperSupersetOfParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("IsProperSupersetOf is invoked with params", () => container.IsProperSupersetOf("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().IsProperSupersetOf(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void GetSubtractionParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("GetSubtraction is invoked with params", () => container.GetSubtraction("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().GetSubtraction(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void GetSymmetricSubtractionParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("GetSymmetricSubtraction is invoked with params",
                () => container.GetSymmetricSubtraction("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().GetSymmetricSubtraction(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void GetIntersectionParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("GetIntersection is invoked with params", () => container.GetIntersection("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().GetIntersection(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void GetUnionWithParamsInvokesCorrectMethod() {
            var container = Given("A collection.",
                () => Substitute.For<IPhxSet<string>>());
            _ = When("GetUnionWith is invoked with params", () => container.GetUnion("hello", "there"));

            _ = Then("The right method was invoked",
                () => container.Received().GetUnion(Arg.Any<IEnumerable<string>>()));
        }
    }
}