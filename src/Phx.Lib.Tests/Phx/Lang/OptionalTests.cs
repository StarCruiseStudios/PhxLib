// -----------------------------------------------------------------------------
//  <copyright file="OptionalTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class OptionalTests : LoggingTestClass {
        private const string TEST_VALUE = "Hello";
        private const string ALT_TEST_VALUE = "Goodbye";

        [TestCase(TEST_VALUE, true)]
        [TestCase(null, false)]
        public void IsPresentWhenPresent(string? value, bool isPresent) {
            _ = Given("An optional value", () => value);
            var actual = When("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));
            Then("The optional is present if expected",
                    isPresent,
                    (expected) => Verify.That(actual.IsPresent.IsEqualTo(expected)));
            Then("The optional is empty if expected",
                    !isPresent,
                    (expected) => Verify.That(actual.IsEmpty.IsEqualTo(expected)));
        }

        [Test]
        public void ValueIsReturnedWhenPresent() {
            var value = Given("An optional value", () => TEST_VALUE);
            var optional = Given("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));

            var actual = When("The value is retrieved from the optional", () => optional.Value);

            Then("The expected value is returned", value, (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void ExceptionIsThrownWhenRetrievingValueOfEmpty() {
            var optional = Given("An empty optional", () => Optional<string>.EMPTY);

            var action = When("The value is retrieved from the optional", () => (Action)(() => _ = optional.Value));

            _ = Then("An InvalidOperationException is thrown",
                    () => TestUtils.TestForError<InvalidOperationException>(action));
        }

        [TestCase(TEST_VALUE, true)]
        [TestCase(null, false)]
        public void IfPresentExcecutesActionWhenPresent(string? value, bool isPresent) {
            _ = Given("An optional value", () => value);
            var optional = Given("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));
            bool ifPresentWasExecuted = false;
            When("IfPresent is invoked", () => optional.IfPresent((_) => ifPresentWasExecuted = true));
            Then("The action was executed if expected",
                    isPresent,
                    (expected) => Verify.That(ifPresentWasExecuted.IsEqualTo(expected)));

            bool ifEmptyWasExecuted = false;
            When("IfEmptyIsInvoked", () => optional.IfEmpty(() => ifEmptyWasExecuted = true));
            Then("The action was executed if expected",
                    !isPresent,
                    (expected) => Verify.That(ifEmptyWasExecuted.IsEqualTo(expected)));
        }

        [Test]
        public void MappedValueIsMappedWhenPresent() {
            var value = Given("An optional value", () => TEST_VALUE);
            var optional = Given("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));

            var actual = When("The value is mapped", () => optional.Map((_) => Optional<string>.Of(ALT_TEST_VALUE)));

            Then("The mapped value is present", () => Verify.That(actual.IsPresent.IsTrue()));
            Then("The value is mapped", ALT_TEST_VALUE, (expected) => Verify.That(actual.Value.IsEqualTo(expected)));
        }

        [Test]
        public void MappedValueIsMappedWhenNotPresent() {
            var optional = Given("An empty optional value", () => Optional<string>.EMPTY);

            var actual = When("The value is mapped", () => optional.Map((_) => Optional<string>.Of(ALT_TEST_VALUE)));

            Then("The mapped value is not present", () => Verify.That(actual.IsPresent.IsFalse()));
        }

        [TestCase(TEST_VALUE, true)]
        [TestCase(null, false)]
        public void OrElseProvidesValueWhenEmpty(string? value, bool isPresent) {
            _ = Given("An optional value", () => value);
            var optional = Given("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));

            var actual = When("OrElse is invoked", () => optional.OrElse(() => ALT_TEST_VALUE));
            Then("The expected value is returned",
                    isPresent
                            ? value
                            : ALT_TEST_VALUE,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(TEST_VALUE, true)]
        [TestCase(null, false)]
        public void OrTryProvidesValueWhenEmpty(string? value, bool isPresent) {
            _ = Given("An optional value", () => value);
            var optional = Given("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));

            var actual = When("OrTry is invoked", () => optional.OrTry(() => Optional<string>.Of(ALT_TEST_VALUE)));
            Then("The resultant optional value is present", () => Verify.That(actual.IsPresent.IsTrue()));
            Then("The expected value is returned",
                    isPresent
                            ? value
                            : ALT_TEST_VALUE,
                    (expected) => Verify.That(actual.Value.IsEqualTo(expected)));
        }

        [Test]
        public void OrElseThrowProvidesValueWhenPresent() {
            var value = Given("An optional value", () => TEST_VALUE);
            var optional = Given("The value is wrapped in an optional", () => Optional<string>.OfNullable(value));

            var actual = When("OrElseThrow is invoked", () => optional.OrElseThrow(() => new TestException()));

            Then("The expected value is returned", TEST_VALUE, (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void OrElseThrowThrowsExceptionWhenNotPresent() {
            var optional = Given("An empty optional value", () => Optional<string>.EMPTY);

            var action = When("OrElseThrow is invoked",
                    () => (Action)(() => optional.OrElseThrow(() => new TestException())));

            _ = Then("A TestException is thrown",
                    () => TestUtils.TestForError<TestException>(action));
        }
    }
}
