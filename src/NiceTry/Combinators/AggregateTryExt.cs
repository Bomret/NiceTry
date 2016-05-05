using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

using static NiceTry.Predef;

namespace NiceTry.Combinators {

    /// <summary>
    ///     Provides extension methods for <see cref="IEnumerable{T}" /> to aggregate without having
    ///     to cope with exceptions.
    /// </summary>
    public static class AggregateTryExt {

        /// <summary>
        ///     Applies an accumulator function over a sequence and returns a <see cref="Try{T}" />
        ///     representing the success or failure of that operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="aggregate"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="aggregate" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> AggregateTry<T>(
            [NotNull] this IEnumerable<T> enumerable,
            [NotNull] Func<T, T, T> aggregate) {
            enumerable.ThrowIfNull (nameof (enumerable));
            aggregate.ThrowIfNull (nameof (aggregate));

            return Try (() => enumerable.Aggregate (aggregate));
        }

        /// <summary>
        ///     Applies an accumulator function over a sequence and returns a <see cref="Try{T}" />
        ///     representing the success or failure of that operation. The specified
        ///     <paramref name="seed" /> value is used as the initial accumulator value.
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
            [NotNull] this IEnumerable<A> enumerable,
            [CanBeNull] B seed,
            [NotNull] Func<B, A, B> aggregate) {
            enumerable.ThrowIfNull (nameof (enumerable));
            aggregate.ThrowIfNull (nameof (aggregate));

            return Try (() => enumerable.Aggregate (seed, aggregate));
        }

        /// <summary>
        ///     Applies an accumulator function over a sequence. The specified
        ///     <paramref name="seed" /> value is used as the initial accumulator value, and the
        ///     specified function is used to select the result value. Returns a
        ///     <see cref="Try{T}" /> that represents the succes or failure of those operations.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="seed"></param>
        /// <param name="aggregate"></param>
        /// <param name="resultSelector"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="aggregate" /> or
        ///     <paramref name="resultSelector" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<C> AggregateTry<A, B, C>(
            [NotNull] this IEnumerable<A> enumerable,
            [CanBeNull] B seed,
            [NotNull] Func<B, A, B> aggregate,
            [NotNull] Func<B, C> resultSelector) {
            enumerable.ThrowIfNull (nameof (enumerable));
            aggregate.ThrowIfNull (nameof (aggregate));
            resultSelector.ThrowIfNull (nameof (resultSelector));

            return Try (() => enumerable.Aggregate (seed, aggregate, resultSelector));
        }
    }
}