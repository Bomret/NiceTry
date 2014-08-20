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
                return predicate(@try.Value)
                    ? new Failure<T>(new ArgumentException("The given Try was rejected"))
                    : @try;
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}