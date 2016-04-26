using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

using static NiceTry.Predef;

namespace NiceTry.Combinators {
    public static class AggregateTryExt {
        /// <summary>
        ///     Applies an accumulator function over a sequence and returns a <see cref="Try{T}" /> representing the
        ///     success or failure of that operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="aggregate"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="aggregate" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> AggregateTry<T>(
            [NotNull] this IEnumerable<Try<T>> enumerable,
            [NotNull] Func<T, T, T> aggregate) {
            enumerable.ThrowIfNull(nameof(enumerable));
            aggregate.ThrowIfNull(nameof(aggregate));

            return enumerable.Aggregate(
                (tAccu, tCurr) => tAccu.Match(
                failure: Fail<T>,
                success: x => tCurr.Match(
                    failure: Fail<T>,
                    success: y => Try(() => aggregate(x, y)))));
        }

        /// <summary>
        ///     Applies an accumulator function over a sequence and returns a <see cref="Try{T}" /> representing the
        ///     success or failure of that operation. The specified <paramref name="seed" /> value is used as the initial
        ///     accumulator value.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="seed"></param>
        /// <param name="aggregate"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="aggregate" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> AggregateTry<A, B>(
            [NotNull] this IEnumerable<Try<A>> enumerable,
            [CanBeNull] B seed,
            [NotNull] Func<B, A, B> aggregate) {
            enumerable.ThrowIfNull(nameof(enumerable));
            aggregate.ThrowIfNull(nameof(aggregate));

            return enumerable.Aggregate(
                Ok(seed),
                (tb, ta) => tb.Match(
                    failure: Fail<B>,
                    success: x => ta.Match(
                        failure: Fail<B>,
                        success: y => Try(() => aggregate(x, y)))));
        }

        /// <summary>
        ///     Applies an accumulator function over a sequence. The specified <paramref name="seed" /> value is used as
        ///     the initial accumulator value, and the specified function is used to select the result value. Returns a
        ///     <see cref="Try{T}" /> that represents the succes or failure of those operations.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="seed"></param>
        /// <param name="aggregate"></param>
        /// <param name="resultSelect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="aggregate" /> or <paramref name="resultSelect" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<C> AggregateTry<A, B, C>(
            [NotNull] this IEnumerable<Try<A>> enumerable,
            [CanBeNull] B seed,
            [NotNull] Func<B, A, B> aggregate,
            [NotNull] Func<B, C> resultSelect) {
            enumerable.ThrowIfNull(nameof(enumerable));
            aggregate.ThrowIfNull(nameof(aggregate));
            resultSelect.ThrowIfNull(nameof(resultSelect));

            return enumerable.Aggregate(
                Ok(seed),
                (tb, ta) => tb.Match(
                    failure: Fail<B>,
                    success: b => ta.Match(
                        failure: Fail<B>,
                        success: a => Try(() => aggregate(b, a)))),
                tb => tb.Match(
                    failure: Fail<C>,
                    success: b => Try(() => resultSelect(b))));
        }
    }
}