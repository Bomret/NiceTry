using System;

namespace NiceTry.Combinators
{
    public static class OnFailureExt
    {
        public static void OnFailure<T>(this Try<T> @try, Action<Exception> onFailure)
        {
            if (@try.IsFailure) onFailure(@try.Error);
        }
    }
}