using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extensions for working with <see cref="IEnumerable{T}" /> that contain instances of <see cref="Try{T}" />. 
    /// </summary>
    public static class AllOrFailureExt {
        /// <summary>
        ///     Returns a single <see cref="NiceTry.Success{T}" /> containing all elements if all <see cref="Try{T}" />
        ///     in the specified <paramref name="candidates" /> represent success, or the first
        ///     <see cref="NiceTry.Failure{T}" /> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="enumerable" /> contains <see langword="null" /> elements.
        /// </exception>
        [NotNull]
        public static Try<IEnumerable<T>> AllOrFailure<T>([NotNull] this IEnumerable<Try<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var res = new List<T>();
            foreach (var @try in enumerable) {
                if (@try.IsNull())
                    throw new ArgumentException("The specified enumerable contains at least one null element.");

                Exception err = null;
                @try.Match(
                    failure: ex => err = ex,
                    success: x => res.Add(x));

                if (err.IsNotNull())
                    return Fail<IEnumerable<T>>(err);
            }

            return Ok(res.AsEnumerable());
        }

        /// <summary>
        ///     Returns a single <see cref="NiceTry.Success{T}" /> containing all elements if all <see cref="Try{T}" />
        ///     in the specified <paramref name="candidates" /> represent success, or the first
        ///     <see cref="NiceTry.Failure{T}" /> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="array" /> contains <see langword="null" /> elements.
        /// </exception>
        [NotNull]
        public static Try<T[]> AllOrFailure<T>([NotNull] this Try<T>[] array) {
            array.ThrowIfNull(nameof(array));

            var res = new List<T>();
            foreach (var @try in array) {
                if (@try.IsNull())
                    throw new ArgumentException("The specified enumerable contains at least one null element.");

                Exception err = null;
                @try.Match(
                    failure: ex => err = ex,
                    success: x => res.Add(x));

                if (err.IsNotNull())
                    return Fail<T[]>(err);
            }

            return Ok(res.ToArray());
        }
    }
}