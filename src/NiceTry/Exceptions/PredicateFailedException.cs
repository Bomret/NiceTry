using System;

namespace NiceTry.Exceptions {
    public sealed class PredicateFailedException : Exception {
        public PredicateFailedException() {}
        public PredicateFailedException(string message) : base(message) {}
        public PredicateFailedException(string message, Exception inner) : base(message, inner) {}
    }
}