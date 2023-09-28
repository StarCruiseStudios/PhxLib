// -----------------------------------------------------------------------------
//  <copyright file="CovarianceCat.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections.Data {
    using System;

    /// <summary> A test class used to test covariance. </summary>
    public class CovarianceCat : CovarianceAnimal {
        public CovarianceCat(int value) : base(value) { }

        public override bool Equals(object? obj) {
            return obj is CovarianceCat cat &&
                    value == cat.value;
        }
        public override int GetHashCode() {
            return HashCode.Combine(value);
        }
    }
}
