// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxMapCollectionsTestBase.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using Phx.Test;

namespace Phx.Collections {
    public abstract class AbstractPhxMapCollectionsTestBase : LoggingTestClass {
        public abstract T GetTestInstance<T>(IEnumerable<(string, string)> elements) where T : IPhxMap<string, string>;

        protected static IEnumerable<(string, string)> CreateElements(int numElements, int minValue = 0) {
            for (int i = 0; i < numElements; i++) {
                yield return ((minValue + i).ToString(), (minValue + i).ToString());
            }
        }
    }
}