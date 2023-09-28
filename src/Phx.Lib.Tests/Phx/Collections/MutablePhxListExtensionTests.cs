// -----------------------------------------------------------------------------
//  <copyright file="MutablePhxListExtensionTests.cs" company="Star Cruise Studios LLC">
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
    public class MutablePhxListExtensionTests : LoggingTestClass {
        [Test]
        public void InsertAllParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                    () => Substitute.For<IPhxMutableList<string>>());
            When("InsertAll is invoked with params", () => container.InsertAll(0, "hello", "there"));

            Then("InsertAll was invoked",
                    () => container.Received().InsertAll(Arg.Any<int>(), Arg.Any<IEnumerable<string>>()));
        }
    }
}
