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
        public static T Get<T>([NotNull] this Try<T> @try) {
            @try.ThrowIfNullOrInvalid(nameof(@try));

            return @try.Match(
                failure: err => {
                    throw new InvalidOperationException(
                        "A failure does not contain a value. Use the InnerException property to see the exception that caused the failure.",
                        err);
                },
                success: x => x);
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
        public static T GetOrElse<T>([NotNull] this Try<T> @try, [CanBeNull] T fallback) =>
            GetOrElse(@try, () => fallback);

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
        public static T GetOrElse<T>([NotNull] this Try<T> @try, [NotNull] Func<T> fallback) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            fallback.ThrowIfNull(nameof(fallback));

            return @try.Match(
                failure: _ => fallback(),
                success: x => x);
        }
    }
}