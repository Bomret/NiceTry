using System;
using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    public static class FirstTryExt {
        /// <summary>
        ///     Returns the first element of a sequence wrapped in a <see cref="Success{T}" /> or a
        ///     <see cref="Failure{T}" /> if the specified <paramref name="enumerable" /> is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="enumerable" /> is <see langword="null" />. </exception>
        public static Try<T> FirstTry<T>(this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Try(enumerable.First);
        }

        /// <summary>
        ///     Returns the first element of a sequence that satisfies a specified condition, wrapped in a
        ///     <see cref="Success{T}" /> or a <see cref="Failure{T}" /> if the specified <paramref name="enumerable" />
        ///     is empty or does not contain an element that satisfies the <paramref name="predicate" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Try<T> FirstTry<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Try(() => enumerable.First(predicate));
        }
    }
}