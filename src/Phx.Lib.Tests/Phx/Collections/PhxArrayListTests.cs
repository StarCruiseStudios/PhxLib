// -----------------------------------------------------------------------------
//  <copyright file="PhxArrayListTests.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public static class PhxArrayListTests {
        private static T ConstructTestInstance<T, U>(IEnumerable<U> elements) where T : IPhxContainer<U> {
            var list = new PhxArrayList<U>(elements);
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
        public class MutableCollectionTests : AbstractMutablePhxCollectionTests {
            protected override bool SupportsDuplicateItems => true;

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

        [TestFixture]
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        [Parallelizable(ParallelScope.All)]
        public class MutableListTests : AbstractMutablePhxListTests {
            public override T GetTestInstance<T, U>(IEnumerable<U> elements) {
                return ConstructTestInstance<T, U>(elements);
            }
        }
    }
}
