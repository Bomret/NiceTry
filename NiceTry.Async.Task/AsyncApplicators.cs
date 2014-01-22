using System;
using System.Reactive;

namespace NiceTry.Async {
    public static class AsyncApplicators {
        public static ITry<T> Synchronize<T>(this AsyncTry<T> @try) {
            return @try.Worker.Result;
        }

        public static AsyncTry<T> Asynchronize<T>(this ITry<T> @try) {
            return TryAsync.FromTry(@try);
        }

        public static AsyncTry<Unit> Apply<T>(this AsyncTry<T> @try, Action<T> action) {
            var task = @try.Worker.ContinueWith(t => t.Result.Apply(action));

            return new PendingAsyncTry<Unit>(task);
        }
    }
}