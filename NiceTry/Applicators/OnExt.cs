using System;

namespace NiceTry.Applicators
{
    public static class OnExt
    {
        public static void OnSuccess<T>(this Try<T> @try, Action<T> onSuccess)
        {
            if (@try.IsSuccess) onSuccess(@try.Value);
        }

        public static void OnFailure<T>(this Try<T> @try, Action<Exception> onFailure)
        {
            if (@try.IsFailure) onFailure(@try.Error);
        }
    }
}