using System.Runtime.CompilerServices;

namespace NiceTry.Async {
    public static class AsyncTryExt {
        public static TaskAwaiter<ITry<T>> GetAwaiter<T>(this AsyncTry<T> @try) {
            return @try.Worker.GetAwaiter();
        }
    }
}