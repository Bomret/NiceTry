using System;

namespace NiceTry {
    public static class Applicators {
        public static void Match(this ITry t, Action whenSuccess, Action<Exception> whenFailure) {
            if (t.IsSuccess)
                whenSuccess();
            else
                whenFailure(t.Error);
        }

        public static void Match<T>(this ITry<T> t, Action<T> whenSuccess, Action<Exception> whenFailure) {
            if (t.IsSuccess)
                whenSuccess(t.Value);
            else
                whenFailure(t.Error);
        }

        public static void WhenComplete(this ITry t, Action<ITry> whenComplete) {
            whenComplete(t);
        }

        public static void WhenComplete<T>(this ITry<T> t, Action<ITry<T>> whenComplete) {
            whenComplete(t);
        }

        public static void WhenSuccess(this ITry t, Action runWhenSuccess) {
            if (t.IsSuccess)
                runWhenSuccess();
        }

        public static void WhenSuccess<T>(this ITry<T> t, Action<T> whenSuccess) {
            if (t.IsSuccess)
                whenSuccess(t.Value);
        }

        public static void WhenFailure(this ITry t, Action<Exception> whenFailure) {
            if (t.IsFailure)
                whenFailure(t.Error);
        }

        public static void WhenFailure<T>(this ITry<T> t, Action<Exception> whenFailure) {
            if (t.IsFailure)
                whenFailure(t.Error);
        }

        public static T Get<T>(this ITry<T> t) {
            if (t.IsFailure)
                throw t.Error;

            return t.Value;
        }

        public static T GetOrElse<T>(this ITry<T> t, T elseValue) {
            return t.IsFailure
                ? elseValue
                : t.Value;
        }
    }
}