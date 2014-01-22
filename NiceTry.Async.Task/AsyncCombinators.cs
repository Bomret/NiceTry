using System;
using System.Linq;
using System.Threading.Tasks;

namespace NiceTry.Async {
    public static class AsyncCombinators {
        public static AsyncTry<B> Map<A, B>(this AsyncTry<A> @try, Func<A, B> f) {
            var task = @try.Worker.ContinueWith(t => t.Result.Map(f));

            return new PendingAsyncTry<B>(task);
        }

        public static AsyncTry<B> FlatMap<A, B>(this AsyncTry<A> @try, Func<A, AsyncTry<B>> f) {
            var ta = Task.WhenAll(@try.Worker);
            var r = ta.Result.First();

            return r.Map(f).Get();
        }
    }
}