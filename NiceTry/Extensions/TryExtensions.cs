using System;

namespace NiceTry.Extensions {
    public static class TryExtensions {
        public static void WhenComplete(this ITry result, Action<ITry> runWhenComplete) {
            runWhenComplete(result);
        }

        public static void WhenComplete<TValue>(this ITry<TValue> result, Action<ITry<TValue>> runWhenComplete) {
            runWhenComplete(result);
        }

        public static void WhenSuccess(this ITry result, Action runWhenSuccess) {
            if (result.IsSuccess)
                runWhenSuccess();
        }

        public static void WhenSuccess<TValue>(this ITry<TValue> result, Action<TValue> runWhenSuccess) {
            if (result.IsSuccess)
                runWhenSuccess(result.Value);
        }

        public static void WhenFailure(this ITry result, Action<Exception> runWhenFailure) {
            if (result.IsFailure)
                runWhenFailure(result.Error);
        }

        public static void WhenFailure<TValue>(this ITry<TValue> result, Action<Exception> runWhenFailure) {
            if (result.IsFailure)
                runWhenFailure(result.Error);
        }

        public static TValue Get<TValue>(this ITry<TValue> t) {
            if (t.IsFailure)
                throw t.Error;

            return t.Value;
        }

        public static TValue GetOrElse<TValue>(this ITry<TValue> t, TValue elseValue) {
            return t.IsFailure
                       ? elseValue
                       : t.Value;
        }

        public static ITry OrElse(this ITry result,
                                  Action orElse) {
            return result.IsFailure
                       ? Try.To(orElse)
                       : result;
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  Func<TValue> orElse) {
            return result.IsFailure
                       ? Try.To(orElse)
                       : result;
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  TValue orElse) {
            return result.IsFailure
                       ? new Success<TValue>(orElse)
                       : result;
        }

        public static ITry<TNewValue> Map<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, TNewValue> func) {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : Try.To(() => func(t.Value));
        }

        public static ITry<TValue> Recover<TValue>(this ITry<TValue> t, Func<Exception, TValue> func) {
            return t.IsFailure
                       ? Try.To(() => func(t.Error))
                       : t;
        }

        public static ITry Recover(this ITry t, Action<Exception> recover) {
            return t.IsFailure
                       ? Try.To(() => recover(t.Error))
                       : t;
        }
    }
}