using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class MatchExt {
        /// <summary>
        ///     Executes one of the given functions for success and failure and returns the result, depending on wether the specified <paramref name="try"/> represents success or failure.
        /// </summary>
        /// <param name="success">Function that is executed if the specified <paramref name="try"/> represents success.</param>
        /// <param name="failure">Function that is executed if the specified <paramref name="try"/> represents failure.</param>
        [CanBeNull]
        public static B Match<A, B>([NotNull] this Try<A> @try, [NotNull] Func<A, B> success, [NotNull] Func<Exception, B> failure) {
            switch(@try.Kind) {
            case TryKind.Failure:
                return failure(((Failure<A>)@try).Error);
            case TryKind.Success:
                return success(((Success<A>)@try).Value);
            default:
                throw new InvalidOperationException("The kind of the specified Try<T> is not supported.");
            }
        }

        /// <summary>
        ///     Executes one of the given side effects for success and failure, depending on wether the specified <paramref name="try"/> represents success or failure.
        /// </summary>
        /// <param name="success">Side effect that is executed if the specified <paramref name="try"/> represents success.</param>
        /// <param name="failure">Side effect that is executed if the specified <paramref name="try"/> represents failure.</param>
        public static void Match<T>([NotNull] this Try<T> @try, [NotNull] Action<T> success, [NotNull] Action<Exception> failure) {
            switch(@try.Kind) {
            case TryKind.Failure:
                failure(((Failure<T>)@try).Error);
                break;
            case TryKind.Success:
                success(((Success<T>)@try).Value);
                break;
            default:
                throw new InvalidOperationException("The kind of the specified Try<T> is not supported.");
            }
        }
    }
}

