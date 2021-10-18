namespace NiceTry
{
	/// <summary>
	///     Provides factory methods to create instances of <see cref="Try{T}" />.
	/// </summary>
	public partial record Try
	{
		/// <summary>
		///     Wraps the specified <paramref name="error" /> in a <see cref="NiceTry.Failure" />.
		/// </summary>
		/// <param name="error"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		public static Try Failure(Exception error)
		{
			error.ThrowIfNull(nameof(error));

			return new Failure(error);
		}

		/// <summary>
		///     Wraps the specified <paramref name="error" /> in a <see cref="NiceTry.Failure{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="error"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> Failure<T>(Exception error)
		{
			error.ThrowIfNull(nameof(error));

			return new Failure<T>(error);
		}

		/// <summary>
		///     Lifts the specified <paramref name="func" /> into a form which can be applied to
		///     an instance of <see cref="Try{T}" /> instead of <typeparamref name="A" />.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <typeparam name="B"></typeparam>
		/// <param name="func"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="func"/> is <see langword="null"/>.
		/// </exception>
		public static Func<Try<A>, Try<B>> Lift<A, B>(Func<A, B> func)
		{
			func.ThrowIfNull(nameof(func));

			return ta => ta.Match(
					success: a => To(() => func(a)),
					failure: Failure<B>);
		}

		/// <summary>
		///     Lifts the specified <paramref name="func" /> into a form which can be applied to
		///     two instance of <see cref="Try{T}" /> instead of <typeparamref name="A" /> and <typeparamref name="B" />.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <typeparam name="B"></typeparam>
		/// <typeparam name="C"></typeparam>
		/// <param name="func"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="func"/> is <see langword="null"/>.
		/// </exception>
		public static Func<Try<A>, Try<B>, Try<C>> Lift2<A, B, C>(Func<A, B, C> func)
		{
			func.ThrowIfNull(nameof(func));

			return (ta, tb) => ta.Match(
					success: a => tb.Match(
							success: b => To(() => func(a, b)),
							failure: Failure<C>),
					failure: Failure<C>);
		}

		/// <summary>
		///     Creates a new <see cref="NiceTry.Success" />.
		/// </summary>
		public static Try Success() => new Success();

		/// <summary>
		///     Wraps the specified <paramref name="value" /> in a new <see cref="NiceTry.Success{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		public static Try<T> Success<T>(T value) => new Success<T>(value);

		/// <summary>
		///     Tries to execute the specified <paramref name="work" /> synchronously. If an
		///     exception is thrown a <see cref="NiceTry.Failure{T}" /> is returned otherwise a <see cref="NiceTry.Success{T}" />.
		/// </summary>
		/// <param name="work"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="work" /> is <see langword="null" />.
		/// </exception>
		public static Try To(Action work)
		{
			work.ThrowIfNull(nameof(work));

			return To(() =>
			{
				work();
				return Success();
			});
		}

		/// <summary>
		///     Tries to execute the specified <paramref name="work" /> synchronously and return its
		///     result. If an exception is thrown a <see cref="NiceTry.Failure{T}" /> is returned
		///     otherwise a <see cref="NiceTry.Success{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="work"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="work" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> To<T>(Func<T> work)
		{
			work.ThrowIfNull(nameof(work));

			return To(() =>
			{
				var res = work();
				return Success(res);
			});
		}

		/// <summary>
		///     Tries to execute the specified <paramref name="work" /> synchronously and return its
		///     result. If an exception is thrown or <paramref name="work" /> returns
		///     <see langword="null" />, a <see cref="NiceTry.Failure{T}" /> is returned otherwise a <see cref="NiceTry.Success{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="work"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="work" /> is <see langword="null" />.
		///  </exception>
		/// <exception cref="ArgumentException">
		///     <paramref name="work" /> produces <see langword="null" />.
		///  </exception>
		public static Try<T> To<T>(Func<Try<T>> work)
		{
			work.ThrowIfNull(nameof(work));

			Try<T> result;
			try
			{
				result = work();
			} catch (Exception ex)
			{
				return Failure<T>(ex);
			}

			return
				result ??
				throw new ArgumentException(
					"The specified expression returned null which is not allowed.",
					nameof(work));
		}
	}
}
