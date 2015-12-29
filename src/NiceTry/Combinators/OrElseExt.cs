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
        public static ITry<T> OrElse<T>(this ITry<T> @try, T fallback) {
            @try.ThrowIfNull(nameof(@try));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: _ => Try.Success(fallback),
                Success: _ => @try);
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
        public static ITry<T> OrElse<T>(this ITry<T> @try, Func<T> fallback) {
            @try.ThrowIfNull(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: _ => Try.To(fallback),
                Success: _ => @try);
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
        public static ITry<T> OrElseWith<T>(this ITry<T> @try, ITry<T> fallback) {
            @try.ThrowIfNull(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: _ => fallback,
                Success: _ => @try);
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
        public static ITry<T> OrElseWith<T>(this ITry<T> @try, Func<ITry<T>> fallback) {
            @try.ThrowIfNull(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: _ => Try.To(fallback),
                Success: _ => @try);
        }
    }
}