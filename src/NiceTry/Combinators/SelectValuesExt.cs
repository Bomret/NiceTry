using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to extract values from enumerables of <see cref="Try{T}"/>.
    /// </summary>
    public static class SelectValuesExt {
        /// <summary>
        ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in the elements of the specified
        ///     <paramref name="enumerable" /> that represent success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains all extracted values.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<Try<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return enumerable
                        .Where(t => t.IsSuccess)
                        .Select(t => ((Success<T>)t).Value);
        }
    }
}