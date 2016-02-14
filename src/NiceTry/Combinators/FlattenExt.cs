using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators
{
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to extract nested instances.
    /// </summary>
    public static class FlattenExt {
        /// <summary>
        ///     Extracts the specified <paramref name="nestedTry"/> into an unnested <see cref="Try{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nestedTry"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="nestedTry" /> is <see langword="null" />.
        /// </exception>
        public static Try<T> Flatten<T>(this Try<Try<T>> nestedTry) {
            nestedTry.ThrowIfNull(nameof(nestedTry));

            return nestedTry.Match(
                failure: Fail<T>,
                success: t => t);
        }
    }
}