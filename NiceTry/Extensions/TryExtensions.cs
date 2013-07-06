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
    }
}