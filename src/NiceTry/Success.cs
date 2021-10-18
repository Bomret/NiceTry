namespace NiceTry
{
	/// <summary>
	///     Represents the successful outcome of an operation.
	/// </summary>
	public sealed record Success() : Try(TryKind.Success)
	{
		public override void IfSuccess(Action sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));

			sideEffect();
		}

		public override void IfFailure(Action<Exception> sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));
		}

		public override void Match(Action success, Action<Exception> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			success();
		}

		public override Try Match(Func<Try> success, Func<Exception, Try> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return success();
		}

		public override Try<T> Match<T>(Func<Try<T>> success, Func<Exception, Try<T>> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return success();
		}
	}

	/// <summary>
	///     Represents the successful outcome of an operation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Value"></param>
	public sealed record Success<T>(T Value) : Try<T>(TryKind.Success)
	{
		public override void IfFailure(Action<Exception> sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));
		}

		public override void IfSuccess(Action<T> sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));

			sideEffect(Value);
		}

		public override void IfSuccess(Action sideEffect)
		{
			sideEffect.ThrowIfNull(nameof(sideEffect));

			sideEffect();
		}

		public override void Match(Action<T> success, Action<Exception> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			success(Value);
		}

		public override B Match<B>(Func<T, B> success, Func<Exception, B> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return success(Value);
		}

		public override void Match(Action success, Action<Exception> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			success();
		}

		public override Try<B> Match<B>(Func<T, Try<B>> success, Func<Exception, Try<B>> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return success(Value);
		}

		public override Try Match(Func<Try> success, Func<Exception, Try> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return success();
		}

		public override Try<B> Match<B>(Func<Try<B>> success, Func<Exception, Try<B>> failure)
		{
			success.ThrowIfNull(nameof(success));
			failure.ThrowIfNull(nameof(failure));

			return success();
		}
	}
}
