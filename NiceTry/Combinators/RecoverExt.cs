using System;

namespace NiceTry.Combinators
{
    public static class RecoverExt
    {
        public static Try<T> Recover<T>(this Try<T> @try, Func<Exception, T> f)
        {
            return @try.IsSuccess ? @try : Try.To(() => f(@try.Error));
        }
    }
}