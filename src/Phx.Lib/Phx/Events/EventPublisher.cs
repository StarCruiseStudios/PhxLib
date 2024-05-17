// -----------------------------------------------------------------------------
// <copyright file="EventPublisher.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Events {
    using Phx.Lang;
    
    public class EventPublisher<TInput> {
        private event Action<TInput>? raiseEvent;
        
        public ICheckedDisposable Subscribe(Action<TInput> action) {
            raiseEvent += action;
            return new Subscription(this, action);
        }
        
        public void Unsubscribe(Action<TInput> action) {
            raiseEvent -= action;
        }

        public void Publish(TInput input) {
            raiseEvent?.Invoke(input);
        }
        
        private sealed class Subscription : ICheckedDisposable {
            public bool IsDisposed { get; private set; }

            private EventPublisher<TInput> publisher;
            private Action<TInput> action;
        
            public Subscription(
                    EventPublisher<TInput> publisher,
                    Action<TInput> action
            ) {
                this.publisher = publisher;
                this.action = action;
            }
        
            public void Dispose() {
                if (!IsDisposed) {
                    publisher.Unsubscribe(action);
                    IsDisposed = true;
                }
            }
        }
    }
    
    public class EventPublisher<TInput1, TInput2> {
        private event Action<TInput1, TInput2>? raiseEvent;
        
        public ICheckedDisposable Subscribe(Action<TInput1, TInput2> action) {
            raiseEvent += action;
            return new Subscription(this, action);
        }
        
        public void Unsubscribe(Action<TInput1, TInput2> action) {
            raiseEvent -= action;
        }

        public void Publish(TInput1 input1, TInput2 input2) {
            raiseEvent?.Invoke(input1, input2);
        }
        
        private sealed class Subscription : ICheckedDisposable {
            public bool IsDisposed { get; private set; }

            private EventPublisher<TInput1, TInput2> publisher;
            private Action<TInput1, TInput2> action;
        
            public Subscription(
                    EventPublisher<TInput1, TInput2> publisher, 
                    Action<TInput1, TInput2> action
            ) {
                this.publisher = publisher;
                this.action = action;
            }
        
            public void Dispose() {
                if (!IsDisposed) {
                    publisher.Unsubscribe(action);
                    IsDisposed = true;
                }
            }
        }
    }
    
    public class EventPublisher<TInput1, TInput2, TInput3> {
        private event Action<TInput1, TInput2, TInput3>? raiseEvent;
        
        public ICheckedDisposable Subscribe(Action<TInput1, TInput2, TInput3> action) {
            raiseEvent += action;
            return new Subscription(this, action);
        }
        
        public void Unsubscribe(Action<TInput1, TInput2, TInput3> action) {
            raiseEvent -= action;
        }

        public void Publish(TInput1 input1, TInput2 input2, TInput3 input3) {
            raiseEvent?.Invoke(input1, input2, input3);
        }
        
        private sealed class Subscription : ICheckedDisposable {
            public bool IsDisposed { get; private set; }

            private EventPublisher<TInput1, TInput2, TInput3> publisher;
            private Action<TInput1, TInput2, TInput3> action;
        
            public Subscription(
                    EventPublisher<TInput1, TInput2, TInput3> publisher, 
                    Action<TInput1, TInput2, TInput3> action
            ) {
                this.publisher = publisher;
                this.action = action;
            }
        
            public void Dispose() {
                if (!IsDisposed) {
                    publisher.Unsubscribe(action);
                    IsDisposed = true;
                }
            }
        }
    }
}
