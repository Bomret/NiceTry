using JetBrains.Annotations;
using System;
using TheVoid;

namespace NiceTry {
    /// <summary>
    ///     Provides static methods to create instances of <see cref="NiceTry.Try{T}" />. Meant for using as static
    ///     import (C# 6 feature).
    /// </summary>
    public static class Predef {        
        [NotNull]
        public static Try<T> Fail<T>([NotNull] Exception err) => NiceTry.Try.Failure<T>(err);

        [NotNull]
        public static Try<T> Ok<T>([CanBeNull] T value) => NiceTry.Try.Success(value);

        [NotNull]
        public static Try<Unit> Try([NotNull] Action work) => NiceTry.Try.To(work);

        [NotNull]
        public static Try<T> Try<T>([NotNull] Func<T> work) => NiceTry.Try.To(work);

        [NotNull]
        public static Try<T> Try<T>([NotNull] Func<Try<T>> work) => NiceTry.Try.To(work);

        [NotNull]
        public static Try<Unit> Using<Disposable>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using(createDisposable, useDisposable);

        [NotNull]
        public static Try<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, T> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using(createDisposable, useDisposable);

        [NotNull]
        public static Try<T> Using<Disposable, T>(
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using(createDisposable, useDisposable);
    }
}