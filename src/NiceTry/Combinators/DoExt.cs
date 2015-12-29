using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class DoExt {
        /// <summary>
        ///     Executes the specified <paramref name="action" /> if the specified <paramref name="try" /> represents success.
        ///     If it represents failure or the <paramref name="action" /> throws an exception,
        ///     a <see cref="Failure" /> is returned.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry Do([NotNull] this ITry @try, [NotNull] Action action) {
            @try.ThrowIfNull(nameof(@try));
            action.ThrowIfNull(nameof(action));

            return @try.IsFailure ? @try : Try.To(action);
        }

        /// <summary>
        ///     Executes the specified <paramref name="action" /> on the contained value if the specified <paramref name="try" />
        ///     represents success. If it represents failure or the <paramref name="action" /> throws an exception, a
        ///     <see cref="Failure" /> is returned.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="action"></param>
        /// <returns>A success if the operation succeeded or else a failure containig the encountered error.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> Do<T>([NotNull] this ITry<T> @try, [NotNull] Action<T> action) {
            @try.ThrowIfNull(nameof(@try));
            action.ThrowIfNull(nameof(action));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: Try.Failure<T>,
                Success: x => {
                    var copy = x;
                    return Try.To(() => action(copy))
                        .Match(
                            Failure: Try.Failure<T>,
                            Success: () => @try);
                });
        }
    }
}