using System;

namespace NiceTry.Combinators
{
    public static class RejectExt
    {
        public static Try<T> Reject<T>(this Try<T> @try, Func<T, bool> predicate)
        {
            if (@try.IsFailure) return new Failure<T>(@try.Error);

            try
            {
                var isRejected = predicate(@try.Value);
                if (isRejected)
                    return new Failure<T>(new ArgumentException("The given Try was rejected"));

                return new Success<T>(@try.Value);
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}