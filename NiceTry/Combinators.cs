using System;
using System.Reactive;

namespace NiceTry {
    public static class Combinators {
        public static Try<B> Retry<A, B>(this Try<A> @try, Func<A, B> f, int retryCount = 1) {
            return @try.FlatMap(v => NiceTry.Retry.To(() => f(v), retryCount));
        }

        public static Try<B> Then<A, B>(this Try<A> @try, Func<Try<A>, B> f) {
            return @try.Map(_ => f(@try));
        }

        public static Try<B> ThenWith<A, B>(this Try<A> @try, Func<Try<A>, Try<B>> f) {
            return @try.FlatMap(_ => f(@try));
        }

        public static Try<T> OrElse<T>(this Try<T> @try, T elseValue) {
            return @try.Recover(_ => elseValue);
        }

        public static Try<T> OrElseWith<T>(this Try<T> @try, Try<T> elseTry) {
            return @try.RecoverWith(_ => elseTry);
        }

        public static Try<Unit> Apply<T>(this Try<T> @try, Action<T> action) {
            return @try.FlatMap(a => Try.To(() => action(a)));
        }

        public static Try<T> Tap<T>(this Try<T> @try, Action<T> action) {
            @try.Apply(action);

            return @try;
        }

        public static Try<T> Finally<T>(this Try<T> @try, Action action) {
            Try.To(action);

            return @try;
        }

        public static Try<B> Map<A, B>(this Try<A> @try, Func<A, B> f) {
            return @try.FlatMap(v => Try.To(() => f(v)));
        }

        public static Try<B> FlatMap<A, B>(this Try<A> @try, Func<A, Try<B>> f) {
            return @try.IsFailure ? Try.Failure(@try.Error) : f(@try.Value);
        }

        public static Try<C> Zip<A, B, C>(this Try<A> tryA, Try<B> tryB, Func<A, B, C> f) {
            return tryA.FlatMap(a => tryB.Map(b => f(a, b)));
        }

        public static Try<C> ZipWith<A, B, C>(this Try<A> tryA, Try<B> tryB, Func<A, B, Try<C>> f) {
            return tryA.FlatMap(a => tryB.FlatMap(b => f(a, b)));
        }

        public static Try<T> Flatten<T>(this Try<Try<T>> @try) {
            return @try.FlatMap(_ => _);
        }

        public static Try<T> Recover<T>(this Try<T> @try, Func<Exception, T> f) {
            return @try.IsFailure ? Try.To(() => f(@try.Error)) : @try;
        }

        public static Try<T> RecoverWith<T>(this Try<T> @try, Func<Exception, Try<T>> f) {
            return @try.IsFailure ? f(@try.Error) : @try;
        }

        public static Try<B> Transform<A, B>(this Try<A> @try, Func<A, B> whenSuccess,
                                             Func<Exception, B> whenFailure) {
            return Try.To(() => @try.IsSuccess ? whenSuccess(@try.Value) : whenFailure(@try.Error));
        }

        public static Try<B> TransformWith<A, B>(this Try<A> @try, Func<A, Try<B>> whenSuccess,
                                                 Func<Exception, Try<B>> whenFailure) {
            return @try.IsSuccess ? whenSuccess(@try.Value) : whenFailure(@try.Error);
        }

        public static Try<T> Filter<T>(this Try<T> @try, Func<T, bool> predicate) {
            return @try.FlatMap(v => predicate(v)
                ? @try
                : new Failure<T>(new ArgumentException("The given predicate does not hold for this Try.")));
        }

        public static Try<T> Reject<T>(this Try<T> @try, Func<T, bool> predicate) {
            return @try.Filter(v => !predicate(v));
        }

        public static Try<B> Using<A, B, TDisposable>(this Try<A> @try, Func<TDisposable> createDisposable,
                                                      Func<TDisposable, A, B> useDisposable)
            where TDisposable : IDisposable {
            return @try.Map(a => { using (var disposable = createDisposable()) return useDisposable(disposable, a); });
        }

        public static Try<B> UsingWith<A, B, TDisposable>(this Try<A> @try, Func<TDisposable> createDisposable,
                                                          Func<TDisposable, A, Try<B>> useDisposable)
            where TDisposable : IDisposable {
            return
                @try.FlatMap(a => { using (var disposable = createDisposable()) return useDisposable(disposable, a); });
        }

        public static Try<B> Using<A, B, TDisposable>(this Try<A> @try, Func<A, TDisposable> createDisposable,
                                                      Func<TDisposable, B> useDisposable)
            where TDisposable : IDisposable {
            return @try.Map(a => { using (var disposable = createDisposable(a)) return useDisposable(disposable); });
        }

        public static Try<B> UsingWith<A, B, TDisposable>(this Try<A> @try, Func<A, TDisposable> createDisposable,
                                                          Func<TDisposable, Try<B>> useDisposable)
            where TDisposable : IDisposable {
            return @try.FlatMap(a => { using (var disposable = createDisposable(a)) return useDisposable(disposable); });
        }
    }
}