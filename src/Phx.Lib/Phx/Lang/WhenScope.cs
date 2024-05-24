// -----------------------------------------------------------------------------
// <copyright file="WhenScope.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    public class WhenScope<T, R> {
        private readonly Func<Func<T, bool>, Func<R>, WhenScope<T, R>> handleWhen;
        private Func<Func<R>, R> handleElse;
        
        private readonly T input;
        private readonly Func<R> caseBlock;
        
        internal WhenScope(T input, Func<T, bool> caseCondition, Func<R> block) {
            this.caseBlock = block;
            this.input = input;
            if (caseCondition(input)) {
                handleWhen = doReturnSelf;
                handleElse = doReturnValue;
            } else {
                handleWhen = doReturnWhen;
                handleElse = doReturnElseValue;
            }
        }
        
        public WhenScope<T, R> When(Func<T, bool> newCaseCondition, Func<R> newBlock) {
            return handleWhen(newCaseCondition, newBlock);
        }
        
        public R Else(Func<R> elseBlock) {
            return handleElse(elseBlock);
        }

        private WhenScope<T, R> doReturnSelf(Func<T, bool> _c, Func<R> _b) {
            return this;
        }
        
        private R doReturnValue(Func<R> _) {
            return caseBlock();
        }

        private WhenScope<T, R> doReturnWhen(Func<T, bool> newCaseCondition, Func<R> newBlock) {
            return new WhenScope<T, R>(input, newCaseCondition, newBlock);
        }

        private static R doReturnElseValue(Func<R> elseBlock) {
            return elseBlock();
        }
    }
}
