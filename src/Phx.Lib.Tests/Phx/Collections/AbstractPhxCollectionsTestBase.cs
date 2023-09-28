// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxCollectionsTestBase.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;
    using Phx.Test;

    public abstract class AbstractPhxCollectionsTestBase : LoggingTestClass {
        public abstract T GetTestInstance<T, U>(IEnumerable<U> elements) where T : class, IPhxContainer<U>;

        protected static IEnumerable<string> CreateElements(int numElements, int minValue = 0) {
            for (int i = 0; i < numElements; i++) {
                yield return (minValue + i).ToString();
            }
        }
    }
}
