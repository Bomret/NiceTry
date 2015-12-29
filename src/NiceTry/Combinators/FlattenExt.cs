using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class FlattenExt {
        /// <summary>
        ///     Extracts the specified <paramref name="nestedTry"/> into an unnested <see cref="ITry{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nestedTry"></param>
        /// <returns>The nested Try.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="nestedTry" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> Flatten<T>([NotNull] this ITry<ITry<T>> nestedTry) {
            nestedTry.ThrowIfNull(nameof(nestedTry));

            // ReSharper disable once AssignNullToNotNullAttribute
            return nestedTry.Match(
                Failure: Try.Failure<T>,
                Success: t => t);
        }
    }
}