using System;

namespace NiceTry.Combinators
{
    public static class ZipWithExt
    {
        public static Try<C> Zip<A, B, C>(this Try<A> tryA, Try<B> tryB, Func<A, B, Try<C>> f)
        {
            if (tryA.IsFailure) return new Failure<C>(tryA.Error);

            if (tryB.IsFailure) return new Failure<C>(tryB.Error);

            try
            {
                var result = f(tryA.Value, tryB.Value);
                return result;
            }
            catch (Exception err)
            {
                return new Failure<C>(err);
            }
        }
    }
}