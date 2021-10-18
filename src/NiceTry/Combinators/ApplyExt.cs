using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> to execute side effects on the value therein.
	/// </summary>
	public static class ApplyExt
	{
		/// <summary>
		///     Applies the specified <paramref name="apply" /> to the value of the specified
		///     <paramref name="try" /> if that represents success. If it represents failure or
		///     <paramref name="apply" /> throws an exception, a <see cref="Failure{T}" /> is returned.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="apply"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="apply" /> is <see langword="null" />.
		/// </exception>
		public static Try Apply<T>(this Try<T> @try, Action<T> apply)
		{
			@try.ThrowIfNull(nameof(@try));
			apply.ThrowIfNull(nameof(apply));

			return Apply(@try, x =>
			{
				apply(x);
				return Ok();
			});
		}

		/// <summary>
		///     Applies the specified <paramref name="apply" /> to the value of the specified
		///     <paramref name="try" /> if that represents success. If it represents failure or
		///     <paramref name="apply" /> throws an exception, a <see cref="Failure{T}" /> is returned.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="apply"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="apply" /> is <see langword="null" />.
		/// </exception>
		public static Try Apply<T>(this Try<T> @try, Func<T, Try> apply)
		{
			apply.ThrowIfNull(nameof(apply));
			@try.ThrowIfNull(nameof(@try));

			return @try.Match(
				success: x => Try(() => apply(x)),
				failure: Fail);
		}

		/// <summary>
		///     Applies the specified <paramref name="apply" /> to the value of the specified
		///     <paramref name="try" /> if both represent success. If one represents failure or
		///     <paramref name="apply" /> throws an exception, a <see cref="Failure{T}" /> is returned.
		/// </summary>
		/// <param name="try"></param>
		/// <param name="apply"></param>
		/// <typeparam name="A"></typeparam>
		/// <typeparam name="B"></typeparam>
		public static Try<B> Apply<A, B>(this Try<A> @try, Try<Func<A, B>> apply)
		{
			@try.ThrowIfNull(nameof(@try));
			apply.ThrowIfNull(nameof(apply));

			return @try.Match(
				success: a => apply.Match(
					success: f => Try(() => f(a)),
					failure: Fail<B>),
				failure: Fail<B>);
		}
	}
}
