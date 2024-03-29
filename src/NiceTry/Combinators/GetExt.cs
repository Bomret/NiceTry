using static NiceTry._;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> to get the value therein.
	/// </summary>
	public static class GetExt
	{
		/// <summary>
		///     Returns the value of the specified <paramref name="try" /> if it represents success
		///     or throws a <see cref="InvalidOperationException" /> containing the encountered
		///     exception if it represents failure.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <exception cref="InvalidOperationException">
		///     <paramref name="try" /> represents failure.
		/// </exception>
		/// <exception cref="ArgumentNullException"> <paramref name="try" /> is <see langword="null" />. </exception>
		public static T Get<T>(this Try<T> @try)
		{
			@try.ThrowIfNull(nameof(@try));

			return @try.Match(
					success: Id,
					failure: err =>
					{
						throw new InvalidOperationException(
											"A failure does not contain a value. Use the InnerException property to see the exception that caused the failure.",
											err);
					});
		}

		/// <summary>
		///     Returns the value of the specified <paramref name="try" /> if it represents success
		///     or the <paramref name="fallback" /> if it represents failure.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="fallback"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
		/// </exception>
		public static T GetOrElse<T>(this Try<T> @try, T fallback) =>
			GetOrElse(@try, () => fallback);

		/// <summary>
		///     Returns the value of the specified <paramref name="try" /> if it represents success
		///     or executes the <paramref name="fallback" /> function and returns the result if it is
		///     a failure.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="fallback"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="fallback" /> is <see langword="null" />.
		/// </exception>
		public static T GetOrElse<T>(this Try<T> @try, Func<T> fallback)
		{
			@try.ThrowIfNull(nameof(@try));
			fallback.ThrowIfNull(nameof(fallback));

			return @try.Match(
					success: Id,
					failure: _ => fallback());
		}
	}
}
