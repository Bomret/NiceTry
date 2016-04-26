using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace NiceTry.Combinators {
    public static class ContainsExt {
        /// <summary>
        ///     Returns a value that indicates if the specified <paramref name="try" /> contains the <paramref name="desiredValue" />. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="desiredValue"></param>
        /// <param name="comparer"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="try" /> is <see langword="null" />. </exception>
        public static bool Contains<T>([NotNull] this Try<T> @try, [CanBeNull] T desiredValue, [CanBeNull] IEqualityComparer<T> comparer = null) {
            @try.ThrowIfNull(nameof(@try));

            var c = comparer ?? EqualityComparer<T>.Default;

            return Contains(@try, desiredValue, c.Equals);
        }

        /// <summary>
        ///     Returns a value that indicates if the specified <paramref name="try" /> contains the
        ///     <paramref name="desiredValue" />. The specified <paramref name="compare" /> function is used to check for equality.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="desiredValue"></param>
        /// <param name="compare"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="compare" /> is <see langword="null" />.
        /// </exception>
        public static bool Contains<T>([NotNull] this Try<T> @try, [CanBeNull] T desiredValue, [NotNull] Func<T, T, bool> compare) {
            @try.ThrowIfNull(nameof(@try));
            compare.ThrowIfNull(nameof(compare));

            return @try.Match(
                failure: _ => false,
                success: x => compare(x, desiredValue));
        }
    }
}