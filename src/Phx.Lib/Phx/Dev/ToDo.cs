// -----------------------------------------------------------------------------
//  <copyright file="ToDo.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Dev {
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Phx.Lang;

    /// <summary> Provides methods for tagging code with development behavior. </summary>
    public static class ToDo {
        /// <summary>
        ///     Inidicates the code needs to be implemented, and throws a
        ///     <see cref="NotImplementedException" /> with the provided justification.
        /// </summary>
        /// <typeparam name="T"> The type that is expected to be returned when this code is implemented. </typeparam>
        /// <param name="reason"> The reason this code is not implemented. </param>
        /// <param name="link"> An optional link or ID containing more details about the issue. </param>
        /// <exception cref="NotImplementedException"> throw when this method is invoked. </exception>
        [SuppressMessage("General",
                "RCS1079",
                Justification = "This method is implemented, the purpose is to throw NotImplementedException.")]
        // [DoesNotReturn]
        public static T NotImplementedYet<T>(string reason, string link = StringUtils.EmptyString) {
            throw new NotImplementedException($"{reason}: {link}");
        }

        /// <summary>
        ///     Inidicates the code needs to be implemented, and throws a
        ///     <see cref="NotImplementedException" /> with the provided justification.
        /// </summary>
        /// <param name="reason"> The reason this code is not implemented. </param>
        /// <param name="link"> An optional link or ID containing more details about the issue. </param>
        /// <exception cref="NotImplementedException"> throw when this method is invoked. </exception>
        [SuppressMessage("General",
                "RCS1079",
                Justification = "This method is implemented, the purpose is to throw NotImplementedException.")]
        // [DoesNotReturn]
        public static Unit NotImplementedYet(string reason, string link = StringUtils.EmptyString) {
            throw new NotImplementedException($"{reason}: {link}");
        }

        /// <summary>
        ///     Inidicates the code path is not currently supported, and throws a
        ///     <see cref="InvalidOperationException" /> with the provided justification.
        /// </summary>
        /// <typeparam name="T"> The type that is expected to be returned when this code is implemented. </typeparam>
        /// <param name="reason"> The reason this code is not supported. </param>
        /// <param name="link"> An optional link or ID containing more details about the issue. </param>
        /// <exception cref="InvalidOperationException"> throw when this method is invoked. </exception>
        // [DoesNotReturn]
        public static T NotSupportedYet<T>(string reason, string link = StringUtils.EmptyString) {
            throw new InvalidOperationException($"{reason}: {link}");
        }

        /// <summary>
        ///     Inidicates the code path is not currently supported, and throws a
        ///     <see cref="InvalidOperationException" /> with the provided justification.
        /// </summary>
        /// <param name="reason"> The reason this code is not supported. </param>
        /// <param name="link"> An optional link or ID containing more details about the issue. </param>
        /// <exception cref="InvalidOperationException"> throw when this method is invoked. </exception>
        // [DoesNotReturn]
        public static Unit NotSupportedYet(string reason, string link = StringUtils.EmptyString) {
            throw new InvalidOperationException($"{reason}: {link}");
        }
    }
}
