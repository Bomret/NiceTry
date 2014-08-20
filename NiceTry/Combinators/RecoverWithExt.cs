using System;

namespace NiceTry.Combinators
{
    public static class RecoverWithExt
    {
        public static Try<T> RecoverWith<T>(this Try<T> @try, Func<Exception, Try<T>> f)
        {
            if (@try.IsSuccess) return @try;

            try
            {
                return f(@try.Error);
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}