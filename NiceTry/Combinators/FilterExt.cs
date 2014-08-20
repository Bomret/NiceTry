using System;

namespace NiceTry.Combinators
{
    public static class FilterExt
    {
        public static Try<T> Filter<T>(this Try<T> @try, Func<T, bool> predicate)
        {
            if (@try.IsFailure) return new Failure<T>(@try.Error);

            try
            {
                return predicate(@try.Value)
                    ? @try
                    : new Failure<T>(new ArgumentException("The given predicate does not hold for this Try."));
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}