// -----------------------------------------------------------------------------
//  <copyright file="TryTest.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using NSubstitute;
using NUnit.Framework;
using Phx.Test;
using Phx.Validation;
using System;

namespace Phx.Lang
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class TryTest : LoggingTestClass
    {
        [Test]
        public void TryAllTriesAllBlocksWhenExceptionsAreThrown()
        {
            var exception1 = Given("An exception", () => new TestException("1"));
            var exception2 = Given("Another exception", () => new TestException("2"));

            var action = When("The exceptions are thrown inside different blocks", () =>
                (Action)(() => Try.All(
                    () => throw exception1,
                    () => throw exception2
                ))
            );

            var aggregateException = Then("An aggregate exception is thrown",
                () => TestUtils.TestForError<AggregateException>(action));
            Then("Exactly two exceptions were thrown",
                () => Verify.That(aggregateException.InnerExceptions.Count.IsEqualTo(2)));
            Then("The first exception was caught",
                () => Verify.That(aggregateException.InnerExceptions.Contains(exception1).IsTrue()));
            Then("The second exception was caught",
                () => Verify.That(aggregateException.InnerExceptions.Contains(exception2).IsTrue()));
        }

        [Test]
        public void TryAllSucceedsWhenNoExceptionsAreThrown()
        {
            var block1Executed = Given("A value indicating whether an action has been executed", () => false);
            var block2Executed = Given("A value indicating whether another action has been executed", () => false);

            When("Two blocks are executed", () => Try.All(
                () => block1Executed = true,
                () => block2Executed = true
            ));

            Then("The value indicating the first action has been executed was modified",
                () => Verify.That(block1Executed.IsTrue()));
            Then("The value indicating the second action has been executed was modified",
                () => Verify.That(block2Executed.IsTrue()));
        }
        [Test]
        public void TryAllWithItemsTriesAllBlocksWhenExceptionsAreThrown()
        {
            var exception1 = Given("An exception", () => new TestException("1"));
            var exception2 = Given("Another exception", () => new TestException("2"));
            var instance1 = Given("An object that throws the first exception", () =>
            {
                var item = Substitute.For<IDisposable>();
                item.When(x => x.Dispose()).Do(_ => throw exception1);
                return item;
            });
            var instance2 = Given("Another object that throws the second exception", () =>
            {
                var item = Substitute.For<IDisposable>();
                item.When(x => x.Dispose()).Do(_ => throw exception2);
                return item;
            });

            var action = When("The exceptions are thrown from different items",
                () => (Action)(() => Try.All((IDisposable d) => d.Dispose(), instance1, instance2)));

            var aggregateException = Then("An aggregate exception is thrown",
                () => TestUtils.TestForError<AggregateException>(action));
            Then("Exactly two exceptions were thrown",
                () => Verify.That(aggregateException.InnerExceptions.Count.IsEqualTo(2)));
            Then("The first exception was caught",
                () => Verify.That(aggregateException.InnerExceptions.Contains(exception1).IsTrue()));
            Then("The second exception was caught",
                () => Verify.That(aggregateException.InnerExceptions.Contains(exception2).IsTrue()));
        }

        [Test]
        public void TryAllWithItemsSucceedsWhenNoExceptionsAreThrown()
        {
            var block1Executed = Given("A value indicating whether an action has been executed", () => false);
            var block2Executed = Given("A value indicating whether another action has been executed", () => false);

            var instance1 = Given("An object that executes the first action", () =>
            {
                var item = Substitute.For<IDisposable>();
                item.When(x => x.Dispose()).Do(_ => block1Executed = true);
                return item;
            });
            var instance2 = Given("Another object that executes the second action", () =>
            {
                var item = Substitute.For<IDisposable>();
                item.When(x => x.Dispose()).Do(_ => block2Executed = true);
                return item;
            });

            When("The exceptions are thrown from different items",
                () => Try.All((IDisposable d) => d.Dispose(), instance1, instance2));

            Then("The value indicating the first action has been executed was modified",
                () => Verify.That(block1Executed.IsTrue()));
            Then("The value indicating the second action has been executed was modified",
                () => Verify.That(block2Executed.IsTrue()));
        }
    }
}