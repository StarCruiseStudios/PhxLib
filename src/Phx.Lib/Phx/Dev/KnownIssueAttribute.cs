// -----------------------------------------------------------------------------
//  <copyright file="KnownIssueAttribute.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Dev {
    using System;
    using Phx.Lang;

    /// <summary>
    ///     An attribute used to indicate that there is a known issue with the behavior or definition
    ///     of a code element.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class KnownIssueAttribute : Attribute {
        /// <summary> Initializes a new instance of the <see cref="KnownIssueAttribute" /> class. </summary>
        /// <param name="description"> A description of the issue. </param>
        /// <param name="workaround">
        ///     An optional description of the workaround that is implemented or that can
        ///     be used to avoid the issue.
        /// </param>
        /// <param name="link"> An optional link or ID containing more details about the issue. </param>
        public KnownIssueAttribute(
                string description,
                string workaround = StringUtils.EmptyString,
                string link = StringUtils.EmptyString) { }
    }
}
