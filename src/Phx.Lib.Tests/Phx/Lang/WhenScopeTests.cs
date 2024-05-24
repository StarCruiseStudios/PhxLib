// -----------------------------------------------------------------------------
// <copyright file="IfScopeTests.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class WhenScopeTests : LoggingTestClass {
        [TestCase(true, false, false, 1)]
        [TestCase(true, true, true, 1)]
        [TestCase(false, true, false, 2)]
        [TestCase(false, true, true, 2)]
        [TestCase(false, false, true, 3)]
        [TestCase(false, false, false, 4)]
        public void WhenReturnsExpected(bool condition1, bool condition2, bool condition3, int expected) {
            Given("Conditions",
                    () => (condition1, condition2, condition3));
            
            var result = When("The condition is evaluated.",
                    () => 10.When(it => condition1, () => 1)
                            .When(it => condition2, () => 2)
                            .When(it => condition3, () => 3)
                            .Else(() => 4));
            
            Then("The expected value is returned.", expected, expected => {
                Verify.That(result.IsEqualTo(expected));
            });
        }
    }
}
