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
        public static Try<B> SelectMany<A, B>([NotNull] this Try<A> @try, [NotNull] Func<A, Try<B>> @select) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            @select.ThrowIfNull(nameof(@select));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<B>,
                success: a => Try.To(() => @select(a)));
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
        public static Try<C> SelectMany<A, B, C>([NotNull] this Try<A> @try, [NotNull] Func<A, Try<B>> trySelect,
            [NotNull] Func<A, B, C> resultSelect) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            trySelect.ThrowIfNull(nameof(trySelect));
            resultSelect.ThrowIfNull(nameof(resultSelect));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: Try.Failure<C>,
                success: a => Try.To(() => trySelect(a))
                    .Match(
                        failure: Try.Failure<C>,
                        success: b => Try.To(() => resultSelect(a, b))));
        }
    }
}