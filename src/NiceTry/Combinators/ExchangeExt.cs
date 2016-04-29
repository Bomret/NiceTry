using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators {

    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> in conjunction with
    ///     <see cref="IEnumerable{T}" /> to exchange the type positions.
    /// </summary>
    public static class ExchangeExt {

        /// <summary>
        ///     Returns all values in the specified <paramref name="tryEnumerable" /> as
        ///     <see cref="Try{T}" />, if it contains an enumerable. If
        ///     <paramref name="tryEnumerable" /> represents failure, an enumerable containing only
        ///     that failure is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tryEnumerable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="tryEnumerable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static IEnumerable<Try<T>> Exchange<T>([NotNull] this Try<IEnumerable<T>> tryEnumerable) {
            tryEnumerable.ThrowIfNull(nameof(tryEnumerable));

            return tryEnumerable.Match(
                failure: err => new[] { Fail<T>(err) }.AsEnumerable(),
                success: xs => xs.Select(Ok));
        }
    }
}