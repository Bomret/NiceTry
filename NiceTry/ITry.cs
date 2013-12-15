using System;

namespace NiceTry {
    public interface ITry<out T> {
        T Value { get; }
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }
    }
}