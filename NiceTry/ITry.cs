using System;

namespace NiceTry {
    public interface ITry<out TValue> {
        TValue Value { get; }
        bool IsSuccess { get; }
        bool IsFailure { get; }
        Exception Error { get; }
    }
}