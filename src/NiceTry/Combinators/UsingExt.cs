using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class UsingExt {
        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success" /> or a <see cref="Failure" />, depending on the outcome of the
        ///     operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry Using<Disposable>([NotNull] this ITry @try, Func<Disposable> createDisposable,
            [NotNull] Action<Disposable> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: () => Try.Using(createDisposable, useDisposable));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success" /> or a <see cref="Failure" />, depending on the outcome of the
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
        public static ITry Using<Disposable, T>([NotNull] this ITry<T> @try, [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable, T> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: x => Try.Using(createDisposable, d => useDisposable(d, x)));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> containing the result or a <see cref="Failure{T}" />, depending
        ///     on the outcome of the operation.
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
        public static ITry<T> Using<Disposable, T>([NotNull] this ITry @try, [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, T> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<T>,
                success: () => Try.Using(createDisposable, useDisposable));
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
        public static ITry<B> Using<Disposable, A, B>([NotNull] this ITry<A> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, A, B> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<B>,
                success: a => Try.Using(createDisposable, d => useDisposable(d, a)));
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
        public static ITry<B> Using<Disposable, A, B>([NotNull] this ITry<A> @try,
            [NotNull] Func<A, Disposable> createDisposable,
            [NotNull] Func<Disposable, B> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<B>,
                success: a => Try.Using(() => createDisposable(a), useDisposable));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success" /> or a <see cref="Failure" />, depending on the outcome of the
        ///     operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry UsingWith<Disposable>([NotNull] this ITry @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, ITry> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: () => Try.UsingWith(createDisposable, useDisposable));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success{T}" /> containing the result or a <see cref="Failure{T}" />, depending
        ///     on the outcome of the operation.
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
        public static ITry<T> UsingWith<Disposable, T>([NotNull] this ITry @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, ITry<T>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<T>,
                success: () => Try.UsingWith(createDisposable, useDisposable));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified <paramref name="try" />
        ///     represents success as specified by the <paramref name="createDisposable" /> and <paramref name="useDisposable" />
        ///     functions and returns a <see cref="Success" /> or a <see cref="Failure" />, depending on the outcome of the
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
        public static ITry UsingWith<Disposable, T>([NotNull] this ITry<T> @try,
            [NotNull] Func<T, Disposable> createDisposable,
            [NotNull] Func<Disposable, ITry> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: x => Try.UsingWith(() => createDisposable(x), useDisposable));
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
        ///     functions and returns a <see cref="Success" /> or a <see cref="Failure" />, depending on the outcome of the
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
        public static ITry UsingWith<Disposable, T>([NotNull] this ITry<T> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, T, ITry> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: x => Try.UsingWith(createDisposable, d => useDisposable(d, x)));
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