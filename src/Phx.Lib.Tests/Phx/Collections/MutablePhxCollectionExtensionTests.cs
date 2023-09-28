// -----------------------------------------------------------------------------
//  <copyright file="MutablePhxCollectionExtensionTests.cs" company="Star Cruise Studios LLC">
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
    public class MutablePhxCollectionExtensionTests : LoggingTestClass {
        [Test]
        public void AddAllParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableCollection<string>>());
            var actual = When("AddAll is invoked with params", () => container.AddAll("hello", "there"));

            _ = Then("AddAll was invoked",
                    () => container.Received().AddAll(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void RemoveAllParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableCollection<string>>());
            var actual = When("RemoveAll is invoked with params", () => container.RemoveAll("hello", "there"));

            _ = Then("RemoveAll was invoked",
                    () => container.Received().RemoveAll(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void RetainOnlyParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableCollection<string>>());
            var actual = When("RetainOnly is invoked with params", () => container.RetainOnly("hello", "there"));

            _ = Then("RetainOnly was invoked",
                    () => container.Received().RetainOnly(Arg.Any<IEnumerable<string>>()));
        }
    }
}
