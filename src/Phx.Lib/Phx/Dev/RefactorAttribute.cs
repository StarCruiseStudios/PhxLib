// -----------------------------------------------------------------------------
//  <copyright file="RefactorAttribute.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Dev {
    using System;
    using Phx.Lang;

    /// <summary> An attribute used to indicate that a code element needs refactoring or redesign. </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class RefactorAttribute : Attribute {
        /// <summary> Initializes a new instance of the <see cref="RefactorAttribute" /> class. </summary>
        /// <param name="description"> A description of the refactoring that needs to be done. </param>
        /// <param name="link"> An optional link or ID containing more details about the refactoring. </param>
        public RefactorAttribute(string description, string link = StringUtils.EmptyString) { }
    }
}
