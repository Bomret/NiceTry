namespace NiceTry.Combinators
{
    public static class OrElseExt
    {
        public static Try<T> OrElse<T>(this Try<T> @try, T elseValue)
        {
            return @try.IsSuccess ? @try : new Success<T>(elseValue);
        }
    }
}