// -----------------------------------------------------------------------------
//  <copyright file="ToDoTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Dev {
    using System;
    using NUnit.Framework;
    using Phx.Test;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class ToDoTests : LoggingTestClass {
        [Test]
        public void NotImplementedYetCanBeUsedAsAValue() {
            Then("ToDo.NotImplementedYet can be used in an assignment expression.",
                    () => {
                        TestUtils.TestForError<NotImplementedException>(() => {
                            int x = ToDo.NotImplementedYet<int>("This is a test.");
                        });
                    });
        }
        
        [Test]
        public void NotImplementedYetCanBeUsedOnItsOwn() {
            Then("ToDo.NotImplementedYet can be used as a standalone expression.",
                    () => {
                        TestUtils.TestForError<NotImplementedException>(() => {
                            ToDo.NotImplementedYet("This is a test.");
                        });
                    });
        }
        
        [Test]
        public void NotSupportedYetCanBeUsedAsAValue() {
            Then("ToDo.NotSupportedYet can be used in an assignment expression.",
                    () => {
                        TestUtils.TestForError<InvalidOperationException>(() => {
                            int x = ToDo.NotSupportedYet<int>("This is a test.");
                        });
                    });
        }
        
        [Test]
        public void NotSupportedYetCanBeUsedOnItsOwn() {
            Then("ToDo.NotSupportedYet can be used as a standalone expression.",
                    () => {
                        TestUtils.TestForError<InvalidOperationException>(() => {
                            ToDo.NotSupportedYet("This is a test.");
                        });
                    });
        }
    }
}
