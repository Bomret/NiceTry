using System.Threading.Tasks;

namespace NiceTry.Async {
    public sealed class PendingAsyncTry<T> : AsyncTry<T> {
        public PendingAsyncTry(Task<ITry<T>> task) {
            Worker = task;
        }
    }
}