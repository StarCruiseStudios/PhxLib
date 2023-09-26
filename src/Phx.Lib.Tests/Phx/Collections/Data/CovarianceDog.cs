// -----------------------------------------------------------------------------
//  <copyright file="CovarianceDog.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections.Data {
    /// <summary>
    ///     A test class used to test covariance.
    /// </summary>
    public class CovarianceDog : CovarianceAnimal {
        public CovarianceDog(int value) : base(value) { }

        public override bool Equals(object? obj) {
            return obj is CovarianceDog dog &&
                    value == dog.value;
        }
        public override int GetHashCode() {
            return System.HashCode.Combine(value);
        }
    }
}