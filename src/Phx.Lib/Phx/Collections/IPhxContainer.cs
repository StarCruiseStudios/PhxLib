// -----------------------------------------------------------------------------
//  <copyright file="IPhxContainer.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {

    /// <summary> Represents a class that contains multiple elements. </summary>
    public interface IPhxContainer {
        /// <summary> Gets the number of elements contained in the <see cref="IPhxContainer" />. </summary>
        public int Count { get; }
    }

    /// <summary>
    ///     Contains extension methods applied to all implementations of <see cref="IPhxContainer" />
    ///     .
    /// </summary>
    public static class IPhxContainerExtensions {
        /// <summary> Gets a value indicating whether the <see cref="IPhxContainer" /> is empty. </summary>
        /// <param name="container"> The container to perform the operation on. </param>
        /// <returns> <c> true </c> if the <see cref="IPhxContainer" /> is empty, otherwise <c> false </c>. </returns>
        public static bool IsEmpty(this IPhxContainer container) {
            return container.Count == 0;
        }

        /// <summary> Gets a value indicating whether the <see cref="IPhxContainer" /> is not empty. </summary>
        /// <param name="container"> The container to perform the operation on. </param>
        /// <returns>
        ///     <c> true </c> if the <see cref="IPhxContainer" /> contains at least one item, otherwise
        ///     <c> false </c>.
        /// </returns>
        public static bool IsNotEmpty(this IPhxContainer container) {
            return container.Count != 0;
        }

        /// <summary> Gets a value indicating whether the <see cref="IPhxContainer" /> is empty or is null. </summary>
        /// <param name="container"> The container to perform the operation on. </param>
        /// <returns>
        ///     <c> true </c> if the <see cref="IPhxContainer" /> is empty or null, otherwise
        ///     <c> false </c>.
        /// </returns>
        public static bool IsNullOrEmpty(this IPhxContainer? container) {
            return container == null || container.Count == 0;
        }
    }
}
