using System;
using System.Threading.Tasks;

namespace NiceTry.Async
{
    internal class AsyncTry : IAsyncTry
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

    internal class AsyncTry<TValue> : IAsyncTry<TValue>
    {
        public AsyncTry(Task<ITry<TValue>> task)
        {
            Worker = task;
        }

        public Task<ITry<TValue>> Worker { get; private set; }

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

        public TValue Value
        {
            get { return Worker.Result.Value; }
        }

        public IAsyncTry AndThen(Func<ITry, ITry> then)
        {
            var task = Worker.ContinueWith(t => then(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            return new AsyncTry(task);
        }

        public IAsyncTry<TNew> AndThen<TNew>(Func<ITry<TValue>, ITry<TNew>> then)
        {
            var task = Worker.ContinueWith(t => then(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            return new AsyncTry<TNew>(task);
        }
    }
}