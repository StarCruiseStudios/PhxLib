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
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class LateInitTests : LoggingTestClass {
        [Test]
        public void LateInitIsAssignableToNonNullValue() {
            var action = DeferredWhen("A non-nullable value is assigned to a Late.Init value.",
                    () => {
                        object x = Late.Init<object>();
                    });
            Then("Assigning to Late.Init does not throw.",
                    () => action());
        }
        
        [Test]
        public void LateInitCanBeCheckedForInitialization() {
            var x = Given<object>("A Late.Init value.",
                    () => Late.Init<object>());
            var result = When("The Late.Init value is checked for initialization.",
                    () => Late.IsInitialized<object>(x));
            Then("The Late.Init value is not initialized.",
                    () => result.IsEqualTo(false));
        }
        
        [Test]
        public void InitializedLateInitCanBeCheckedForInitialization() {
            var x = Given<object>("A Late.Init value.",
                    () => Late.Init<object>());
            x = Given("The Late.Init value is assigned a non-null value.",
                    () => new object());
            var result = When("The Late.Init value is checked for initialization.",
                    () => Late.IsInitialized<object>(x));
            Then("The Late.Init value is initialized.",
                    () => result.IsEqualTo(true));
        }
        
        [Test]
        public void ANonLateInitValueCanBeCheckedForInitialization() {
            var x = Given<object>("A non-Late.Init value.",
                    () => new object());
            var result = When("The value is checked for initialization.",
                    () => Late.IsInitialized<object>(x));
            Then("The value is initialized.",
                    () => result.IsEqualTo(true));
        }
    }
}
