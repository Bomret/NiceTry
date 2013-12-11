using System;

namespace NiceTry {
    public static class Applicators {
        public static void Match<A>(this ITry<A> t, Action<A> whenSuccess, Action<Exception> whenFailure) {
            if (t.IsSuccess) whenSuccess(t.Value);
            else whenFailure(t.Error);
        }

        public static void WhenComplete<A>(this ITry<A> t, Action<ITry<A>> whenComplete) {
            whenComplete(t);
        }

        public static void WhenSuccess<A>(this ITry<A> t, Action<A> whenSuccess) {
            if (t.IsSuccess) whenSuccess(t.Value);
        }

        public static void WhenFailure<A>(this ITry<A> t, Action<Exception> whenFailure) {
            if (t.IsFailure) whenFailure(t.Error);
        }

        public static A Get<A>(this ITry<A> t) {
            if (t.IsFailure) throw t.Error;
            return t.Value;
        }

        public static B GetOrElse<A, B>(this ITry<A> t, B b) where A : B {
            return t.IsFailure ? b : t.Value;
        }
    }
}