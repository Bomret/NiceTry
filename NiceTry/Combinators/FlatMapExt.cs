using System;

namespace NiceTry.Combinators
{
    public static class FlatMapExt
    {
        public static Try<B> FlatMap<A, B>(this Try<A> @try, Func<A, Try<B>> f)
        {
            if (@try.IsFailure) return new Failure<B>(@try.Error);

            try
            {
                var result = f(@try.Value);
                return result;
            }
            catch (Exception err)
            {
                return new Failure<B>(err);
            }
        }
    }
}