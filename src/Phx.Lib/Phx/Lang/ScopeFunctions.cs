// -----------------------------------------------------------------------------
// <copyright file="ScopeFunctions.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {

    public static class ScopeFunctions {
        public static T Also<T>(this T obj, Action<T> block) {
            block(obj);
            return obj;
        }

        public static R Let<T, R>(this T obj, Func<T, R> block) {
            return block(obj);
        }

        public static T? TakeIf<T>(this T obj, Func<T, bool> condition) {
            return condition(obj)
                    ? obj
                    : default(T?);
        }
        
        public static T? TakeUnless<T>(this T obj, Func<T, bool> condition) {
            return condition(obj)
                    ? default(T?)
                    : obj;
        }
        
        public static IOptional<T> If<T>(this T? value, Func<T?, bool> condition) {
            return condition(value)
                    ? Optional.OfNullable(value)
                    : Optional<T>.EMPTY;
        }
        
        public static IfScope<T> If<T>(bool condition, Func<T> block) {
            return new IfScope<T>(block, condition);
        }
        
        public static WhenScope<T, R> When<T, R>(this T input, Func<T, bool> condition, Func<R> block) {
            return new WhenScope<T, R>(input, condition, block);
        }
    }
}
