namespace NiceTry.Combinators
{
    public static class OrElseWithExt
    {
        public static Try<T> OrElseWith<T>(this Try<T> @try, Try<T> elseTry)
        {
            return @try.IsSuccess ? @try : elseTry;
        }
    }
}