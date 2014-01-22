using System;
using System.Threading.Tasks;

namespace NiceTry.Async {
    public sealed class AsyncFailure<T> : AsyncTry<T> {
        public AsyncFailure(Exception error) {
            ITry<T> @try = new Failure<T>(error);
            Worker = Task.FromResult(@try);
        }
    }
}