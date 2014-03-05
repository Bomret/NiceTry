using System;

namespace NiceTry {
    public static class Applicators {
        public static B Match<A, B>(this Try<A> @try, Func<A, B> onSuccess, Func<Exception, B> onFailure) {
            return @try.IsSuccess ? onSuccess(@try.Value) : onFailure(@try.Error);
        }

        public static void Match<T>(this Try<T> @try, Action<T> onSuccess, Action<Exception> onFailure) {
            if (@try.IsSuccess)
                onSuccess(@try.Value);
            else
                onFailure(@try.Error);
        }

        public static void OnSuccess<T>(this Try<T> @try, Action<T> onSuccess) {
            if (@try.IsSuccess) onSuccess(@try.Value);
        }

        public static void OnFailure<T>(this Try<T> @try, Action<Exception> whenFailure) {
            if (@try.IsFailure) whenFailure(@try.Error);
        }

        public static T Get<T>(this Try<T> @try) {
            return @try.Match(value => value, err => { throw err; });
        }

        public static T GetOrElse<T>(this Try<T> @try, T elseValue) {
            return @try.Match(value => value, _ => elseValue);
        }

        public static T GetOrDefault<T>(this Try<T> @try) {
            return @try.Match(value => value, _ => default(T));
        }
    }
}