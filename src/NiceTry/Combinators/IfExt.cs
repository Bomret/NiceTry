using System;
using NiceTry;
namespace Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to apply side effects for success and failure separately.
    /// </summary>
    public static class IfExt {
        /// <summary>
        ///     Executes the specified <paramref name="success"/> side effect if the specified <paramref name="try"/> represents success.
        ///     If <paramref name="try"/> represents failure, the side effect is not executed.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <typeparam name="T"></typeparam>
        public static void IfSuccess<T>(this Try<T> @try, Action<T> success) {
            @try.ThrowIfNull(nameof(@try));
            success.ThrowIfNull(nameof(success));

            if(@try.IsSuccess)
                success(((Success<T>)@try).Value);
        }

        /// <summary>
        ///     Executes the specified <paramref name="failure"/> side effect if the specified <paramref name="try"/> represents failure.
        ///     If <paramref name="try"/> represents success, the side effect is not executed.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <typeparam name="T"></typeparam>
        public static void IfFailure<T>(this Try<T> @try, Action<Exception> failure) {
            @try.ThrowIfNull(nameof(@try));
            failure.ThrowIfNull(nameof(failure));

            if(@try.IsFailure)
                failure(((Failure<T>)@try).Error);
        }
    }
}

