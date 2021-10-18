using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	public static class SingleTryExt
	{
		/// <summary>
		///     Returns the only element of a sequence wrapped in a <see cref="Success{T}" /> or
		///     returns a <see cref="Failure{T}" /> if there is not exactly one element in the sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="enumerable" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> SingleTry<T>(this IEnumerable<T> enumerable)
		{
			enumerable.ThrowIfNull(nameof(enumerable));

			return Try(enumerable.Single);
		}

		/// <summary>
		///     Returns the only element of a sequence that satisfies a specified condition, wrapped
		///     in a <see cref="Success{T}" /> or returns a <see cref="Failure{T}" /> if more than
		///     one such element exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="predicate"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="enumerable" /> or <paramref name="predicate" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> SingleTry<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		{
			enumerable.ThrowIfNull(nameof(enumerable));
			predicate.ThrowIfNull(nameof(predicate));

			return Try(() => enumerable.Single(predicate));
		}
	}
}
