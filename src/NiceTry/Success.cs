namespace NiceTry {
    /// <summary>
    ///     Represents the successful outcome of an operation.
    /// </summary>
    public sealed class Success<T> : Try<T> {
        /// <summary>
        ///     The result of a successful operation.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; }
        public override TryKind Kind => TryKind.Success;

        internal Success(T value) {
            Value = value;
        }
    }
}