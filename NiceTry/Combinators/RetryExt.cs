using System;

namespace NiceTry.Combinators
{
    public static class RetryExt
    {
        public static Try<B> Retry<A, B>(this Try<A> @try, Func<A, B> f, int retryCount = 1)
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : NiceTry.Retry.To(() => f(@try.Value), retryCount);
        }
    }
}