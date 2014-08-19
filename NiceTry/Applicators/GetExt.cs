namespace NiceTry.Applicators
{
    public static class GetExt
    {
        public static T Get<T>(this Try<T> @try)
        {
            if (@try.IsSuccess)
                return @try.Value;

            throw @try.Error;
        }

        public static T GetOrElse<T>(this Try<T> @try, T elseValue)
        {
            return @try.IsSuccess ? @try.Value : elseValue;
        }

        public static T GetOrDefault<T>(this Try<T> @try)
        {
            return @try.IsSuccess ? @try.Value : default(T);
        }
    }
}