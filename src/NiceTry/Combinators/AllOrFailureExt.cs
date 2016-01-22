using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceTry.Combinators {
    /// <summary>
    /// Provides extensions for working with <see cref="IEnumerable{T}"/> that contain instances of <see cref="ITry{T}"/>.
    /// </summary>
    public static class AllOrFailureExt {
        /// <summary>
        /// Returns a single <see cref="Success{T}"/> if all elements in the specified <paramref name="enumerable"/> represent success, or a <see cref="Failure{T}"/> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static ITry<IEnumerable<T>> AllOrFailure<T>(this IEnumerable<ITry<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Try.To(() => {
                var res = new List<T>();
                foreach (var @try in enumerable) {
                    @try.ThrowIfNullOrInvalid(nameof(@try));

                    Exception err = null;
                    @try.Match(
                        failure: ex => err = ex,
                        success: x => res.Add(x));

                    if (err.IsNotNull())
                        return Try.Failure<IEnumerable<T>>(err);
                }

                return Try.Success(res.AsEnumerable());
            });
        }
    }
}