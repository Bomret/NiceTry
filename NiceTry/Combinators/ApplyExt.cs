using System;

namespace NiceTry.Combinators
{
    public static class ApplyExt
    {
        public static Try<Unit> Apply<T>(this Try<T> @try, Action<T> action)
        {
            return @try.IsFailure
                ? new Failure<Unit>(@try.Error)
                : Try.To(() => action(@try.Value));
        }
    }
}