using System;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Contains extension methods to work with the produced value or error inside of a <see cref="Try{T}"/>.
    /// </summary>
    public static class MatchExt {
        /// <summary>
        ///     Executes one of the given functions for success and failure and returns the result, depending on wether the specified <paramref name="try"/> represents success or failure.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <exception cref="ArgumentException">
        ///     The property Kind of <paramref name="try"/> is not a valid value of <see cref="TryKind"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/> or <paramref name="success"/> or <paramref name="failure"/> is null.
        /// </exception>
        public static B Match<A, B>(this Try<A> @try, Func<A, B> success, Func<Exception, B> failure) {
            @try.ThrowIfNull(nameof(@try));
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            switch(@try.Kind) {
            case TryKind.Failure:
                return failure(((Failure<A>)@try).Error);
            case TryKind.Success:
                return success(((Success<A>)@try).Value);
            default:
                throw new ArgumentException("The kind of the specified Try<T> is not supported.");
            }
        }

        /// <summary>
        ///     Executes one of the given side effects for success and failure, depending on wether the specified <paramref name="try"/> represents success or failure.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <exception cref="ArgumentException">
        ///     The property Kind of <paramref name="try"/> is not a valid value of <see cref="TryKind"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/> or <paramref name="success"/> or <paramref name="failure"/> is null.
        /// </exception>
        public static void Match<T>(this Try<T> @try, Action<T> success, Action<Exception> failure) {
            @try.ThrowIfNull(nameof(@try));
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            switch(@try.Kind) {
            case TryKind.Failure:
                failure(((Failure<T>)@try).Error);
                break;
            case TryKind.Success:
                success(((Success<T>)@try).Value);
                break;
            default:
                throw new ArgumentException("The kind of the specified Try<T> is not supported.");
            }
        }
    }
}

