// -----------------------------------------------------------------------------
// <copyright file="WeakReferenceExtensions.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    public static class WeakReferenceExtensions {
        public static void IfPresent<T>(this WeakReference<T> reference, Action<T> action) where T : class {
            if (reference.TryGetTarget(out var target)) {
                action(target);
            }
        }

        public static T OrElse<T>(this WeakReference<T> reference, Func<T> getDefault) where T : class {
            return reference.TryGetTarget(out var target)
                    ? target
                    : getDefault();
        }
        
        public static T? OrNull<T>(this WeakReference<T>reference) where T : class {
            return reference.TryGetTarget(out var target) ? target : null;
        }
    }
}
