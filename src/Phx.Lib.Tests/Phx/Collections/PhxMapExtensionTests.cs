// -----------------------------------------------------------------------------
//  <copyright file="PhxMapExtensionTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using NSubstitute;
using NUnit.Framework;
using Phx.Lang;
using Phx.Test;
using Phx.Validation;
using static Phx.Lang.Unit;

namespace Phx.Collections {
    using System.Collections.Generic;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class PhxMapExtensionTests : LoggingTestClass {
        [Test]
        public void GetOrDefaultReturnsExistingValue() {
            var collection = Given("A map.",
                () => Substitute.For<IPhxMap<string, string>>());
            var key = Given("A key", () => "key");
            var value = Given("A value", () => "value");
            _ = Given("The map contains the value for the key",
                () => collection.Get(Arg.Is(key)).Returns(Result.Success<string, Unit>(value)));

            var actual = Given("GetOrDefault is invoked for the key", () => collection.GetOrElse(key, () => "DEFAULT"));

            Then("The expected result is returned", value,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetOrDefaultReturnsDefaultValue() {
            var collection = Given("A map.",
                () => Substitute.For<IPhxMap<string, string>>());
            var key = Given("A key", () => "key");
            var defaultValue = Given("A default value", () => "DEFAULT");
            _ = Given("The map does not contain a value for the key",
                () => collection.Get(Arg.Is(key)).Returns(Result.Failure<string, Unit>(UNIT)));

            var actual = Given("GetOrDefault is invoked for the key", () => collection.GetOrElse(key, () => defaultValue));

            Then("The expected result is returned", defaultValue,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetOrElseReturnsExistingValue() {
            var collection = Given("A map.",
                () => Substitute.For<IPhxMap<string, string>>());
            var key = Given("A key", () => "key");
            var value = Given("A value", () => "value");
            _ = Given("The map contains the value for the key",
                () => collection.Get(Arg.Is(key)).Returns(Result.Success<string, Unit>(value)));

            var actual = Given("GetOrElse is invoked for the key", () => collection.GetOrElse(key, () => "DEFAULT"));

            Then("The expected result is returned", value,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetOrElseReturnsDefaultValue() {
            var collection = Given("A map.",
                () => Substitute.For<IPhxMap<string, string>>());
            var key = Given("A key", () => "key");
            var defaultValue = Given("A default value", () => "DEFAULT");
            _ = Given("The map does not contain a value for the key",
                () => collection.Get(Arg.Is(key)).Returns(Result.Failure<string, Unit>(UNIT)));

            var actual = Given("GetOrElse is invoked for the key", () => collection.GetOrElse(key, () => defaultValue));

            Then("The expected result is returned", defaultValue,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetValueReturnsExistingValue() {
            var collection = Given("A map.",
                () => Substitute.For<IPhxMap<string, string>>());
            var key = Given("A key", () => "key");
            var value = Given("A value", () => "value");
            _ = Given("The map contains the value for the key",
                () => collection.Get(Arg.Is(key)).Returns(Result.Success<string, Unit>(value)));

            var actual = Given("GetValue is invoked for the key", () => collection.GetOrThrow(key));

            Then("The expected result is returned", value,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [Test]
        public void GetValueThrowsOnNonExistingValue() {
            var collection = Given("A map.",
                () => Substitute.For<IPhxMap<string, string>>());
            var key = Given("A key", () => "key");
            _ = Given("The map does not contain a value for the key",
                () => collection.Get(Arg.Is(key)).Returns(Result.Failure<string, Unit>(UNIT)));

            var action = DeferredWhen("GetValue is invoked for the key",
                () => collection.GetOrThrow(key));

            _ = Then("An InvalidOperationException is thrown",
                () => TestUtils.TestForError<KeyNotFoundException>(action));
        }
    }
}