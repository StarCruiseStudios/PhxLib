// -----------------------------------------------------------------------------
//  <copyright file="LateInitTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using NUnit.Framework;
    using Phx.Test;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class LateInitTests : LoggingTestClass {
        [Test]
        public void LateInitIsAssignableToNonNullValue() {
            Then("A Late.Init value can be assigned to a non null value.",
                    () => {
                        object x = Late.Init<object>();
                    });
        }
    }
}
