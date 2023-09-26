// -----------------------------------------------------------------------------
//  <copyright file="ImmutablePhxListTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Phx.Collections {
    public static class ImmutablePhxListTests {
        private static T ConstructTestInstance<T, U>(IEnumerable<U> elements) where T : IPhxContainer {
            var list = new ImmutablePhxList<U>(elements);
            if (list is T l) {
                return l;
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
        public class CollectionTests : AbstractPhxCollectionTests {
            public override T GetTestInstance<T, U>(IEnumerable<U> elements) {
                return ConstructTestInstance<T, U>(elements);
            }
        }

        [TestFixture]
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        [Parallelizable(ParallelScope.All)]
        public class ListTests : AbstractPhxListTests {
            public override T GetTestInstance<T, U>(IEnumerable<U> elements) {
                return ConstructTestInstance<T, U>(elements);
            }
        }
    }
}