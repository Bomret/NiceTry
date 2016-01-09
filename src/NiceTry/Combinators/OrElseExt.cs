using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class OrElseExt {
        /// <summary>
        ///     Returns the specified <paramref name="try" /> if it represents success or else a <see cref="Success{T}" />
        ///     containing the <paramref name="fallback" /> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> OrElse<T>([NotNull] this ITry<T> @try, [CanBeNull] T fallback) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            
            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: _ => Try.Success(fallback),
                success: _ => @try);
        }

        /// <summary>
        ///     Returns the specified <paramref name="try" /> if it represents success or else tries to evaluate the specified
        ///     <paramref name="fallback" /> function and its eventual result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="fallback"/> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> OrElse<T>([NotNull] this ITry<T> @try, [NotNull] Func<T> fallback) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: _ => Try.To(fallback),
                success: _ => @try);
        }

        /// <summary>
        ///     Returns the specified <paramref name="try" /> if it represents success or else the specified
        ///     <paramref name="fallback" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="fallback"/> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> OrElseWith<T>([NotNull] this ITry<T> @try, [NotNull] ITry<T> fallback) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: _ => fallback,
                success: _ => @try);
        }

        /// <summary>
        ///     Returns the specified <paramref name="try" /> if it represents success or else tries to evaluate the specified
        ///     <paramref name="fallback" /> function and its eventual result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="fallback"/> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> OrElseWith<T>([NotNull] this ITry<T> @try, [NotNull] Func<ITry<T>> fallback) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: _ => Try.To(fallback),
                success: _ => @try);
        }
    }
}