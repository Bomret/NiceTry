namespace NiceTry.Combinators
{
    public static class GetOrElseExt
    {
        public static T GetOrElse<T>(this Try<T> @try, T elseValue)
        {
            return @try.IsSuccess ? @try.Value : elseValue;
        }
    }
}