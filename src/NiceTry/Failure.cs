using System;

namespace NiceTry
{
	/// <summary>
	///     Represents the failed outcome of an operation.
	/// </summary>
	public sealed record Failure(Exception Error) : Try(TryKind.Failure)
	{
		public override void IfFailure(Action<Exception> sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));

			sideEffect(Error);
		}

		public override void IfSuccess(Action sideEffect) =>
				sideEffect.ThrowIfNull(nameof(sideEffect));

		public override void Match(Action success, Action<Exception> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			failure(Error);
		}

		public override Try Match(Func<Try> success, Func<Exception, Try> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return failure(Error);
		}

		public override Try<T> Match<T>(Func<Try<T>> success, Func<Exception, Try<T>> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return failure(Error);
		}
	}

	/// <summary>
	///     Represents the failed outcome of an operation.
	/// </summary>
	public sealed record Failure<T>(Exception Error) : Try<T>(TryKind.Failure)
	{
		public override void IfFailure(Action<Exception> sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));

			sideEffect(Error);
		}

		public override void IfSuccess(Action<T> sideEffect) =>
			sideEffect.ThrowIfNull(nameof(sideEffect));

		public override void IfSuccess(Action sideEffect) =>
			sideEffect.ThrowIfNull(nameof(sideEffect));

		public override void Match(Action<T> success, Action<Exception> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			failure(Error);
		}

		public override B Match<B>(Func<T, B> success, Func<Exception, B> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return failure(Error);
		}

		public override void Match(Action success, Action<Exception> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			failure(Error);
		}

		public override Try<B> Match<B>(Func<T, Try<B>> success, Func<Exception, Try<B>> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return failure(Error);
		}

		public override Try Match(Func<Try> success, Func<Exception, Try> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return failure(Error);
		}

		public override Try<B> Match<B>(Func<Try<B>> success, Func<Exception, Try<B>> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return failure(Error);
		}
	}
}
