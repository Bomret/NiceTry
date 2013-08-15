using System;
using System.Threading.Tasks;

namespace NiceTry.Async
{
    public static class Combinators
    {
        public static IAsyncTry<TValue> Asynchronize<TValue>(this ITry<TValue> syncTry)
        {
            var task = Task.Factory.StartNew(() => syncTry);
            return new AsyncTry<TValue>(task);
        }

        public static IAsyncTry<TNewValue> Map<TValue, TNewValue>(this IAsyncTry<TValue> a,
                                                                  Func<TValue, TNewValue> map)
        {
            return a.AndThen(t => t.Map(map));
        }

        public static IAsyncTry<TNewValue> FlatMap<TValue, TNewValue>(this IAsyncTry<TValue> a,
                                                                      Func<TValue, ITry<TNewValue>> map)
        {
            return a.AndThen(t => t.FlatMap(map));
        }

        public static IAsyncTry<TValue> Filter<TValue>(this IAsyncTry<TValue> result,
                                                       Func<TValue, bool> predicate)
        {
            return result.AndThen(t => t.Filter(predicate));
        }

        public static IAsyncTry<TValue> Recover<TValue>(this IAsyncTry<TValue> result, Func<Exception, TValue> func)
        {
            return result.AndThen(t => t.Recover(func));
        }

        public static IAsyncTry<TNewValue> Transform<TValue, TNewValue>(this IAsyncTry<TValue> result,
                                                                        Func<TValue, ITry<TNewValue>> whenSuccess,
                                                                        Func<Exception, ITry<TNewValue>> whenFailure)
        {
            return result.AndThen(t => t.Transform(whenSuccess, whenFailure));
        }

        public static IAsyncTry<TValue> OrElse<TValue>(this IAsyncTry<TValue> a,
                                                       Func<ITry<TValue>> orElse)
        {
            return a.AndThen(t => t.OrElse(orElse));
        }
    }
}