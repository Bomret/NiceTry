using System;

namespace NiceTry
{
    public interface ITry
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }
    }

    public interface ITry<out TValue>
    {
        TValue Value { get; }
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }
    }
}