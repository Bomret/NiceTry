using System.Diagnostics;

namespace NiceTry
{
	/// <summary>
	///     Represents the success or failure of an operation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DebuggerDisplay("{ToString(),nq}")]
	public abstract partial record Try(TryKind Kind)
	{
		/// <summary>
		///     Indicates if this represents failure.
		/// </summary>
		public bool IsFailure => Kind == TryKind.Failure;

		/// <summary>
		///     Indicates if this represents success.
		/// </summary>
		public bool IsSuccess => Kind == TryKind.Success;

		/// <summary>
		///     Executes the specified <paramref name="sideEffect" /> only if this instance
		///     represents failure.
		/// </summary>
		/// <param name="sideEffect"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="sideEffect" /> is <see langword="null" />.
		/// </exception>
		public abstract void IfFailure(Action<Exception> sideEffect);

		/// <summary>
		///     Executes the specified <paramref name="sideEffect" /> only if this instance
		///     represents success.
		/// </summary>
		/// <param name="sideEffect"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="sideEffect" /> is <see langword="null" />.
		/// </exception>
		public abstract void IfSuccess(Action sideEffect);

		/// <summary>
		///     Executes on of the specified side effects, depending on wether this instance
		///     represents success or failure.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="failure"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
		/// </exception>
		public abstract void Match(Action success, Action<Exception> failure);

		/// <summary>
		///     Executes on of the specified side effects, depending on wether this instance
		///     represents success or failure.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="failure"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
		/// </exception>
		public abstract Try Match(Func<Try> success, Func<Exception, Try> failure);

		/// <summary>
		///     Executes on of the specified side effects, depending on wether this instance
		///     represents success or failure.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="failure"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
		/// </exception>
		public abstract Try<T> Match<T>(Func<Try<T>> success, Func<Exception, Try<T>> failure);
	}

	/// <summary>
	///     Represents the success or failure of an operation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DebuggerDisplay("{ToString(),nq}")]
	public abstract record Try<T>(TryKind Kind) : Try(Kind)
	{
		/// <summary>
		///     Executes the specified <paramref name="sideEffect" /> only if this instance
		///     represents success.
		/// </summary>
		/// <param name="sideEffect"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="sideEffect" /> is <see langword="null" />.
		/// </exception>
		public abstract void IfSuccess(Action<T> sideEffect);

		/// <summary>
		///     Executes on of the specified side effects, depending on wether this instance
		///     represents success or failure.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="failure"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
		/// </exception>
		public abstract void Match(Action<T> success, Action<Exception> failure);

		/// <summary>
		///     Executes on of the specified side effects, depending on wether this instance
		///     represents success or failure, and returns the produced result.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="failure"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
		/// </exception>
		public abstract B Match<B>(Func<T, B> success, Func<Exception, B> failure);

		/// <summary>
		///     Executes on of the specified side effects, depending on wether this instance
		///     represents success or failure, and returns the produced result.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="failure"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
		/// </exception>
		public abstract Try<B> Match<B>(Func<T, Try<B>> success, Func<Exception, Try<B>> failure);
	}
}
