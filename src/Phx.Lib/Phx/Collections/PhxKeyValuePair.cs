// -----------------------------------------------------------------------------
//  <copyright file="PhxKeyValuePair.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2023 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Collections {
    using System.Collections.Generic;
    using System.Text;
    using Phx.Debug;
    using Phx.Lang;

    public class PhxKeyValuePair<TKey, TValue> : IPhxKeyValuePair<TKey, TValue>, IDebugDisplay {
        public TKey Key { get; }

        public TValue Value { get; }

        public PhxKeyValuePair(TKey key, TValue value) {
            Key = key;
            Value = value;
        }

        public override bool Equals(object? obj) {
            return obj is IPhxKeyValuePair<TKey, TValue> pair &&
                    EqualityComparer<TKey>.Default.Equals(Key, pair.Key) &&
                    EqualityComparer<TValue>.Default.Equals(Value, pair.Value);
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = hash * 23 + (Key?.GetHashCode() ?? 0);
            hash = hash * 23 + (Value?.GetHashCode() ?? 0);

            return hash;
        }

        /// <inheritdoc />
        public string ToDebugDisplay() {
            StringBuilder builder = new StringBuilder(GetType().Name).Append(" [ ");
            builder.Append(Key.ToDebugDisplayString()).Append(", ");
            builder.Append(Value.ToDebugDisplayString());
            return builder.Append(" ]").ToString();
        }

        /// <inheritdoc />
        public override string ToString() {
            return ToDebugDisplay();
        }
    }
}
