using JetBrains.Annotations;
using System;
using TheVoid;

namespace NiceTry {

    /// <summary>
    ///     Provides static methods to create instances of <see cref="NiceTry.Try{T}" />. Meant for
    ///     using as static import (C# 6 feature).
    /// </summary>
    public static class Predef {

        /// <summary>
        ///     Returns an new <see cref="NiceTry.Failure{T}"/> wrapping the specified <paramref name="err"/>.
        /// </summary>
        /// <param name="err"></param>
        /// <typeparam name="T"></typeparam>
        [NotNull]
        public static Try<T> Fail<T>([NotNull] Exception err) => NiceTry.Try.Failure<T> (err);

        /// <summary>
        ///     Returns a new <see cref="NiceTry.Success{T}"/> wrapping the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        [NotNull]
        public static Try<T> Ok<T>([CanBeNull] T value) => NiceTry.Try.Success (value);

        /// <summary>
        ///     Calls the specified <paramref name="work"/> action and returns a <see cref="NiceTry.Try{T}"/> that represents the success or failure of that operation.
        /// </summary>
        /// <param name="work"></param>
        /// <typeparam name="T"></typeparam>
        [NotNull]
        public static Try<Unit> Try([NotNull] Action work) => NiceTry.Try.To (work);

        /// <summary>
        ///     Calls the specified <paramref name="work"/> function and returns a <see cref="NiceTry.Try{T}"/> that represents the success or failure of that operation.
        /// </summary>
        /// <param name="work"></param>
        /// <typeparam name="T"></typeparam>
        [NotNull]
        public static Try<T> Try<T>([NotNull] Func<T> work) => NiceTry.Try.To (work);

        /// <summary>
        ///     Calls the specified <paramref name="work"/> function and returns the produced <see cref="NiceTry.Try{T}"/> that represents the success or failure of that operation.
        /// </summary>
        /// <param name="work"></param>
        /// <typeparam name="T"></typeparam>
        [NotNull]
        public static Try<T> Try<T>([NotNull] Func<Try<T>> work) => NiceTry.Try.To (work);

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="NiceTry.Try{T}" /> that represents the success or failure of that operation.
        /// </summary>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <typeparam name="Disposable"></typeparam>
        [NotNull]
        public static Try<Unit> Using<Disposable>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using (createDisposable, useDisposable);

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="NiceTry.Try{T}" /> that represents the success or failure of that operation.
        /// </summary>
        /// <param name="createDisposable">Create disposable.</param>
        /// <param name="useDisposable">Use disposable.</param>
        /// <typeparam name="Disposable">The 1st type parameter.</typeparam>
        /// <typeparam name="T">The 2nd type parameter.</typeparam>
        [NotNull]
        public static Try<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, T> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using (createDisposable, useDisposable);

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="NiceTry.Try{T}" /> that represents the success or failure of that operation.
        /// </summary>
        /// <param name="createDisposable">Create disposable.</param>
        /// <param name="useDisposable">Use disposable.</param>
        /// <typeparam name="Disposable">The 1st type parameter.</typeparam>
        /// <typeparam name="T">The 2nd type parameter.</typeparam>
        [NotNull]
        public static Try<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using (createDisposable, useDisposable);
    }
}