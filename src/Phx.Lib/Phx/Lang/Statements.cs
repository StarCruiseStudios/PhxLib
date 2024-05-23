// -----------------------------------------------------------------------------
// <copyright file="Statements.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    public struct IfScope<T> {
        private readonly bool ifCondition;
        private readonly Func<T> thenBlock;
        internal IfScope(Func<T> thenBlock, bool ifCondition) {
            this.thenBlock = thenBlock;
            this.ifCondition = ifCondition;
        }

        public IfScope<T> ElseIf(bool elseCondition, Func<T> elseBlock) {
            if (ifCondition) {
                return this;
            } else {
                return new IfScope<T>(elseBlock, elseCondition);
            }
        }

        public T Else(Func<T> elseBlock) {
            if (ifCondition) {
                return thenBlock();
            } else {
                return elseBlock();
            }
        }
    }
    
    public struct WhenScope<T, R> {
        private readonly T input;
        private readonly bool caseCondition;
        private readonly Func<R> block;
        
        internal WhenScope(T input, Func<T, bool> caseCondition, Func<R> block) {
            this.input = input;
            this.caseCondition = caseCondition(input);
            this.block = block;
        }
        
        public WhenScope<T, R> When(Func<T, bool> newCaseCondition, Func<R> newBlock) {
            if (caseCondition) {
                return this;
            } else {
                return new WhenScope<T, R>(input, newCaseCondition, newBlock);
            }
        }
        
        public R Else(Func<R> elseBlock) {
            if (caseCondition) {
                return block();
            } else {
                return elseBlock();
            }
        }
    }
}
