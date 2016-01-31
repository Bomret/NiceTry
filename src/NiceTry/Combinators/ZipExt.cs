using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class ZipExt {
        /// <summary>
        ///     Applies the specified <paramref name="zip" /> function to the values of the specified <paramref name="tryA" /> and
        ///     <paramref name="tryB" /> if both represent success and eventually produces a new <see cref="ITry" /> with the
        ///     result.
        ///     If <paramref name="tryA" /> or <paramref name="tryB" /> represent failure or <paramref name="zip" /> throws an
        ///     exception, a <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="tryA"></param>
        /// <param name="tryB"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="tryA" />, <paramref name="tryB" /> or <paramref name="zip" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<C> Zip<A, B, C>(this Try<A> tryA, Try<B> tryB, Func<A, B, C> zip) {
            zip.ThrowIfNull(nameof(zip));

            return ZipWith(tryA, tryB, (a, b) => {
                var c = zip(a, b);
                return Try.Success(c);
            });
        }

        /// <summary>
        ///     Applies the specified <paramref name="zip" /> function to the values of the specified <paramref name="tryA" /> and
        ///     <paramref name="tryB" /> if both represent success and eventually produces a new <see cref="ITry" />.
        ///     If <paramref name="tryA" /> or <paramref name="tryB" /> represent failure or <paramref name="zip" /> throws an
        ///     exception, a <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="tryA"></param>
        /// <param name="tryB"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="tryA" />, <paramref name="tryB" /> or <paramref name="zip" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<C> ZipWith<A, B, C>(this Try<A> tryA, Try<B> tryB, Func<A, B, Try<C>> zip) {
            tryA.ThrowIfNullOrInvalid(nameof(tryA));
            tryB.ThrowIfNullOrInvalid(nameof(tryB));
            zip.ThrowIfNull(nameof(zip));

            // ReSharper disable once AssignNullToNotNullAttribute
            return tryA.Match(
                failure: Try.Failure<C>,
                success: a => tryB.Match(
                    failure: Try.Failure<C>,
                    success: b => Try.To(() => zip(a, b))));
        }
    }
}