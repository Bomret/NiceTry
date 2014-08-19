using System;

namespace NiceTry.Combinators
{
    public static class MapExt
    {
        public static Try<B> Map<A, B>(this Try<A> @try, Func<A, B> f)
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : Try.To(() => f(@try.Value));
        }
    }
}