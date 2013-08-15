using System;
using System.Threading.Tasks;

namespace NiceTry.Async
{
    internal class AsyncTry<T> : IAsyncTry<T>
    {
        public AsyncTry(Task<ITry<T>> task)
        {
            Worker = task;
        }

        public Task<ITry<T>> Worker { get; private set; }

        public bool IsSuccess
        {
            get { return Worker.Result.IsSuccess; }
        }

        public bool IsFailure
        {
            get { return Worker.Result.IsFailure; }
        }

        public Exception Error
        {
            get { return Worker.Result.Error; }
        }

        public T Value
        {
            get { return Worker.Result.Value; }
        }

        public IAsyncTry AndThen(Func<ITry, ITry> then)
        {
            var task = Worker.ContinueWith(_ => then(_.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            return new AsyncTry(task);
        }

        public IAsyncTry<TNew> AndThen<TNew>(Func<ITry<T>, ITry<TNew>> then)
        {
            var task = Worker.ContinueWith(_ => then(_.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            return new AsyncTry<TNew>(task);
        }
    }

    public class AsyncTry : IAsyncTry
    {
        public AsyncTry(Task<ITry> task)
        {
            Worker = task;
        }

        internal Task<ITry> Worker { get; private set; }

        public bool IsSuccess
        {
            get { return Worker.Result.IsSuccess; }
        }

        public bool IsFailure
        {
            get { return Worker.Result.IsFailure; }
        }

        public Exception Error
        {
            get { return Worker.Result.Error; }
        }

        public IAsyncTry AndThen(Func<ITry, ITry> then)
        {
            var task = Worker.ContinueWith(_ => then(_.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            return new AsyncTry(task);
        }
    }
}