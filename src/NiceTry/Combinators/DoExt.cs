using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class DoExt {
        /// <summary>
        ///     Executes the specified <paramref name="action" /> on the contained value if the specified <paramref name="try" />
        ///     represents success. If it represents failure or the <paramref name="action" /> throws an exception, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="action"></param>
        /// <returns>A success if the operation succeeded or else a failure containing the encountered error.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> Do<T>([NotNull] this ITry<T> @try, [NotNull] Action<T> action) {
            action.ThrowIfNull(nameof(action));
            @try.ThrowIfNullOrInvalid(nameof(@try));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<T>,
                success: x => {
                    var copy = x;
                    return Try.To(() => action(copy))
                        .Match(
                            failure: Try.Failure<T>,
                            success: _ => @try);
                });
        }
    }
}