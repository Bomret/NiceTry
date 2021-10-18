using static NiceTry.Predef;

namespace NiceTry.Combinators
{
	/// <summary>
	///     Provides extension methods for <see cref="Try{T}" /> to execute side effects on the value therein.
	/// </summary>
	public static class DoExt
	{
		/// <summary>
		///     Executes the specified <paramref name="action" /> on the contained value if the
		///     specified <paramref name="try" /> represents success. If it represents failure or the
		///     <paramref name="action" /> throws an exception, a <see cref="Failure{T}" /> is returned.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="try"></param>
		/// <param name="action"></param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="try" /> or <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public static Try<T> Do<T>(this Try<T> @try, Action<T> action)
		{
			@try.ThrowIfNull(nameof(@try));
			action.ThrowIfNull(nameof(action));

			return
				@try.Match(
					success: x =>
						Try.To(() => action(x)).Match(
							success: () => @try,
							failure: Fail<T>),
					failure: Fail<T>);
		}
	}
}
