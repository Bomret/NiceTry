using System;
using System.Threading.Tasks;
using NiceTry.Async;

namespace NiceTry
{
    public static class Try
    {
        public static ITry To(Action work)
        {
            try
            {
                work();
                return new Success();
            }
            catch (Exception error)
            {
                return new Failure(error);
            }
        }

        public static ITry<TValue> To<TValue>(Func<TValue> work)
        {
            try
            {
                var result = work();
                return new Success<TValue>(result);
            }
            catch (Exception error)
            {
                return new Failure<TValue>(error);
            }
        }

        public static IAsyncTry<TValue> ToAsync<TValue>(Func<TValue> work)
        {
            var task = Task.Factory.StartNew(() => To(work));
            return new AsyncTry<TValue>(task);
        }
    }
}