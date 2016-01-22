using System;
using JetBrains.Annotations;
using TheVoid;

namespace NiceTry {
    /// <summary>
    ///     Provides factory methods to create instances of <see cref="NiceTry.Try" /> and <see cref="Try" />.
    /// </summary>
    public static partial class Try {
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
        ///     Wraps the given <paramref name="value" /> in a new <see cref="NiceTry.Success{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [NotNull]
        public static ITry<T> Success<T>([CanBeNull] T value) => new Success<T>(value);

        /// <summary>
        ///     Tries to execute the given <paramref name="work" /> synchronously.
        ///     If an exception is thrown a <see cref="NiceTry.Failure{T}" /> is returned otherwise <see cref="NiceTry.Success{T}" />.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="work" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<Unit> To([NotNull] Action work) {
            work.ThrowIfNull(nameof(work));

            return To(() => {
                work();
                return Success(Unit.Default);
            });
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

            return To(() => {
                var res = work();
                return Success(res);
            });
        }

        /// <summary>
        ///     Tries to execute the specified <paramref name="work" /> synchronously and return its result.
        ///     If an exception is thrown or <paramref name="work" /> returns <see langword="null" />, a
        ///     <see cref="NiceTry.Failure{T}" /> is returned otherwise a <see cref="NiceTry.Success{T}" />.
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
                if(result.IsNull()) {
                    throw new ArgumentException(
                        "The specified expression returned null which is not allowed.",
                        nameof(work));
                }

                return result;
            } catch(Exception ex) {
                return Failure<T>(ex);
            }
        }

        /// <summary>
        ///     Transforms the specified <paramref name="func" /> into a form which can be applied to an instance of
        ///     <see cref="ITry{T}" /> instead of <typeparamref name="A" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [NotNull]
        public static Func<ITry<A>, ITry<B>> Lift<A, B>([NotNull] Func<A, B> func) {
            func.ThrowIfNull(nameof(func));
            return ta => ta.Match(
                failure: Failure<B>,
                success: a => To(() => func(a)));
        }

        /// <summary>
        ///     Transforms the specified <paramref name="func" /> into a form which can be applied to two instance of
        ///     <see cref="ITry{T}" /> instead of <typeparamref name="A" /> and <typeparamref name="B" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [NotNull]
        public static Func<ITry<A>, ITry<B>, ITry<C>> Lift2<A, B, C>([NotNull] Func<A, B, C> func) {
            func.ThrowIfNull(nameof(func));
            return (ta, tb) => ta.Match(
                failure: Failure<C>,
                success: a => tb.Match(
                    failure: Failure<C>,
                    success: b => To(() => func(a, b))));
        }
    }
}