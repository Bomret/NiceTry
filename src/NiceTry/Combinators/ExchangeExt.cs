using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> in conjunction with
	///     <see cref="IEnumerable{T}" /> to exchange the type positions.
	/// </summary>
	public static class ExchangeExt
	{
		/// <summary>
		///     Returns all values in the specified <paramref name="tryEnumerable" /> as
		///     <see cref="Try{T}" />, if it contains an enumerable. If
		///     <paramref name="tryEnumerable" /> represents failure, an enumerable containing only
		///     that failure is returned.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tryEnumerable"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="tryEnumerable" /> is <see langword="null" />.
		/// </exception>
		public static IEnumerable<Try<T>> Exchange<T>(this Try<IEnumerable<T>> tryEnumerable)
		{
			tryEnumerable.ThrowIfNull(nameof(tryEnumerable));

			return tryEnumerable.Match(
					success: xs => xs.Select(Ok),
					failure: err => new[] { Fail<T>(err) }.AsEnumerable());
		}
	}
}
