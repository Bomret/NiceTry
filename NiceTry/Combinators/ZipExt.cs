using System;

namespace NiceTry.Combinators
{
    public static class ZipExt
    {
        public static Try<C> Zip<A, B, C>(this Try<A> tryA, Try<B> tryB, Func<A, B, C> f)
        {
            if (tryA.IsFailure) return new Failure<C>(tryA.Error);

            if (tryB.IsFailure) return new Failure<C>(tryB.Error);

            return Try.To(() => f(tryA.Value, tryB.Value));
        }
    }
}