// -----------------------------------------------------------------------------
//  <copyright file="Unit.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;

namespace Phx.Lang
{
    [Serializable]
    /// <summary>
    ///     A type that represents no type and holds no information.
    /// </summary>
    public sealed class Unit
    {
        /// <summary>
        ///     The <see cref="Unit"/> instance.
        /// </summary>
        public static Unit UNIT = new Unit();

        private Unit() { }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "[Unit]";
        }
    }
}