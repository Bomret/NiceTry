using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    public static class LastTryExt {
        /// <summary>
        ///     Returns the last element of a sequence wrapped in a <see cref="Success{T}" /> or a
        ///     <see cref="Failure{T}" /> if the specified <paramref name="enumerable" /> is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="enumerable" /> is <see langword="null" />. </exception>
        [NotNull]
        public static Try<T> LastTry<T>([NotNull] this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Try(enumerable.Last);
        }

        /// <summary>
        ///     Returns the last element of a sequence that satisfies a specified condition, wrapped in a
        ///     <see cref="Success{T}" /> or a <see cref="Failure{T}" /> if the specified <paramref name="enumerable" />
        ///     is empty or does not contain an element that satisfies the <paramref name="predicate" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> LastTry<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate) {
            enumerable.ThrowIfNull(nameof(enumerable));
            predicate.ThrowIfNull(nameof(predicate));

            return Try(() => enumerable.Last(predicate));
        }
    }
}