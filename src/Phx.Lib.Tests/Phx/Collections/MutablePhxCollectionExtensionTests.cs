// -----------------------------------------------------------------------------
//  <copyright file="PhxContainerExtensionTests.cs" company="DangerDan9631">
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
    public class MutablePhxCollectionExtensionTests : LoggingTestClass {
        [Test]
        public void AddAllParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxCollection<string>>());
            var actual = When("AddAll is invoked with params", () => container.AddAll("hello", "there"));

            _ = Then("AddAll was invoked",
                () => container.Received().AddAll(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void RemoveAllParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxCollection<string>>());
            var actual = When("RemoveAll is invoked with params", () => container.RemoveAll("hello", "there"));

            _ = Then("RemoveAll was invoked",
                () => container.Received().RemoveAll(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void RetainOnlyParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxCollection<string>>());
            var actual = When("RetainOnly is invoked with params", () => container.RetainOnly("hello", "there"));

            _ = Then("RetainOnly was invoked",
                () => container.Received().RetainOnly(Arg.Any<IEnumerable<string>>()));
        }
    }
}