using System;
using JetBrains.Annotations;
using TheVoid;

namespace NiceTry.Combinators {
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
        [NotNull]
        public static ITry<Unit> Using<Disposable, T>(
            [NotNull] this ITry<T> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable, T> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(@try, createDisposable, (d, x) => {
                useDisposable(d, x);
                return Try.Success(Unit.Default);
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
        [NotNull]
        public static ITry<B> Using<Disposable, A, B>(
            [NotNull] this ITry<A> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, A, B> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(@try, createDisposable, (d, a) => {
                var b = useDisposable(d, a);
                return Try.Success(b);
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
        [NotNull]
        public static ITry<B> Using<Disposable, A, B>(
            [NotNull] this ITry<A> @try,
            [NotNull] Func<A, Disposable> createDisposable,
            [NotNull] Func<Disposable, B> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(@try, createDisposable, d => {
                var res = useDisposable(d);
                return Try.Success(res);
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
        [NotNull]
        public static ITry<B> UsingWith<Disposable, A, B>([NotNull] this ITry<A> @try,
            [NotNull] Func<A, Disposable> createDisposable,
            [NotNull] Func<Disposable, ITry<B>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<B>,
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
        [NotNull]
        public static ITry<B> UsingWith<Disposable, A, B>([NotNull] this ITry<A> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, A, ITry<B>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<B>,
                success: a => Try.UsingWith(createDisposable, d => useDisposable(d, a)));
        }
    }
}