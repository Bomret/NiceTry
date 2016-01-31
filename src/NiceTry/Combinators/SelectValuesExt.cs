using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceTry.Combinators {
    public static class SelectValuesExt {
        /// <summary>
        ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in the elements of the specified
        ///     <paramref name="enumerable" /> that represent success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<Try<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return enumerable
                .Select(t => t.Match(
                    failure: _ => new {hasVal = false, val = default(T)},
                    success: x => new {hasVal = true, val = x}))
                .Where(o => o.hasVal)
                .Select(o => o.val);
        }
    }
}