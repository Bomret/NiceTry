using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class SelectManyExt {
        /// <summary>
        ///     Projects the value of the specified <paramref name="try" /> into a new <see cref="ITry" />, if it represents
        ///     success.
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
        public static ITry<B> SelectMany<A, B>([NotNull] this ITry<A> @try, [NotNull] Func<A, ITry<B>> @select) {
            @try.ThrowIfNull(nameof(@try));
            @select.ThrowIfNull(nameof(@select));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: Try.Failure<B>,
                Success: a => Try.To(() => @select(a)));
        }

        /// <summary>
        /// Projects the value of the specified <paramref name="try" /> into a new <see cref="ITry" />, if it represents
        ///     success and invokes the specified <paramref name="resultSelect"/> on both values.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="try"></param>
        /// <param name="trySelect"></param>
        /// <param name="resultSelect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/>, <paramref name="trySelect"/> or <paramref name="resultSelect"/> is <see langword="null"/>.
        /// </exception>
        [NotNull]
        public static ITry<C> SelectMany<A, B, C>([NotNull] this ITry<A> @try, [NotNull] Func<A, ITry<B>> trySelect,
            [NotNull] Func<A, B, C> resultSelect) {
            @try.ThrowIfNull(nameof(@try));
            trySelect.ThrowIfNull(nameof(trySelect));
            resultSelect.ThrowIfNull(nameof(resultSelect));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: Try.Failure<C>,
                Success: a => Try.To(() => trySelect(a))
                    .Match(
                        Failure: Try.Failure<C>,
                        Success: b => Try.To(() => resultSelect(a, b))));
        }
    }
}