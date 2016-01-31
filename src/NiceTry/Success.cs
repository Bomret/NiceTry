namespace NiceTry {
    /// <summary>
    ///     Represents the successful outcome of an operation.
    /// </summary>
    public sealed class Success<T> : Try<T> {
        public T Value { get; }

        public override TryKind Kind => TryKind.Success;

        internal Success(T value) {
            Value = value;
        }
    }
}