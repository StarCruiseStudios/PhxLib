// -----------------------------------------------------------------------------
//  <copyright file="PhxHashMapTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Phx.Test;
    using Phx.Validation;

    [TestFixture]
    public static class PhxHashMapTests {
        private static T ConstructTestInstance<T>(
                IEnumerable<(string, string)> elements
        ) where T : IPhxMap<string, string> {
            var map = new PhxHashMap<string, string>(elements);
            if (map is T m) {
                return m;
            }

            throw new InvalidOperationException($"Cannot convert to expected type {typeof(T)}.");
        }

        private static T ConstructTestInstance<T, U>(IEnumerable<U> elements)
                where T : IPhxContainer<IPhxKeyValuePair<U, U>> {
            var map = new PhxHashMap<U, U>();
            foreach (var element in elements) {
                map[element] = element;
            }

            if (map is T m) {
                return m;
            }

            throw new InvalidOperationException($"Cannot convert to expected type {typeof(T)}.");
        }

        [TestFixture]
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        [Parallelizable(ParallelScope.All)]
        public class ContainerTests : LoggingTestClass {
            public IPhxMap<string, string> GetTestInstance(IEnumerable<(string, string)> elements) {
                return ConstructTestInstance<IPhxMap<string, string>>(elements);
            }

            protected static IEnumerable<string> CreateElements(int numElements, int minValue = 0) {
                for (int i = 0; i < numElements; i++) {
                    yield return (minValue + i).ToString();
                }
            }
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(10)]
            public void CountReturnsExpectedValue(int numElements) {
                var elements = Given("A collection of elements", () => CreateElements(numElements));
                var container = Given("An collection constructed with those elements.",
                        () => GetTestInstance(elements.Select(e => (e, e))));

                var actual = When("The collection count is retrieved", () => container.Count);

                Then("The expected result is returned",
                        numElements,
                        (expected) => Verify.That(actual.IsEqualTo(expected)));
            }

            [TestCase(0, true)]
            [TestCase(1, false)]
            [TestCase(10, false)]
            public void IsEmptyReturnsExpectedValue(int numElements, bool shouldBeEmpty) {
                var elements = Given("A collection of elements", () => CreateElements(numElements));
                var container = Given("An collection constructed with those elements.",
                        () => GetTestInstance(elements.Select(e => (e, e))));

                var actual = When("The collection is checked for emptiness", () => container.IsEmpty());

                Then("The expected result is returned",
                        shouldBeEmpty,
                        (expected) => Verify.That(actual.IsEqualTo(expected)));
            }
        }

        [TestFixture]
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        [Parallelizable(ParallelScope.All)]
        public class MapTests : AbstractPhxMapTests {
            public override T GetTestInstance<T>(IEnumerable<(string, string)> elements) {
                return ConstructTestInstance<T>(elements);
            }
        }

        [TestFixture]
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        [Parallelizable(ParallelScope.All)]
        public class MutableMapTests : AbstractMutablePhxMapTests {
            public override T GetTestInstance<T>(IEnumerable<(string, string)> elements) {
                return ConstructTestInstance<T>(elements);
            }
        }
    }
}
