// -----------------------------------------------------------------------------
//  <copyright file="ImmutablePhxMapTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Phx.Collections {
    public static class ImmutablePhxMapTests {
        private static T ConstructTestInstance<T>(
            IEnumerable<(string, string)> elements
        ) where T : IPhxMap<string, string> {
            var map = new ImmutablePhxMap<string, string>(elements);
            if (map is T m) {
                return m;
            }
            throw new InvalidOperationException($"Cannot convert to expected type {typeof(T)}.");
        }

        private static T ConstructTestInstance<T, U>(IEnumerable<U> elements) where T : IPhxContainer {
            var valueMap = new PhxHashMap<U, U>();
            foreach (var element in elements) {
                valueMap[element] = element;
            }
            var map = new ImmutablePhxMap<U, U>(valueMap);

            if (map is T m) {
                return m;
            }
            throw new InvalidOperationException($"Cannot convert to expected type {typeof(T)}.");
        }

        [TestFixture]
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        [Parallelizable(ParallelScope.All)]
        public class ContainerTests : AbstractPhxContainerTests {
            public override T GetTestInstance<T, U>(IEnumerable<U> elements) {
                return ConstructTestInstance<T, U>(elements);
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
    }
}