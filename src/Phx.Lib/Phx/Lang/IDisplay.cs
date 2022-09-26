// -----------------------------------------------------------------------------
//  <copyright file="IDisplay.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang
{
    /// <summary>
    ///     Defines a method used to retrieve the user facing display string representation of an instance.
    /// </summary>
    public interface IDisplay
    {
        /// <summary>
        ///     Returns a user facing display string representation of an instance.
        /// </summary>
        /// <returns> A string representing the instance that can be used for displaying to the end user. </returns>
        string ToDisplay();
    }
}