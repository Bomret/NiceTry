using JetBrains.Annotations;
using NiceTry.Combinators;
using System.Collections.Generic;
using System.Linq;

namespace NiceTry {
    /// <summary>
    ///     Provides factory methods to create instances of <see cref="NiceTry.Try{T}" />. 
    /// </summary>
    public static partial class Try {
        /// <summary>
        ///     Returns a single <see cref="NiceTry.Success{T}" /> containing all elements if all <see cref="Try{T}" />
        ///     in the specified <paramref name="candidates" /> represent success, or the first
        ///     <see cref="NiceTry.Failure{T}" /> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidates"></param>
        [NotNull]
        public static Try<IEnumerable<T>> AllOrFailure<T>([NotNull] params Try<T>[] candidates) =>
            candidates.AsEnumerable().AllOrFailure();

        /// <summary>
        ///     Returns a single <see cref="NiceTry.Success{T}" /> containing either the values of all elements of the
        ///     specified <paramref name="candidates" /> if all elements represent success, or the first
        ///     <see cref="NiceTry.Failure{T}" /> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidates"></param>
        [NotNull]
        public static Try<IEnumerable<T>> AllOrFailure<T>([NotNull] IEnumerable<Try<T>> candidates) =>
            candidates.AllOrFailure();

        /// <summary>
        ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in the elements of the
        ///     specified <paramref name="candidates" /> that represent success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidates"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="candidates" /> is <see langword="null" />. </exception>
        [NotNull]
        public static IEnumerable<T> SelectValues<T>([NotNull] params Try<T>[] candidates) =>
            candidates.SelectValues();

        /// <summary>
        ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in the elements of the
        ///     specified <paramref name="candidates" /> that represent success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidates"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="candidates" /> is <see langword="null" />. </exception>
        [NotNull]
        public static IEnumerable<T> SelectValues<T>([NotNull] IEnumerable<Try<T>> candidates) =>
            candidates.SelectValues();

        /// <summary>
        ///     Searches the specified <paramref name="candidates" /> for the first success. If no success can be found,
        ///     a <see cref="NiceTry.Failure{T}" /> is returned.
        /// </summary>
        /// <param name="candidates"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"> <paramref name="candidates" /> is <see langword="null" />. </exception>
        [NotNull]
        public static Try<T> Switch<T>([NotNull] params Try<T>[] candidates) =>
            candidates.Switch();

        /// <summary>
        ///     Searches the specified <paramref name="candidates" /> for the first success. If no success can be found,
        ///     a <see cref="NiceTry.Failure{T}" /> is returned.
        /// </summary>
        /// <param name="candidates"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"> <paramref name="candidates" /> is <see langword="null" />. </exception>
        [NotNull]
        public static Try<T> Switch<T>([NotNull] IEnumerable<Try<T>> candidates) =>
            candidates.Switch();
    }
}