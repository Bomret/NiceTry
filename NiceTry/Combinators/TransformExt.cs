using System;

namespace NiceTry.Combinators
{
    public static class TransformExt
    {
        public static Try<B> Transform<A, B>(this Try<A> @try, Func<A, B> onSuccess,
            Func<Exception, B> onFailure)
        {
            try
            {
                if (@try.IsSuccess)
                {
                    var success = onSuccess(@try.Value);
                    return new Success<B>(success);
                }

                var failure = onFailure(@try.Error);
                return new Success<B>(failure);
            }
            catch (Exception err)
            {
                return new Failure<B>(err);
            }
        }
    }
}