using System;
using JetBrains.Annotations;

namespace NiceTry {
    public static partial class Try {
        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions and returns a
        ///     <see cref="NiceTry.Success{T}" /> containing the result or a <see cref="NiceTry.Failure{T}" />, depending on the
        ///     outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, T> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return To(() => {
                using (var d = createDisposable())
                    return useDisposable(d);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions and returns a
        ///     <see cref="NiceTry.Success" /> containing the result or a <see cref="NiceTry.Failure" />, depending on the outcome
        ///     of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry Using<Disposable>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return To(() => {
                using (var d = createDisposable())
                    useDisposable(d);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions and returns a
        ///     <see cref="NiceTry.Success{T}" /> containing the result or a <see cref="NiceTry.Failure{T}" />, depending on the
        ///     outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> UsingWith<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, ITry<T>> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return To(() => {
                using (var d = createDisposable())
                    return useDisposable(d);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions and returns a
        ///     <see cref="NiceTry.Success" /> containing the result or a <see cref="NiceTry.Failure" />, depending on the outcome
        ///     of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is
        ///     <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry UsingWith<Disposable>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, ITry> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return To(() => {
                using (var d = createDisposable())
                    return useDisposable(d);
            });
        }
    }
}