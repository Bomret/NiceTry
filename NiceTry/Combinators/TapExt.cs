using System;

namespace NiceTry.Combinators
{
    public static class TapExt
    {
        public static Try<T> Tap<T>(this Try<T> @try, Action<T> action)
        {
            if (@try.IsFailure) return new Failure<T>(@try.Error);

            try
            {
                action(@try.Value);

                return new Success<T>(@try.Value);
            }
            catch (Exception err)
            {
                return new Failure<T>(err);
            }
        }
    }
}