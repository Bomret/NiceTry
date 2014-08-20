using System;

namespace NiceTry.Combinators
{
    public static class TapExt
    {
        public static Try<T> Tap<T>(this Try<T> @try, Action<T> action)
        {
            if (@try.IsFailure) return @try;

            try
            {
                action(@try.Value);

                return @try;
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}