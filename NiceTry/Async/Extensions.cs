namespace NiceTry.Async
{
    public static class Extensions
    {
        public static TValue Get<TValue>(this IAsyncTry<TValue> result)
        {
            if (result.IsFailure)
                throw result.Error;

            return result.Value;
        }
    }
}