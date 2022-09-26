// -----------------------------------------------------------------------------
//  <copyright file="CodeTags.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary> Provides methods for tagging code with development behavior. </summary>
    public static class CodeTags {
        /// <summary>
        ///     Inidicates the code needs to be implemented, and throws a <see cref="NotImplementedException" /> with the
        ///     provided justification.
        /// </summary>
        /// <param name="reason"> The reason this code is not implemented. </param>
        /// <exception cref="NotImplementedException"> throw when this method is invoked. </exception>
        [SuppressMessage("General",
                "RCS1079",
                Justification = "This method is implemented, the purpose is to throw NotImplementedException.")]
        [DoesNotReturn]
        public static Exception ToDo(string reason) {
            throw new NotImplementedException(reason);
        }
    }
}
