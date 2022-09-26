// -----------------------------------------------------------------------------
//  <copyright file="ICheckedDisposable.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;

namespace Phx.Lang
{
    /// <summary>
    ///     Represents an IDisposable that can be checked for its disposed state.
    /// </summary>
    public interface ICheckedDisposable : IDisposable
    {
        /// <summary>
        ///     Gets a value that indicates whether the object is disposed.
        /// </summary>
        /// <value> <c>true</c> if the instance has been disposed, otherwise <c>false</c>. </value>
        public bool IsDisposed { get; }
    }
}