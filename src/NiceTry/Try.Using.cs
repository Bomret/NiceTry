using System;
using TheVoid;
using static NiceTry.Predef;

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
        public static Try<T> Using<Disposable, T>(
            Func<Disposable> createDisposable,
            Func<Disposable, T> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(createDisposable, d => {
                var res = useDisposable(d);
                return Success(res);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions and returns a
        ///     <see cref="NiceTry.Success{T}" /> containing the result or a <see cref="NiceTry.Failure{T}" />, depending on the outcome
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
        public static Try<Unit> Using<Disposable>(
            Func<Disposable> createDisposable,
            Action<Disposable> useDisposable) where Disposable : IDisposable {
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return UsingWith(createDisposable, d => {
                useDisposable(d);
                return Success(Unit.Default);
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
        public static Try<T> UsingWith<Disposable, T>(
            Func<Disposable> createDisposable,
            Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Try(() => {
                using(var d = createDisposable())
                    return useDisposable(d);
            });
        }
    }
}