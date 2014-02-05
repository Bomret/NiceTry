using System;

namespace NiceTry {
    public static class Applicators {
        public static void Match<T>(this Try<T> @try, Action<T> whenSuccess, Action<Exception> whenFailure) {
            if (@try.IsSuccess) whenSuccess(@try.Value);
            else whenFailure(@try.Error);
        }

        public static B Match<A, B>(this Try<A> @try, Func<A, B> onSuccess, Func<Exception, B> onFailure) {
            return @try.IsSuccess ? onSuccess(@try.Value) : onFailure(@try.Error);
        }

        public static void OnSuccess<T>(this Try<T> @try, Action<T> whenSuccess) {
            if (@try.IsSuccess) whenSuccess(@try.Value);
        }

        public static void OnFailure<T>(this Try<T> @try, Action<Exception> whenFailure) {
            if (@try.IsFailure) whenFailure(@try.Error);
        }

        public static T Get<T>(this Try<T> @try) {
            if (@try.IsFailure) throw @try.Error;

            return @try.Value;
        }

        public static T GetOrElse<T>(this Try<T> @try, T elseValue) {
            return @try.Match(value => value, error => elseValue);
        }

        public static T GetOrDefault<T>(this Try<T> @try) {
            return @try.Match(value => value, error => default(T));
        }
    }
}