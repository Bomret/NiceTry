using System;

namespace NiceTry {
    public interface ITry {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }
    }

    public interface ITry<out TValue> {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }
        TValue Value { get; }
    }
}