using System;

namespace NiceTry.Combinators
{
    public static class ThenWithExt
    {
        public static Try<B> ThenWith<A, B>(this Try<A> @try, Func<Try<A>, Try<B>> f)
        {
            if (@try.IsFailure) return new Failure<B>(@try.Error);

            try
            {
                var result = f(@try);

                return result;
            }
            catch (Exception err)
            {
                return new Failure<B>(err);
            }
        }
    }
}