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
        public static Try<B> Select<A, B>([NotNull] this Try<A> @try, [NotNull] Func<A, B> @select) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            @select.ThrowIfNull(nameof(@select));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<B>,
                success: a => Try.To(() => @select(a)));
        }
    }
}