using System;
using TheVoid;

namespace NiceTry {
    /// <summary>
    ///     Provides static methods to create instances of <see cref="NiceTry.Try{T}" />. Meant for using as static
    ///     import (C# 6 feature).
    /// </summary>
    public static class Predef {
        public static Try<T> Fail<T>(Exception err) => NiceTry.Try.Failure<T>(err);

        public static Try<T> Ok<T>(T value) => NiceTry.Try.Success(value);

        public static Try<Unit> Try(Action work) => NiceTry.Try.To(work);

        public static Try<T> Try<T>(Func<T> work) => NiceTry.Try.To(work);

        public static Try<T> Try<T>(Func<Try<T>> work) => NiceTry.Try.To(work);

        public static Try<Unit> Using<Disposable>(
            Func<Disposable> createDisposable,
            Action<Disposable> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using(createDisposable, useDisposable);

        public static Try<T> Using<Disposable, T>(
            Func<Disposable> createDisposable,
            Func<Disposable, T> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.Using(createDisposable, useDisposable);

        public static Try<T> Using<Disposable, T>(
            Func<Disposable> createDisposable,
            Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable =>
            NiceTry.Try.UsingWith(createDisposable, useDisposable);
    }
}