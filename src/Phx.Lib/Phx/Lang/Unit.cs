// -----------------------------------------------------------------------------
//  <copyright file="Unit.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System;

    /// <summary> A type that represents no type and holds no information. </summary>
    /// <remarks>
    ///     <para>
    ///         This type is used similarly to the way that <c> void </c> is used as a return value of a
    ///         function. It represents a function that returns nothing, returns a value that should be
    ///         ignored, or will never return a value.
    ///     </para>
    ///     <para>
    ///         <c> void </c> should be used as a return value when possible, but <c> Unit </c> offers the
    ///         following benefits over <c> void </c> when needed:
    ///         <list type="bullet">
    ///             <item> <c> Unit </c> is a type and can be used as a type argument. </item>
    ///             <item>
    ///                 <c> Unit </c> has a single value and can be assigned, passed around and
    ///                 compared.
    ///             </item>
    ///             <item>
    ///                 It is self documenting and clearly communicates that a function will never
    ///                 return (such as a <c> fail() </c> assertion, or a main application loop).
    ///             </item>
    ///         </list>
    ///     </para>
    /// </remarks>
    [Serializable]
    public sealed class Unit {
        /// <summary> The <see cref="Unit" /> instance. </summary>
        public static Unit UNIT = new();

        private Unit() { }

        /// <inheritdoc />
        public override string ToString() {
            return "[Unit]";
        }
    }
}
