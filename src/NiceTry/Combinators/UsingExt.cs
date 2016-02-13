using System;
using TheVoid;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to handle instances of <see cref="IDisposable"/>.
    /// </summary>
    public static class UsingExt {
        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> or a <see cref="Failure{T}" />, depending on the outcome of the
        ///     operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        public static Try<Unit> Using<Disposable, T>(
            this Try<T> @try,
            Func<Disposable> createDisposable,
            Action<Disposable, T> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(@try, createDisposable, (d, x) => {
                useDisposable(d, x);
                return Ok(Unit.Default);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> containing the result or a <see cref="Failure{T}" />, depending
        ///     on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        public static Try<B> Using<Disposable, A, B>(
            this Try<A> @try,
            Func<Disposable> createDisposable,
            Func<Disposable, A, B> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(@try, createDisposable, (d, a) => {
                var b = useDisposable(d, a);
                return Ok(b);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> containing the result or a <see cref="Failure{T}" />, depending
        ///     on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        public static Try<B> Using<Disposable, A, B>(
            this Try<A> @try,
            Func<A, Disposable> createDisposable,
            Func<Disposable, B> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(@try, createDisposable, d => {
                var res = useDisposable(d);
                return Ok(res);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> containing the result or a <see cref="Failure{T}" />, depending
        ///     on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        public static Try<B> UsingWith<Disposable, A, B>(this Try<A> @try,
            Func<A, Disposable> createDisposable,
            Func<Disposable, Try<B>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return @try.Match(
                failure: Fail<B>,
                success: a => Try.UsingWith(() => createDisposable(a), useDisposable));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> containing the result or a <see cref="Failure{T}" />, depending
        ///     on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        public static Try<B> UsingWith<Disposable, A, B>(this Try<A> @try,
            Func<Disposable> createDisposable,
            Func<Disposable, A, Try<B>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return @try.Match(
                failure: Fail<B>,
                success: a => Try.UsingWith(createDisposable, d => useDisposable(d, a)));
        }
    }
}