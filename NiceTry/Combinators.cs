using System;
using System.Reactive;

namespace NiceTry {
    public static class Combinators {
        public static ITry<A> Succeed<A>(this ITry<A> t, A b) {
            return new Success<A>(b);
        }

        public static ITry<A> Fail<A>(this ITry<A> t, Exception error) {
            return new Failure<A>(error);
        }

        public static ITry<B> Retry<A, B>(this ITry<A> t, Func<A, B> f, int retryCount = 1) {
            return t.FlatMap(a => NiceTry.Retry.To(() => f(a), retryCount));
        }

        public static ITry<B> RetryWith<A, B>(this ITry<A> t, Func<A, ITry<B>> f, int retryCount = 1) {
            return t.FlatMap(a => NiceTry.Retry.To(() => f(a), retryCount).Flatten());
        }

        public static ITry<B> Then<A, B>(this ITry<A> ta, Func<ITry<A>, ITry<B>> f) {
            return ta.FlatMap(_ => f(ta));
        }

        public static ITry<B> OrElse<A, B>(this ITry<A> t, Func<ITry<B>> f) where A : B {
            return t.RecoverWith(_ => f());
        }

        public static ITry<B> OrElse<A, B>(this ITry<A> t, ITry<B> te) where A : B {
            return t.RecoverWith(_ => te);
        }

        public static ITry<Unit> Apply<A>(this ITry<A> t, Action<A> a) {
            return t.FlatMap(x => Try.To(() => a(x)));
        }

        public static ITry<A> Inspect<A>(this ITry<A> t, Action<ITry<A>> a) {
            Try.To(() => a(t));

            return t;
        }

        public static ITry<B> Map<A, B>(this ITry<A> ta, Func<A, B> f) {
            return ta.FlatMap(a => Try.To(() => f(a)));
        }

        public static ITry<B> FlatMap<A, B>(this ITry<A> ta, Func<A, ITry<B>> f) {
            return ta.IsFailure ? new Failure<B>(ta.Error) : f(ta.Value);
        }

        public static ITry<C> LiftMap<A, B, C>(this ITry<A> ta, ITry<B> tb, Func<A, B, C> f) {
            return ta.FlatMap(a => tb.Map(b => f(a, b)));
        }

        public static ITry<A> Flatten<A>(this ITry<ITry<A>> tt) {
            return tt.FlatMap(t => t);
        }

        public static ITry<B> Recover<A, B>(this ITry<A> t, Func<Exception, B> f) where A : B {
            return t.IsFailure ? Try.To(() => f(t.Error)) : new Success<B>(t.Value);
        }

        public static ITry<B> RecoverWith<A, B>(this ITry<A> t, Func<Exception, ITry<B>> f) where A : B {
            return t.IsFailure ? f(t.Error) : new Success<B>(t.Value);
        }

        public static ITry<B> Transform<A, B>(this ITry<A> ta, Func<A, ITry<B>> whenSuccess,
                                              Func<Exception, ITry<B>> whenFailure) {
            return ta.IsSuccess ? whenSuccess(ta.Value) : whenFailure(ta.Error);
        }

        public static ITry<A> Filter<A>(this ITry<A> t, Func<A, bool> p) {
            return t.FlatMap(x =>
                             p(x)
                                 ? t
                                 : new Failure<A>(
                                       new ArgumentException("The given predicate does not hold for this Try.")));
        }

        public static ITry<A> Reject<A>(this ITry<A> t, Func<A, bool> p) {
            return t.FlatMap(x =>
                             p(x)
                                 ? new Failure<A>(
                                       new ArgumentException("The given predicate does not hold for this Try."))
                                 : t);
        }
    }
}