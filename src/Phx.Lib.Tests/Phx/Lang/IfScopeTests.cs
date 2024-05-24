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
    using static ScopeFunctions;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class IfScopeTests : LoggingTestClass {
        [TestCase(true, "then")]
        [TestCase(false, "else")]
        public void IfElseReturnsExpected(bool condition, string expected) {
            Given("A condition.",
                    () => condition);
            var thenValue = Given("A then value.",
                    () => "then");
            var elseValue = Given("An else value.",
                    () => "else");
            
            var result = When("The condition is evaluated.",
                    () => If(condition, () => thenValue)
                            .Else(() => elseValue));
            
            Then("The expected value is returned.", expected, expected => {
                Verify.That(result.IsEqualTo(expected));
            });
        }
        
        [TestCase(true, false, "then")]
        [TestCase(true, true, "then")]
        [TestCase(false, true, "elseIf")]
        [TestCase(false, false, "else")]
        public void ElseIfReturnsExpected(bool condition1, bool condition2, string expected) {
            Given("A first condition.",
                    () => condition1);
            Given("A second condition.",
                    () => condition2);
            var thenValue = Given("A then value.",
                    () => "then");
            var elseIfValue = Given("An elseIf value.",
                    () => "elseIf");
            var elseValue = Given("An else value.",
                    () => "else");
            
            var result = When("The condition is evaluated.",
                    () => If(condition1, () => thenValue)
                            .ElseIf(condition2, () => elseIfValue)
                            .Else(() => elseValue));
            
            Then("The expected value is returned.", expected, expected => {
                Verify.That(result.IsEqualTo(expected));
            });
        }
    }
}
