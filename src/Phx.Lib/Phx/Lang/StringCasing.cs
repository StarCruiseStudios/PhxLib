// -----------------------------------------------------------------------------
//  <copyright file="StringCasing.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    /// <summary> Enumerates the supported string casings. </summary>
    public enum StringCasing {
        /// <summary>
        ///     Only alphanumeric characters. Each word starts with an uppercase letter. String starts
        ///     with a lowercase letter.
        /// </summary>
        Camel,

        /// <summary>
        ///     Only alphanumeric characters and underscores. All letters uppercase. Words separated by a
        ///     single underscore. Leading underscores are ignored. String starts with a letter.
        /// </summary>
        Caps,

        /// <summary>
        ///     Only alphanumeric characters and hyphens. All letters lowercase. Words separated by a
        ///     single hyphen. String starts with a letter.
        /// </summary>
        Kebab,

        /// <summary>
        ///     Only alphanumeric characters. Each word starts with an uppercase letter. String starts
        ///     with an uppercase letter.
        /// </summary>
        Pascal,

        /// <summary>
        ///     Only alphanumeric characters and underscores. All letters lowercase. Words separated by a
        ///     single underscore. Leading underscores are ignored. String starts with a letter.
        /// </summary>
        Snake
    }
}
