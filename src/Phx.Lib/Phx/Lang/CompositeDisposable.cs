// -----------------------------------------------------------------------------
// <copyright file="CompositeDisposable.cs" company="Star Cruise Studios LLC">
//     Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
//     Licensed under the Apache License, Version 2.0.
//     See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Lang {
    using System.Collections;

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
        
        
        public void Add(params IDisposable[] disposables) {
            this.disposables.AddRange(disposables);
        }
        
        public void Add(IEnumerable<IDisposable> disposables) {
            this.disposables.AddRange(disposables);
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
                foreach (var disposable in disposables) {
                    disposable.Dispose();
                }
                
                if (disposing) {
                    IsDisposed = true;
                }
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
