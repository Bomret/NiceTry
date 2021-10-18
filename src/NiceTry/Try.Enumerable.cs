using NiceTry.Combinators;

namespace NiceTry
{
  /// <summary>
  ///     Provides factory methods to create instances of <see cref="NiceTry.Try{T}" />. 
  /// </summary>
  public partial record Try
  {
    /// <summary>
    ///     Returns a single <see cref="NiceTry.Success{T}" /> containing all elements if all
    ///     <see cref="Try{T}" /> in the specified <paramref name="candidates" /> represent
    ///     success, or the first <see cref="NiceTry.Failure{T}" /> otherwise.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="candidates"></param> 
    public static Try<IEnumerable<T>> AllOrFailure<T>(params Try<T>[] candidates) =>
        candidates.AsEnumerable().AllOrFailure();

    /// <summary>
    ///     Returns a single <see cref="NiceTry.Success{T}" /> containing either the values of
    ///     all elements of the specified <paramref name="candidates" /> if all elements
    ///     represent success, or the first <see cref="NiceTry.Failure{T}" /> otherwise.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="candidates"></param>
    public static Try<IEnumerable<T>> AllOrFailure<T>(IEnumerable<Try<T>> candidates) =>
        candidates.AllOrFailure();

    /// <summary>
    ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in
    ///     the elements of the specified <paramref name="candidates" /> that represent success.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="candidates"></param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="candidates" /> is <see langword="null" />.
    /// </exception>
    public static IEnumerable<T> SelectValues<T>(params Try<T>[] candidates) =>
        candidates.SelectValues();

    /// <summary>
    ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in
    ///     the elements of the specified <paramref name="candidates" /> that represent success.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="candidates"></param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="candidates" /> is <see langword="null" />.
    /// </exception>
    public static IEnumerable<T> SelectValues<T>(IEnumerable<Try<T>> candidates) =>
        candidates.SelectValues();

    /// <summary>
    ///     Searches the specified <paramref name="candidates" /> for the first success. If no
    ///     success can be found, a <see cref="NiceTry.Failure{T}" /> is returned.
    /// </summary>
    /// <param name="candidates"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="candidates" /> is <see langword="null" />.
    /// </exception>
    public static Try<T> Switch<T>(params Try<T>[] candidates) =>
        candidates.Switch();

    /// <summary>
    ///     Searches the specified <paramref name="candidates" /> for the first success. If no
    ///     success can be found, a <see cref="NiceTry.Failure{T}" /> is returned.
    /// </summary>
    /// <param name="candidates"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="candidates" /> is <see langword="null" />.
    /// </exception>
    public static Try<T> Switch<T>(IEnumerable<Try<T>> candidates) =>
        candidates.Switch();
  }
}