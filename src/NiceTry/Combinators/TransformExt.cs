using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class TransformExt {
        public static ITry<B> Transform<A, B>([NotNull] this ITry<A> @try, [NotNull] Func<A, B> success, [NotNull] Func<Exception, B> failure) {
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            return TransformWith(
                @try,
                a => {
                    var res = success(a);
                    return Try.Success(res);
                }, err => {
                    var res = failure(err);
                    return Try.Success(res);
                });
        }

        public static ITry<B> TransformWith<A, B>([NotNull] this ITry<A> @try, [NotNull] Func<A, ITry<B>> success, [NotNull] Func<Exception, ITry<B>> failure) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            return @try.Match(
                failure: err => Try.To(() => failure(err)),
                success: x => Try.To(() => success(x)));
        }
    }
}
