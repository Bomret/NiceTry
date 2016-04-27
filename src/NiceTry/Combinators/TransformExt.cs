using JetBrains.Annotations;
using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> to transform instances of it. 
    /// </summary>
    public static class TransformExt {
        /// <summary>
        ///     Transform the specified <paramref name="try" /> using one of the specified functions for
        ///     <paramref name="success" /> and <paramref name="failure" />.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" />, <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> Transform<A, B>(
            [NotNull] this Try<A> @try,
            [NotNull] Func<A, B> success,
            [NotNull] Func<Exception, B> failure) {
            @try.ThrowIfNull(nameof(@try));
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            return Transform(
                @try,
                a => {
                    var res = success(a);
                    return Ok(res);
                },
                err => {
                    var res = failure(err);
                    return Ok(res);
                });
        }

        /// <summary>
        ///     Transform the specified <paramref name="try" /> using one of the specified functions for
        ///     <paramref name="success" /> and <paramref name="failure" />.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" />, <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> Transform<A, B>(
            [NotNull] this Try<A> @try,
            [NotNull] Func<A, Try<B>> success,
            [NotNull] Func<Exception, Try<B>> failure) {
            @try.ThrowIfNull(nameof(@try));
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            return @try.Match(
                failure: err => Try(() => failure(err)),
                success: x => Try(() => success(x)));
        }
    }
}