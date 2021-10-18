using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> to transform the value therein into
	///     a new form.
	/// </summary>
	public static class SelectExt
	{
		/// <summary>
		///     Projects the value of the specified <paramref name="try" /> into a new form if it
		///     represents success.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <typeparam name="B"></typeparam>
		/// <param name="try"></param>
		/// <param name="select"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="select" /> is <see langword="null" />.
		/// </exception>
		public static Try<B> Select<A, B>(
			this Try<A> @try,
			Func<A, B> @select)
		{
			@try.ThrowIfNull(nameof(@try));
			@select.ThrowIfNull(nameof(@select));

			return @try.Match(
					success: a => Try(() => @select(a)),
					failure: Fail<B>);
		}
	}
}
