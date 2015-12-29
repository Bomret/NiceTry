using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class SelectExt {
        /// <summary>
        ///     Projects the value of the specified <paramref name="try" /> into a new form if it represents success.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="select" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<B> Select<A, B>([NotNull] this ITry<A> @try, [NotNull] Func<A, B> @select) {
            @try.ThrowIfNull(nameof(@try));
            @select.ThrowIfNull(nameof(@select));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: Try.Failure<B>,
                Success: a => Try.To(() => @select(a)));
        }
    }
}