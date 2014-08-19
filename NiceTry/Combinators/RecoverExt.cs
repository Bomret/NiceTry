using System;

namespace NiceTry.Combinators
{
    public static class RecoverExt
    {
        public static Try<T> Recover<T>(this Try<T> @try, Func<Exception, T> f)
        {
            return @try.IsSuccess ? new Success<T>(@try.Value) : Try.To(() => f(@try.Error));
        }
    }
}