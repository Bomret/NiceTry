namespace NiceTry.Combinators
{
    public static class GetExt
    {
        public static T Get<T>(this Try<T> @try)
        {
            if (@try.IsSuccess)
                return @try.Value;

            throw @try.Error;
        }
    }
}