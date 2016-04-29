using JetBrains.Annotations;
using System;
using TheVoid;
using static NiceTry.Predef;

namespace NiceTry {

    public static partial class Try {

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="NiceTry.Success{T}" /> containing the result or a
        ///     <see cref="NiceTry.Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, T> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Using(createDisposable, disp => {
                var res = useDisposable(disp);
                return Success(res);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="NiceTry.Success{T}" /> containing the result or a
        ///     <see cref="NiceTry.Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<Unit> Using<Disposable>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Using(createDisposable, d => {
                useDisposable(d);
                return Success(Unit.Default);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="NiceTry.Success{T}" /> containing the result or a
        ///     <see cref="NiceTry.Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable {
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Try(() => {
                using (var disp = createDisposable())
                    return useDisposable(disp);
            });
        }
    }
}