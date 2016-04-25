using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> to lookup <see cref="IEnumerable{T}" /> for success. 
    /// </summary>
    public static class SwitchExt {
        /// <summary>
        ///     Returns the specified <paramref name="try" /> if it represents success. Otherwise searches the specified
        ///     <paramref name="candidates" /> for the first success. If no success can be found, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <param name="try">       </param>
        /// <param name="candidates"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="candidates" /> is <see langword="null" />.
        /// </exception>
        public static Try<T> Switch<T>(this Try<T> @try, params Try<T>[] candidates) {
            @try.ThrowIfNull(nameof(@try));
            candidates.ThrowIfNull(nameof(candidates));

            return Switch(new[] { @try }.Concat(candidates));
        }

        /// <summary>
        ///     Returns the specified <paramref name="try" /> if it represents success. Otherwise searches the specified
        ///     <paramref name="candidates" /> for the first success. If no success can be found, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <param name="try">       </param>
        /// <param name="candidates"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="candidates" /> is <see langword="null" />.
        /// </exception>
        public static Try<T> Switch<T>(this Try<T> @try, IEnumerable<Try<T>> candidates) {
            @try.ThrowIfNull(nameof(@try));
            candidates.ThrowIfNull(nameof(candidates));

            return Switch(new[] { @try }.Concat(candidates));
        }

        /// <summary>
        ///     Searches the specified <paramref name="candidates" /> for the first success. If no success can be found,
        ///     a <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <param name="candidates"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"> <paramref name="candidates" /> is <see langword="null" />. </exception>
        public static Try<T> Switch<T>(this IEnumerable<Try<T>> candidates) {
            candidates.ThrowIfNull(nameof(candidates));

            return Try(() => candidates.First(t => t.IsSuccess));
        }
    }
}