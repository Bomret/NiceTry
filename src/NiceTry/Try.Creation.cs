using JetBrains.Annotations;
using System;
using TheVoid;

namespace NiceTry {

    /// <summary> 
    ///     Provides factory methods to create instances of <see cref="NiceTry.Try{T}" />. 
    /// </summary>
    public static partial class Try {

        /// <summary> 
        ///     Wraps the specified <paramref name="error" /> in a <see cref="NiceTry.Failure{T}" />. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        /// <exception cref="ArgumentNullException"> 
        ///     <paramref name="error" /> is <see langword="null" />. 
        /// </exception>
        [NotNull]
        public static Try<T> Failure<T>([NotNull] Exception error) {
            error.ThrowIfNull (nameof (error));

            return new Failure<T> (error);
        }

        /// <summary>
        ///     Lifts the specified <paramref name="func" /> into a form which can be applied to
        ///     an instance of <see cref="Try{T}" /> instead of <typeparamref name="A" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="func"/> is <see langword="null"/>.
        /// </exception>
        [NotNull]
        public static Func<Try<A>, Try<B>> Lift<A, B>([NotNull] Func<A, B> func) {
            func.ThrowIfNull (nameof (func));

            return ta => ta.Match (
                failure: Failure<B>,
                success: a => To (() => func (a)));
        }

        /// <summary>
        ///     Lifts the specified <paramref name="func" /> into a form which can be applied to
        ///     two instance of <see cref="Try{T}" /> instead of <typeparamref name="A" /> and <typeparamref name="B" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="func"/> is <see langword="null"/>.
        /// </exception>
        [NotNull]
        public static Func<Try<A>, Try<B>, Try<C>> Lift2<A, B, C>([NotNull] Func<A, B, C> func) {
            func.ThrowIfNull (nameof (func));

            return (ta, tb) => ta.Match (
                failure: Failure<C>,
                success: a => tb.Match (
                    failure: Failure<C>,
                    success: b => To (() => func (a, b))));
        }

        /// <summary> 
        ///     Wraps the specified <paramref name="value" /> in a new <see cref="NiceTry.Success{T}" />. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        [NotNull]
        public static Try<T> Success<T>([CanBeNull] T value) => new Success<T> (value);

        /// <summary>
        ///     Tries to execute the specified <paramref name="work" /> synchronously. If an
        ///     exception is thrown a <see cref="NiceTry.Failure{T}" /> is returned otherwise a <see cref="NiceTry.Success{T}" />.
        /// </summary>
        /// <param name="work"></param>
        /// <exception cref="ArgumentNullException"> 
        ///     <paramref name="work" /> is <see langword="null" />. 
        /// </exception>
        [NotNull]
        public static Try<Unit> To([NotNull] Action work) {
            work.ThrowIfNull (nameof (work));

            return To (() => {
                work ();
                return Success (Unit.Default);
            });
        }

        /// <summary>
        ///     Tries to execute the specified <paramref name="work" /> synchronously and return its
        ///     result. If an exception is thrown a <see cref="NiceTry.Failure{T}" /> is returned
        ///     otherwise a <see cref="NiceTry.Success{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <exception cref="ArgumentNullException"> 
        ///     <paramref name="work" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> To<T>([NotNull] Func<T> work) {
            work.ThrowIfNull (nameof (work));

            return To (() => {
                var res = work ();
                return Success (res);
            });
        }

        /// <summary>
        ///     Tries to execute the specified <paramref name="work" /> synchronously and return its
        ///     result. If an exception is thrown or <paramref name="work" /> returns
        ///     <see langword="null" />, a <see cref="NiceTry.Failure{T}" /> is returned otherwise a <see cref="NiceTry.Success{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <exception cref="ArgumentNullException"> 
        ///     <paramref name="work" /> is <see langword="null" />.
        ///  </exception>
        /// <exception cref="ArgumentException"> 
        ///     <paramref name="work" /> produces <see langword="null" />.
        ///  </exception>
        [NotNull]
        public static Try<T> To<T>([NotNull] Func<Try<T>> work) {
            work.ThrowIfNull (nameof (work));

            Try<T> result;
            try {
                result = work ();
            } catch (Exception ex) {
                return Failure<T> (ex);
            }

            if (result.IsNull ()) {
                throw new ArgumentException (
                    "The specified expression returned null which is not allowed.",
                    nameof (work));
            }

            return result;
        }
    }
}