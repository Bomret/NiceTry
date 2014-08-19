using System;

namespace NiceTry.Combinators
{
    public static class OnSuccessExt
    {
        public static void OnSuccess<T>(this Try<T> @try, Action<T> onSuccess)
        {
            if (@try.IsSuccess) onSuccess(@try.Value);
        }
    }
}