// -----------------------------------------------------------------------------
// <copyright file="IfScope.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    public class IfScope<T> {
        private readonly Func<T> thenBlock;

        private Func<bool, Func<T>, IfScope<T>> handleIf;
        private Func<Func<T>, T> handleElse;
        
        internal IfScope(Func<T> thenBlock, bool ifCondition) {
            this.thenBlock = thenBlock;
            if (ifCondition) {
                handleIf = this.doReturnSelf;
                handleElse = this.doReturnValue;
            } else {
                handleIf = doReturnElse;
                handleElse = doReturnElseValue;
            }
        }

        public IfScope<T> ElseIf(bool elseCondition, Func<T> elseBlock) {
            return handleIf(elseCondition, elseBlock);
        }

        public T Else(Func<T> elseBlock) {
            return handleElse(elseBlock);
        }

        private IfScope<T> doReturnSelf(bool _b, Func<T> _f) {
            return this;
        }
        
        private T doReturnValue(Func<T> _) {
            return thenBlock();
        }
        
        private static IfScope<T> doReturnElse(bool ifCondition, Func<T> thenBlock) {
            return new IfScope<T>(thenBlock, ifCondition);
        }
        
        private static T doReturnElseValue(Func<T> elseBlock) {
            return elseBlock();
        }
    }
}
