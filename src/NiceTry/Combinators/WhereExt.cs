using System;
using JetBrains.Annotations;
using NiceTry.Exceptions;

namespace NiceTry.Combinators {
    public static class WhereExt {
        /// <summary>
        /// Filters the specified <paramref name="try" /> based on the specified <paramref name="predicate" />.
        ///     Returns a <see cref="Failure{T}" /> containing a <see cref="PredicateFailedException" /> if the predicate does not
        ///     hold.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/> or <paramref name="predicate"/> is <see langword="null"/>.
        /// </exception>
        [NotNull]
        public static ITry<T> Where<T>(this ITry<T> @try, Func<T, bool> predicate) {
            @try.ThrowIfNull(nameof(@try));
            predicate.ThrowIfNull(nameof(predicate));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: Try.Failure<T>,
                Success: x => predicate(x)
                        ? @try 
                        : Try.Failure<T>(new PredicateFailedException($"The specified predicate did not hold for the specified {@try}.")));
        }
    }
}