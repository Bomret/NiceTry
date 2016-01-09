using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class ApplyExt {
        /// <summary>
        ///     Applies the specified <paramref name="apply" /> to the value of the specified <paramref name="try" /> if that
        ///     represents success. If it represents failure or <paramref name="apply" /> throws an exception, a
        ///     <see cref="Failure" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="apply" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry Apply<T>([NotNull] this ITry<T> @try, [NotNull] Action<T> apply) {
            apply.ThrowIfNull(nameof(apply));
            @try.ThrowIfNullOrInvalid(nameof(@try));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: x => Try.To(() => apply(x)));
        }

        /// <summary>
        ///     Applies the specified <paramref name="apply" /> to the value of the specified <paramref name="try" /> if that
        ///     represents success. If it represents failure or <paramref name="apply" /> throws an exception, a
        ///     <see cref="Failure" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="apply"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="apply" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry ApplyWith<T>([NotNull] this ITry<T> @try, [NotNull] Func<T, ITry> apply) {
            apply.ThrowIfNull(nameof(apply));
            @try.ThrowIfNullOrInvalid(nameof(@try));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure,
                success: x => Try.To(() => apply(x)));
        }
    }
}