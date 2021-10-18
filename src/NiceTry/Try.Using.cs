using System;
using static NiceTry.Predef;

namespace NiceTry
{
  /// <summary>
  ///     Provides factory methods to create instances of <see cref="Try{T}" />.
  /// </summary>
  public partial record Try
  {
    /// <summary>
    ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
    ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
    ///     and returns a <see cref="NiceTry.Success{T}" /> containing the result or a
    ///     <see cref="NiceTry.Failure{T}" />, depending on the outcome of the operation.
    /// </summary>
    /// <typeparam name="Disposable"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="createDisposable"></param>
    /// <param name="useDisposable"></param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
    /// </exception>
    public static Try<T> Using<Disposable, T>(
        Func<Disposable> createDisposable,
        Func<Disposable, T> useDisposable) where Disposable : IDisposable
    {
      createDisposable.ThrowIfNull(nameof(createDisposable));
      useDisposable.ThrowIfNull(nameof(useDisposable));

      return Using(createDisposable, disp =>
      {
        T? res = useDisposable(disp);

        return Success(res);
      });
    }

    /// <summary>
    ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
    ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
    ///     and returns a <see cref="NiceTry.Success{T}" /> containing the result or a
    ///     <see cref="NiceTry.Failure{T}" />, depending on the outcome of the operation.
    /// </summary>
    /// <typeparam name="Disposable"></typeparam>
    /// <param name="createDisposable"></param>
    /// <param name="useDisposable"></param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
    /// </exception>
    public static Try Using<Disposable>(
        Func<Disposable> createDisposable,
        Action<Disposable> useDisposable) where Disposable : IDisposable
    {
      createDisposable.ThrowIfNull(nameof(createDisposable));
      useDisposable.ThrowIfNull(nameof(useDisposable));

      return Using(createDisposable, d =>
      {
        useDisposable(d);

        return Success();
      });
    }

    /// <summary>
    ///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
    ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
    ///     and returns a <see cref="NiceTry.Success{T}" /> containing the result or a
    ///     <see cref="NiceTry.Failure{T}" />, depending on the outcome of the operation.
    /// </summary>
    /// <typeparam name="Disposable"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="createDisposable"></param>
    /// <param name="useDisposable"></param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
    /// </exception>
    public static Try<T> Using<Disposable, T>(
        Func<Disposable> createDisposable,
        Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable
    {
      createDisposable.ThrowIfNull(nameof(createDisposable));
      useDisposable.ThrowIfNull(nameof(useDisposable));

      return Try(() =>
      {
        using Disposable? disp = createDisposable();

        return useDisposable(disp);
      });
    }
  }
}
