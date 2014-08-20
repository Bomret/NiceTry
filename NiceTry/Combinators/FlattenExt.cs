namespace NiceTry.Combinators
{
    public static class FlattenExt
    {
        public static Try<T> Flatten<T>(this Try<Try<T>> @try)
        {
            return @try.IsFailure ? new Failure<T>(@try.Error) : @try.Value;
        }
    }
}