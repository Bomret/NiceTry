using System.Threading.Tasks;

namespace NiceTry.Async {
    public sealed class AsyncSuccess<T> : AsyncTry<T> {
        public AsyncSuccess(T value) {
            ITry<T> @try = new Success<T>(value);
            Worker = Task.FromResult(@try);
        }
    }
}