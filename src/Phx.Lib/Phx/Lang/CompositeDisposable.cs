// -----------------------------------------------------------------------------
// <copyright file="CompositeDisposable.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System.Collections;
    using Collections;

    public sealed class CompositeDisposable : ICheckedDisposable, IEnumerable<IDisposable> {
        public bool IsDisposed { get; private set; }

        private List<IDisposable> disposables;

        public CompositeDisposable() 
                : this(Enumerable.Empty<IDisposable>()) { }

        public CompositeDisposable(params IDisposable[] disposables)
                : this(disposables.AsEnumerable()) { }

        public CompositeDisposable(IEnumerable<IDisposable> disposables) {
            this.disposables = new(disposables);
        }

        public IDisposable Add(IDisposable disposable) {
            this.disposables.Add(disposable);
            return disposable;
        }

        
        public IEnumerable<IDisposable> AddRange(params IDisposable[] disposables) {
            this.disposables.AddRange(disposables);
            return disposables;
        }
        
        public IEnumerable<IDisposable> AddRange(IEnumerable<IDisposable> disposables) {
            this.disposables.AddRange(disposables);
            return disposables;
        }

        public void Clear() {
            Dispose(false);
            disposables.Clear();
            IsDisposed = false;
        }
        
        public void Dispose() {
            Dispose(true);
        }
        
        private void Dispose(bool disposing) {
            if (!IsDisposed) {
                // Dispose all disposables in reverse order to ensure proper cleanup.
                for (var i = disposables.Count - 1; i >= 0; i--) {
                    disposables[i].Dispose();
                }
            }
                
            if (disposing) {
                IsDisposed = true;
            }
        }
        
        public IEnumerator<IDisposable> GetEnumerator() {
            return disposables.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
