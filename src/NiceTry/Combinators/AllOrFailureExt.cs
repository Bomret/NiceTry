﻿using System;
using System.Collections.Generic;
using System.Linq;
using static NiceTry.Predef;

namespace NiceTry.Combinators
{
  /// <summary>
  ///     Provides extensions for working with <see cref="IEnumerable{T}" /> that contain instances
  ///     of <see cref="Try{T}" />.
  /// </summary>
  public static class AllOrFailureExt
  {
    /// <summary>
    ///     Returns a single <see cref="NiceTry.Success{T}" /> containing all elements if all
    ///     <see cref="Try{T}" /> in the specified <paramref name="enumerable" /> represent
    ///     success, or the first <see cref="NiceTry.Failure{T}" /> otherwise.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="enumerable"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="enumerable" /> contains <see langword="null" /> elements.
    /// </exception>
    public static Try<IEnumerable<T>> AllOrFailure<T>(this IEnumerable<Try<T>> enumerable)
    {
      enumerable.ThrowIfNull(nameof(enumerable));

      var res = new List<T>();
      foreach (var @try in enumerable)
      {
        if (@try is null)
          throw new ArgumentException("The specified enumerable contains at least one null element.");

        Exception? err = null;
        @try.Match(
      success: x => res.Add(x),
      failure: ex => err = ex);

        if (err is not null)
          return Fail<IEnumerable<T>>(err);
      }

      return Ok(res.AsEnumerable());
    }
  }
}
