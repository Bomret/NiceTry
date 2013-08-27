using System;

namespace NiceTry
{
    public sealed class Success : ITry
    {
        public bool IsSuccess
        {
            get { return true; }
        }

        public bool IsFailure
        {
            get { return false; }
        }

        public Exception Error
        {
            get { return null; }
        }
    }

    public sealed class Success<TValue> : ITry<TValue>
    {
        public Success(TValue value)
        {
            Value = value;
        }

        public bool IsSuccess
        {
            get { return true; }
        }

        public bool IsFailure
        {
            get { return false; }
        }

        public Exception Error
        {
            get { return null; }
        }

        public TValue Value { get; private set; }
    }
}