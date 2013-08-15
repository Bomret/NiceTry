using System;

namespace NiceTry.Async
{
    public interface IAsyncTry
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }

        IAsyncTry AndThen(Func<ITry, ITry> then);
    }

    public interface IAsyncTry<out T> : IAsyncTry
    {
        T Value { get; }

        IAsyncTry<TNew> AndThen<TNew>(Func<ITry<T>, ITry<TNew>> then);
    }
}