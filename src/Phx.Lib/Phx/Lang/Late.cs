// -----------------------------------------------------------------------------
//  <copyright file="Late.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    /// <summary> Provides a named wrapper around a null-forgiving null value used to lazily initialize non-nullable values. </summary>
    public static class Late {
        /// <summary> Provides a null-forgiving null value of the given type. </summary>
        /// <typeparam name="T"> The type of value that will be lateinit-ed. </typeparam>
        /// <returns> A late init value. </returns>
        public static T LateInit<T>() where T : class {
            return null!;
        }
    }
}
