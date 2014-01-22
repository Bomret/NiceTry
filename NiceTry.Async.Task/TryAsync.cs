using System;
using System.Reactive;
using System.Threading.Tasks;

namespace NiceTry.Async {
    public static class TryAsync {
        public static AsyncTry<T> To<T>(Func<T> work) {
            var task = Task.Run(() => Try.To(work));

            return new PendingAsyncTry<T>(task);
        }

        public static AsyncTry<Unit> To(Action work) {
            var task = Task.Run(() => Try.To(work));

            return new PendingAsyncTry<Unit>(task);
        }

        public static AsyncTry<T> FromTry<T>(ITry<T> @try) {
            return @try.IsSuccess
                ? (AsyncTry<T>) new AsyncSuccess<T>(@try.Value)
                : new AsyncFailure<T>(@try.Error);
        }

        public static AsyncTry<T> FromTask<T>(Task<T> task) {
            var continuation = task.ContinueWith(t => t.IsCompleted
                ? (ITry<T>) new Success<T>(t.Result)
                : new Failure<T>(t.Exception));

            return new PendingAsyncTry<T>(continuation);
        }

        public static AsyncTry<Unit> FromTask(Task task) {
            var continuation = task.ContinueWith(t => t.IsCompleted
                ? (ITry<Unit>) new Success<Unit>(Unit.Default)
                : new Failure<Unit>(t.Exception));

            return new PendingAsyncTry<Unit>(continuation);
        }
    }
}