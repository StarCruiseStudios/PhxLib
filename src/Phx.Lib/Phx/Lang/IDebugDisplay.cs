// -----------------------------------------------------------------------------
//  <copyright file="IDebugDisplay.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    /// <summary> Defines a method used to retrieve the programmer facing debug display string representation of an instance. </summary>
    public interface IDebugDisplay {
        /// <summary>
        ///     The value to use inside of the <see cref="DebuggerDisplayAttribute" /> to invoke the
        ///     <see cref="IDebugDisplay.ToDebugDisplay" /> method.
        /// </summary>
        public const string DEBUGGER_DISPLAY_STRING = "{ToDebugDisplay(),nq}";

        /// <summary> Returns a programming facing debug display string representation of the instance. </summary>
        /// <returns> A string representing the instance that can be used for debuging and diagnostics. </returns>
        public string ToDebugDisplay();
    }
}
