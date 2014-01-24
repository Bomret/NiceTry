using System;

namespace NiceTry {
    public static class Applicators {
        public static void Match<T>(this ITry<T> @try, Action<T> whenSuccess, Action<Exception> whenFailure) {
            if (@try.IsSuccess) whenSuccess(@try.Value);
            else whenFailure(@try.Error);
        }

        public static B Match<A, B>(this ITry<A> @try, Func<A, B> onSuccess, Func<Exception, B> onFailure) {
            return @try.IsSuccess ? onSuccess(@try.Value) : onFailure(@try.Error);
        }

        public static void OnSuccess<T>(this ITry<T> @try, Action<T> whenSuccess) {
            if (@try.IsSuccess) whenSuccess(@try.Value);
        }

        public static void OnFailure<T>(this ITry<T> @try, Action<Exception> whenFailure) {
            if (@try.IsFailure) whenFailure(@try.Error);
        }

        public static T Get<T>(this ITry<T> @try) {
            if (@try.IsFailure) throw @try.Error;

            return @try.Value;
        }

        public static B GetOrElse<A, B>(this ITry<A> @try, B elseValue) where A : B {
            return @try.IsFailure ? elseValue : @try.Value;
        }

        public static T GetOrDefault<T>(this ITry<T> @try) {
            return @try.IsSuccess ? @try.Value : default (T);
        }
    }
}