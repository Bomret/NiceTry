using System;
using System.Reactive;

namespace NiceTry {
    public static class Combinators {
        public static Success<T> Succeed<T>(this ITry<T> @try, T value) {
            return value;
        }

        public static Failure<T> Fail<T>(this ITry<T> @try, Exception error) {
            return new Failure<T>(error);
        }

        public static ITry<B> Retry<A, B>(this ITry<A> @try, Func<A, B> f, int retryCount = 1) {
            return @try.FlatMap(v => NiceTry.Retry.To(() => f(v), retryCount));
        }

        public static ITry<B> Then<A, B>(this ITry<A> @try, Func<ITry<A>, ITry<B>> f) {
            return @try.FlatMap(_ => f(@try));
        }

        public static ITry<T> OrElse<T>(this ITry<T> @try, Func<T> createElseValue) {
            return @try.Recover(_ => createElseValue());
        }

        public static ITry<T> OrElse<T>(this ITry<T> @try, T elseValue) {
            return @try.Recover(_ => elseValue);
        }

        public static ITry<T> OrElseWith<T>(this ITry<T> @try, Func<ITry<T>> createElseTry) {
            return @try.RecoverWith(_ => createElseTry());
        }

        public static ITry<T> OrElseWith<T>(this ITry<T> @try, ITry<T> elseTry) {
            return @try.RecoverWith(_ => elseTry);
        }

        public static ITry<Unit> Apply<T>(this ITry<T> @try, Action<T> action) {
            return @try.FlatMap(a => Try.To(() => action(a)));
        }

        public static ITry<T> Tap<T>(this ITry<T> @try, Action<T> action) {
            @try.Apply(action);

            return @try;
        }

        public static ITry<T> Finally<T>(this ITry<T> @try, Action action) {
            Try.To(action);

            return @try;
        }

        public static ITry<B> Map<A, B>(this ITry<A> @try, Func<A, B> f) {
            return @try.FlatMap(v => Try.To(() => f(v)));
        }

        public static ITry<B> FlatMap<A, B>(this ITry<A> @try, Func<A, ITry<B>> f) {
            return @try.IsFailure ? new Failure<B>(@try.Error) : f(@try.Value);
        }

        public static ITry<C> Zip<A, B, C>(this ITry<A> tryA, ITry<B> tryB, Func<A, B, C> f) {
            return tryA.FlatMap(a => tryB.Map(b => f(a, b)));
        }

        public static ITry<T> Flatten<T>(this ITry<ITry<T>> @try) {
            return @try.FlatMap(_ => _);
        }

        public static ITry<B> Recover<A, B>(this ITry<A> @try, Func<Exception, B> f) where A : B {
            return @try.IsFailure ? Try.To(() => f(@try.Error)) : new Success<B>(@try.Value);
        }

        public static ITry<B> RecoverWith<A, B>(this ITry<A> @try, Func<Exception, ITry<B>> f) where A : B {
            return @try.IsFailure ? f(@try.Error) : new Success<B>(@try.Value);
        }

        public static ITry<B> Transform<A, B>(this ITry<A> @try, Func<A, ITry<B>> whenSuccess,
                                              Func<Exception, ITry<B>> whenFailure) {
            return @try.IsSuccess ? whenSuccess(@try.Value) : whenFailure(@try.Error);
        }

        public static ITry<T> Filter<T>(this ITry<T> @try, Func<T, bool> predicate) {
            return @try.FlatMap(v => predicate(v)
                ? @try
                : new Failure<T>(new ArgumentException("The given predicate does not hold for this Try.")));
        }

        public static ITry<T> Reject<T>(this ITry<T> @try, Func<T, bool> predicate) {
            return @try.Filter(v => !predicate(v));
        }

        public static ITry<B> Using<A, B, TDisposable>(this ITry<A> @try, Func<TDisposable> createDisposable,
                                                       Func<TDisposable, A, B> useDisposable)
            where TDisposable : IDisposable {
            return @try.Map(a => { using (var disposable = createDisposable()) return useDisposable(disposable, a); });
        }

        public static ITry<B> UsingWith<A, B, TDisposable>(this ITry<A> @try, Func<TDisposable> createDisposable,
                                                           Func<TDisposable, A, ITry<B>> useDisposable)
            where TDisposable : IDisposable {
            return
                @try.FlatMap(a => { using (var disposable = createDisposable()) return useDisposable(disposable, a); });
        }

        public static ITry<B> Using<A, B, TDisposable>(this ITry<A> @try, Func<A, TDisposable> createDisposable,
                                                       Func<TDisposable, B> useDisposable)
            where TDisposable : IDisposable {
            return @try.Map(a => { using (var disposable = createDisposable(a)) return useDisposable(disposable); });
        }

        public static ITry<B> UsingWith<A, B, TDisposable>(this ITry<A> @try, Func<A, TDisposable> createDisposable,
                                                           Func<TDisposable, ITry<B>> useDisposable)
            where TDisposable : IDisposable {
            return @try.FlatMap(a => { using (var disposable = createDisposable(a)) return useDisposable(disposable); });
        }
    }
}