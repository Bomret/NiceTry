namespace NiceTry.Combinators
{
    public static class GetOrDefaultExt{
        public static T GetOrDefault<T>(this Try<T> @try)
        {
            return @try.IsSuccess ? @try.Value : default(T);
        }
    }
}