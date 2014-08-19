using System;

namespace NiceTry.Combinators
{
    public static class RecoverWithExt
    {
        public static Try<T> RecoverWith<T>(this Try<T> @try, Func<Exception, Try<T>> f)
        {
            if (@try.IsSuccess) return new Success<T>(@try.Value);

            try
            {
                var result = f(@try.Error);
                return result;
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}