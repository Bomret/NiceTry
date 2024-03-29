using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> to provide fallbacks in case of failure.
	/// </summary>
	public static class OrElseExt
	{
		/// <summary>
		///     Returns the specified <paramref name="try" /> if it represents success or else a
		///     <see cref="Success{T}" /> containing the <paramref name="fallback" /> value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="fallback"></param>
		/// <exception cref="ArgumentNullException"> <paramref name="try" /> is <see langword="null" />. </exception>
		public static Try<T> OrElse<T>(this Try<T> @try, T fallback)
		{
			@try.ThrowIfNull(nameof(@try));

			return OrElse(@try, () => Ok(fallback));
		}

		/// <summary>
		///     Returns the specified <paramref name="try" /> if it represents success or else tries
		///     to evaluate the specified <paramref name="fallback" /> function and its result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="fallback"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> OrElse<T>(this Try<T> @try, Func<T> fallback)
		{
			@try.ThrowIfNull(nameof(@try));
			fallback.ThrowIfNull(nameof(fallback));

			return OrElse(@try, () =>
			{
				var res = fallback();
				return Ok(res);
			});
		}

		/// <summary>
		///     Returns the specified <paramref name="try" /> if it represents success or else the
		///     specified <paramref name="fallback" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="fallback"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> OrElse<T>(this Try<T> @try, Try<T> fallback)
		{
			@try.ThrowIfNull(nameof(@try));
			fallback.ThrowIfNull(nameof(fallback));

			return OrElse(@try, () => fallback);
		}

		/// <summary>
		///     Returns the specified <paramref name="try" /> if it represents success or else tries
		///     to evaluate the specified <paramref name="fallback" /> function and its eventual result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="fallback"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> OrElse<T>(this Try<T> @try, Func<Try<T>> fallback)
		{
			@try.ThrowIfNull(nameof(@try));
			fallback.ThrowIfNull(nameof(fallback));

			return @try.Match(
					success: _ => @try,
					failure: _ => Try(fallback));
		}
	}
}
