using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class JoinExt {
        /// <summary>
        ///     Joins the results of <paramref name="tryA" /> and <paramref name="tryB" /> using the
        ///     <paramref name="resultSelect" /> if both represent success and <paramref name="aKeySelect" /> and
        ///     <paramref name="bKeySelect" /> return equal keys.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="tryA"></param>
        /// <param name="tryB"></param>
        /// <param name="aKeySelect"></param>
        /// <param name="bKeySelect"></param>
        /// <param name="resultSelect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="tryA" />, <paramref name="tryB" />, <paramref name="aKeySelect" />, <paramref name="bKeySelect" />
        ///     or <paramref name="resultSelect" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<C> Join<A, B, K, C>(ITry<A> tryA, ITry<B> tryB, Func<A, K> aKeySelect,
            Func<B, K> bKeySelect, Func<A, B, C> resultSelect) {
            return Join(tryA, tryB, aKeySelect, bKeySelect, resultSelect, EqualityComparer<K>.Default);
        }

        /// <summary>
        ///     Joins the results of <paramref name="tryA" /> and <paramref name="tryB" /> using the
        ///     <paramref name="resultSelect" /> if both represent success and <paramref name="aKeySelect" /> and
        ///     <paramref name="bKeySelect" /> return equal keys.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="tryA"></param>
        /// <param name="tryB"></param>
        /// <param name="aKeySelect"></param>
        /// <param name="bKeySelect"></param>
        /// <param name="resultSelect"></param>
        /// <param name="keyCompare"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="tryA" />, <paramref name="tryB" />, <paramref name="aKeySelect" />, <paramref name="bKeySelect" />
        ///     or <paramref name="resultSelect" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<C> Join<A, B, K, C>(ITry<A> tryA, ITry<B> tryB, Func<A, K> aKeySelect,
            Func<B, K> bKeySelect, Func<A, B, C> resultSelect, IEqualityComparer<K> keyCompare) {
            tryA.ThrowIfNullOrInvalid(nameof(tryA));
            tryB.ThrowIfNullOrInvalid(nameof(tryB));
            aKeySelect.ThrowIfNull(nameof(aKeySelect));
            bKeySelect.ThrowIfNull(nameof(bKeySelect));
            resultSelect.ThrowIfNull(nameof(resultSelect));
            
            // ReSharper disable once AssignNullToNotNullAttribute
            return tryA.Match(
                failure: Try.Failure<C>,
                success: a => tryB.Match(
                    failure: Try.Failure<C>,
                    success: b => Try.To(() => {
                        var aKey = aKeySelect(a);
                        var bKey = bKeySelect(b);
                        var compare = keyCompare ?? EqualityComparer<K>.Default;

                        if (compare.Equals(aKey, bKey)) {
                            var result = resultSelect(a, b);
                            return Try.Success(result);
                        }
                        return Try.Failure<C>(new Exception($"{tryA} and {tryB} could not be joined because their keys are not equal"));
                    })));
        }
    }
}