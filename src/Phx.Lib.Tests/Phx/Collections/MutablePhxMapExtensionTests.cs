// -----------------------------------------------------------------------------
//  <copyright file="MutablePhxMapExtensionTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using static Phx.Lang.Unit;

namespace Phx.Collections {
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using Phx.Lang;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class MutablePhxMapExtensionTests : LoggingTestClass {
        [Test]
        public void GetOrInsertReturnsExistingValue() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            var key = Given("A key", () => "key");
            var value = Given("A value", () => "value");
            _ = Given("The map contains the value for the key",
                    () => collection.Get(Arg.Is(key)).Returns(Result.Success<string, Unit>(value)));

            var actual = Given("GetOrInsert is invoked for the key", () => collection.GetOrInsert(key, "DEFAULT"));

            Then("The expected result is returned",
                    value,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetOrInsertReturnsAndInsertsDefaultValue() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            var key = Given("A key", () => "key");
            var defaultValue = Given("A default value", () => "DEFAULT");
            _ = Given("The map does not contain a value for the key",
                    () => collection.Get(Arg.Is(key)).Returns(Result.Failure<string, Unit>(UNIT)));

            var actual = Given("GetOrInsert is invoked for the key", () => collection.GetOrInsert(key, defaultValue));

            Then("The expected result is returned",
                    defaultValue,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The default value was inserted",
                    () => collection.Received().Set(Arg.Is(key), Arg.Is(defaultValue)));
        }

        [Test]
        public void GetOrInsertWithPredicateReturnsExistingValue() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            var key = Given("A key", () => "key");
            var value = Given("A value", () => "value");
            _ = Given("The map contains the value for the key",
                    () => collection.Get(Arg.Is(key)).Returns(Result.Success<string, Unit>(value)));

            var actual = Given("GetOrInsert is invoked for the key",
                    () => collection.GetOrInsert(key, () => "DEFAULT"));

            Then("The expected result is returned",
                    value,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetOrInsertWithPredicateReturnsAndInsertsDefaultValue() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            var key = Given("A key", () => "key");
            var defaultValue = Given("A default value", () => "DEFAULT");
            _ = Given("The map does not contain a value for the key",
                    () => collection.Get(Arg.Is(key)).Returns(Result.Failure<string, Unit>(UNIT)));

            var actual = Given("GetOrInsert is invoked for the key",
                    () => collection.GetOrInsert(key, () => defaultValue));

            Then("The expected result is returned",
                    defaultValue,
                    (expected) => Verify.That(actual.IsEqualTo(expected)));
            Then("The default value was inserted",
                    () => collection.Received().Set(Arg.Is(key), Arg.Is(defaultValue)));
        }

        [Test]
        public void RemoveAllParamsInvokesCorrectMethod() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            _ = When("RemoveAll is invoked with params", () => collection.RemoveAll("hello", "there"));

            _ = Then("RemoveAll was invoked",
                    () => collection.Received().RemoveAll(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void RetainOnlyParamsInvokesCorrectMethod() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            _ = When("RetainOnly is invoked with params", () => collection.RetainOnly("hello", "there"));

            _ = Then("RetainOnly was invoked",
                    () => collection.Received().RetainOnly(Arg.Any<IEnumerable<string>>()));
        }

        [Test]
        public void SetAllParamsInvokesCorrectMethod() {
            var collection = Given("A map.",
                    () => Substitute.For<IPhxMutableMap<string, string>>());
            When("SetAll is invoked with params",
                    () => collection.SetAll(("hello", "there"), ("goodbye", "there")));

            Then("SetAll was invoked",
                    () => collection.Received().SetAll(Arg.Any<IEnumerable<(string, string)>>()));
        }
    }
}
