using System;

namespace NiceTry.Combinators
{
    public static class TransformWithExt
    {
        public static Try<B> Transform<A, B>(this Try<A> @try, Func<A, Try<B>> onSuccess,
            Func<Exception, Try<B>> onFailure)
        {
            try
            {
                return @try.IsSuccess ? onSuccess(@try.Value) : onFailure(@try.Error);
            }
            catch (Exception err)
            {
                return new Failure<B>(err);
            }
        }
    }
}