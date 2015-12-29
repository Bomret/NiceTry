using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class GetExt {
        /// <summary>
        ///     Returns the value of the specified <paramref name="try" /> if it represents success or throws a
        ///     <see cref="InvalidOperationException" /> containing the encountered exception if it represents failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="try" /> represents failure.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> is <see langword="null" />.
        /// </exception>
        [CanBeNull]
        public static T Get<T>([NotNull] this ITry<T> @try) {
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                Failure: err => {
                    throw new InvalidOperationException(
                        "A failure does not contain a value. Use the InnerException property to see the exception that caused the failure.",
                        err);
                },
                Success: x => x);
        }

        /// <summary>
        ///     Returns the value of the specified <paramref name="try" /> if it represents success or the
        ///     <paramref name="fallback" /> if it represents failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
        /// </exception>
        [CanBeNull]
        public static T GetOrElse<T>([NotNull] this ITry<T> @try, [CanBeNull] T fallback) {
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                Failure: _ => fallback,
                Success: x => x);
        }

        /// <summary>
        ///     Returns the value of the specified <paramref name="try" /> if it represents success or executes the
        ///     <paramref name="fallback" /> function and returns the result if it is a failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
        /// </exception>
        [CanBeNull]
        public static T GetOrElse<T>([NotNull] this ITry<T> @try, [NotNull] Func<T> fallback) {
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                Failure: _ => fallback(),
                Success: x => x);
        }

        /// <summary>
        ///     Returns the value of the specified <paramref name="try" /> if it represents success or the default of
        ///     <typeparam name="T" />
        ///     if it represents failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> is <see langword="null" />.
        /// </exception>
        [CanBeNull]
        public static T GetOrDefault<T>([NotNull] this ITry<T> @try) {
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                Failure: _ => default(T),
                Success: x => x);
        }
    }
}