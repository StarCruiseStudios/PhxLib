// -----------------------------------------------------------------------------
//  <copyright file="IDebugDisplay.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Debug {
    public static class DebugDisplay {
        /// <summary>
        ///     The value to use inside of the <see cref="System.Diagnostics.DebuggerDisplayAttribute" />
        ///     to invoke the <see cref="IDebugDisplay.ToDebugDisplay" /> method.
        /// </summary>
        /// <example>
        ///     <code>
        ///     using System.Diagnostics;
        ///     [DebuggerDisplay(IDebugDisplay.DEBUGGER_DISPLAY_STRING)]
        ///     public class MyClass : IDebugDisplay {
        ///         public string ToDebugDisplay() {
        ///             return "MyClass";
        ///         }
        ///     }
        /// </code>
        /// </example>
        public const string DEBUGGER_DISPLAY_STRING = "{ToDebugDisplay(),nq}";
    }

    /// <summary>
    ///     Defines a method used to retrieve the programmer facing debug display string
    ///     representation of an instance.
    /// </summary>
    public interface IDebugDisplay {
        /// <summary> Returns a programming facing debug display string representation of the instance. </summary>
        /// <returns> A string representing the instance that can be used for debuging and diagnostics. </returns>
        public string ToDebugDisplay();
    }
}
