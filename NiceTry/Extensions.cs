using System;

namespace NiceTry {
    public static class Extensions {
        public static void Match(this ITry result,
                                 Action whenSuccess,
                                 Action<Exception> whenFailure) {
            if (result.IsSuccess) {
                whenSuccess();
            } else {
                whenFailure(result.Error);
            }
        }

        public static void Match<TValue>(this ITry<TValue> result,
                                         Action<TValue> whenSuccess,
                                         Action<Exception> whenFailure) {
            if (result.IsSuccess) {
                whenSuccess(result.Value);
            } else {
                whenFailure(result.Error);
            }
        }

        public static void WhenComplete(this ITry result,
                                        Action<ITry> whenComplete) {
            whenComplete(result);
        }

        public static void WhenComplete<TValue>(this ITry<TValue> result,
                                                Action<ITry<TValue>> whenComplete) {
            whenComplete(result);
        }

        public static void WhenSuccess(this ITry result,
                                       Action runWhenSuccess) {
            if (result.IsSuccess) {
                runWhenSuccess();
            }
        }

        public static void WhenSuccess<TValue>(this ITry<TValue> result,
                                               Action<TValue> whenSuccess) {
            if (result.IsSuccess) {
                whenSuccess(result.Value);
            }
        }

        public static void WhenFailure(this ITry result,
                                       Action<Exception> whenFailure) {
            if (result.IsFailure) {
                whenFailure(result.Error);
            }
        }

        public static void WhenFailure<TValue>(this ITry<TValue> result,
                                               Action<Exception> whenFailure) {
            if (result.IsFailure) {
                whenFailure(result.Error);
            }
        }

        public static TValue Get<TValue>(this ITry<TValue> t) {
            if (t.IsFailure) {
                throw t.Error;
            }

            return t.Value;
        }

        public static TValue GetOrElse<TValue>(this ITry<TValue> t,
                                               TValue elseValue) {
            return t.IsFailure
                       ? elseValue
                       : t.Value;
        }
    }
}