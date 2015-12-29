using System;

namespace NiceTry.Exceptions {
    public sealed class JoinFailedException : Exception {
        public JoinFailedException() {}
        public JoinFailedException(string message) : base(message) {}
        public JoinFailedException(string message, Exception inner) : base(message, inner) {}
    }
}