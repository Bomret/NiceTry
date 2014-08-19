using System;

namespace NiceTry.Combinators
{
    public static class MatchExt
    {
        public static B Match<A, B>(this Try<A> @try, Func<A, B> onSuccess, Func<Exception, B> onFailure)
        {
            return @try.IsSuccess ? onSuccess(@try.Value) : onFailure(@try.Error);
        }

        public static void Match<T>(this Try<T> @try, Action<T> onSuccess, Action<Exception> onFailure)
        {
            if (@try.IsSuccess)
                onSuccess(@try.Value);
            else
                onFailure(@try.Error);
        }
    }
}