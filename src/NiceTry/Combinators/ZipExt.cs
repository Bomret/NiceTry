using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> to combine instances.
	/// </summary>
	public static class ZipExt
	{
		/// <summary>
		///     Applies the specified <paramref name="zip" /> function to the values of the specified
		///     <paramref name="tryA" /> and <paramref name="tryB" /> if both represent success. If
		///     <paramref name="tryA" /> or <paramref name="tryB" /> represent failure or
		///     <paramref name="zip" /> throws an exception, a <see cref="Failure{T}" /> is returned.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <typeparam name="B"></typeparam>
		/// <typeparam name="C"></typeparam>
		/// <param name="tryA"></param>
		/// <param name="tryB"></param>
		/// <param name="zip"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="tryA" />, <paramref name="tryB" /> or <paramref name="zip" /> is <see langword="null" />.
		/// </exception>
		public static Try<C> Zip<A, B, C>(
				this Try<A> tryA, Try<B> tryB,
				Func<A, B, C> zip)
		{
			tryA.ThrowIfNull(nameof(tryA));
			tryB.ThrowIfNull(nameof(tryB));
			zip.ThrowIfNull(nameof(zip));

			return Zip(tryA, tryB, (a, b) =>
			{
				var c = zip(a, b);
				return Ok(c);
			});
		}

		/// <summary>
		///     Applies the specified <paramref name="zip" /> function to the values of the specified
		///     <paramref name="tryA" /> and <paramref name="tryB" /> if both represent success. If
		///     <paramref name="tryA" /> or <paramref name="tryB" /> represent failure or
		///     <paramref name="zip" /> throws an exception, a <see cref="Failure{T}" /> is returned.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <typeparam name="B"></typeparam>
		/// <typeparam name="C"></typeparam>
		/// <param name="tryA"></param>
		/// <param name="tryB"></param>
		/// <param name="zip"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="tryA" />, <paramref name="tryB" /> or <paramref name="zip" /> is <see langword="null" />.
		/// </exception>
		public static Try<C> Zip<A, B, C>(
				this Try<A> tryA,
				Try<B> tryB,
				Func<A, B, Try<C>> zip)
		{
			tryA.ThrowIfNull(nameof(tryA));
			tryB.ThrowIfNull(nameof(tryB));
			zip.ThrowIfNull(nameof(zip));

			return tryA.Match(
					success: a => tryB.Match(
							success: b => Try(() => zip(a, b)),
							failure: Fail<C>),
					failure: Fail<C>);
		}
	}
}
