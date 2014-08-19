using System;

namespace NiceTry.Combinators
{
    public static class UsingExt
    {
        public static Try<B> Using<A, B, TDisposable>(this Try<A> @try, Func<TDisposable> createDisposable,
            Func<TDisposable, A, B> useDisposable) where TDisposable : IDisposable
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : Try.Using(createDisposable, d => useDisposable(d, @try.Value));
        }

        public static Try<B> Using<A, B, TDisposable>(this Try<A> @try, Func<A, TDisposable> createDisposable,
            Func<TDisposable, B> useDisposable)
            where TDisposable : IDisposable
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : Try.Using(() => createDisposable(@try.Value), useDisposable);
        }
    }
}