// -----------------------------------------------------------------------------
//  <copyright file="MutablePhxListExtensionTests.cs" company="DangerDan9631">
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
    public class MutablePhxListExtensionTests : LoggingTestClass {
        [Test]
        public void InsertAllParamsInvokesCorrectMethod() {
            var container = Given("A mutable collection.",
                () => Substitute.For<IMutablePhxList<string>>());
            When("InsertAll is invoked with params", () => container.InsertAll(0, "hello", "there"));

            Then("InsertAll was invoked",
                () => container.Received().InsertAll(Arg.Any<int>(), Arg.Any<IEnumerable<string>>()));
        }
    }
}