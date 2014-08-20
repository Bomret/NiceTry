using System;

namespace NiceTry.Combinators
{
    public static class FinallyExt
    {
        public static Try<T> Finally<T>(this Try<T> @try, Action action)
        {
            if (@try.IsFailure) return @try;

            try
            {
                action();

                return @try;
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}