using System;

namespace NiceTry.Combinators
{
    public static class UsingWithExt
    {
        public static Try<B> UsingWith<A, B, TDisposable>(this Try<A> @try, Func<TDisposable> createDisposable,
            Func<TDisposable, A, Try<B>> useDisposable) where TDisposable : IDisposable
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : Try.UsingWith(createDisposable, d => useDisposable(d, @try.Value));
        }

        public static Try<B> UsingWith<A, B, TDisposable>(this Try<A> @try, Func<A, TDisposable> createDisposable,
            Func<TDisposable, Try<B>> useDisposable)
            where TDisposable : IDisposable
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : Try.UsingWith(() => createDisposable(@try.Value), useDisposable);
        }
    }
}