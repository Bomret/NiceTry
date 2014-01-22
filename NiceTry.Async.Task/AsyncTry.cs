using System.Threading.Tasks;

namespace NiceTry.Async {
    public abstract class AsyncTry<T> {
        internal Task<ITry<T>> Worker { get; set; }
    }
}