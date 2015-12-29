using System;
using JetBrains.Annotations;

namespace NiceTry {
    /// <summary>
    ///     Provides factory methods to create instances of <see cref="NiceTry.Try" /> and <see cref="Try" />.
    /// </summary>
    public static partial class Try {
        /// <summary>
        ///     Wraps the given <paramref name="error" /> in a <see cref="NiceTry.Failure" />.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="error" /> is <see langword="null" />
        /// </exception>
        [NotNull]
        public static ITry Failure([NotNull] Exception error) {
            error.ThrowIfNull(nameof(error));
            return new Failure(error);
        }

        /// <summary>
        ///     Wraps the given <paramref name="error" /> in a <see cref="NiceTry.Failure{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="error" /> is <see langword="null" />
        /// </exception>
        [NotNull]
        public static ITry<T> Failure<T>([NotNull] Exception error) {
            error.ThrowIfNull(nameof(error));
            return new Failure<T>(error);
        }

        /// <summary>
        ///     Returns the single instance of <see cref="NiceTry.Success" /> without a value.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        public static ITry Success() => NiceTry.Success.Instance;

        /// <summary>
        ///     Wraps the given <paramref name="value" /> in a new <see cref="NiceTry.Success" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [NotNull]
        public static ITry<T> Success<T>([CanBeNull] T value) => new Success<T>(value);

        /// <summary>
        ///     Tries to execute the given <paramref name="work" /> synchronously.
        ///     If an exception is thrown a <see cref="NiceTry.Failure" /> is returned otherwise <see cref="NiceTry.Success" />.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="work" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry To([NotNull] Action work) {
            work.ThrowIfNull(nameof(work));

            try {
                work();
                return Success();
            }
            catch (Exception ex) {
                return new Failure(ex);
            }
        }

        /// <summary>
        ///     Tries to execute the given <paramref name="work" /> synchronously and return its result.
        ///     If an exception is thrown a <see cref="NiceTry.Failure{T}" /> is returned otherwise a
        ///     <see cref="NiceTry.Success{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="work" /> is <see langword="null" />
        /// </exception>
        [NotNull]
        public static ITry<T> To<T>([NotNull] Func<T> work) {
            work.ThrowIfNull(nameof(work));

            try {
                var result = work();
                return Success(result);
            }
            catch (Exception ex) {
                return Failure<T>(ex);
            }
        }

        /// <summary>
        ///     Tries to execute the given <paramref name="work" /> synchronously and return its result.
        ///     If an exception is thrown or <paramref name="work" /> returns <see langword="null" />, a
        ///     <see cref="NiceTry.Failure{T}" /> is returned otherwise a <see cref="NiceTry.Success" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="work" /> is <see langword="null" />
        /// </exception>
        [NotNull]
        public static ITry<T> To<T>([NotNull] Func<ITry<T>> work) {
            work.ThrowIfNull(nameof(work));

            try {
                var result = work();
                if (result.IsNull())
                    throw new ArgumentException("The given work returned null which is not allowed.", nameof(work));

                return result;
            }
            catch (Exception ex) {
                return Failure<T>(ex);
            }
        }
    }
}