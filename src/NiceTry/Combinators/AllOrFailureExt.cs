using System;
using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extensions for working with <see cref="IEnumerable{T}"/> that contain instances of <see cref="Try{T}"/>.
    /// </summary>
    public static class AllOrFailureExt {
        /// <summary>
        /// Returns a single <see cref="Success{T}"/> if all elements in the specified <paramref name="enumerable"/> represent success, or the first <see cref="Failure{T}"/> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static Try<IEnumerable<T>> AllOrFailure<T>(this IEnumerable<Try<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Try(() => {
                var res = new List<T>();
                foreach(var @try in enumerable) {
                    @try.ThrowIfNull(nameof(@try));

                    Exception err = null;
                    @try.Match(
                        failure: ex => err = ex,
                        success: x => res.Add(x));

                    if(err.IsNotNull())
                        return Fail<IEnumerable<T>>(err);
                }

                return Ok(res.AsEnumerable());
            });
        }
    }
}