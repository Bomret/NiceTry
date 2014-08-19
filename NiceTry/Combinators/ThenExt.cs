using System;

namespace NiceTry.Combinators
{
    public static class ThenExt
    {
        public static Try<B> Then<A, B>(this Try<A> @try, Func<Try<A>, B> f)
        {
            return @try.IsFailure
                ? new Failure<B>(@try.Error)
                : Try.To(() => f(@try));
        }
    }
}