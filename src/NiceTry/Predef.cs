using System;

namespace NiceTry
{
	/// <summary>
	///     Provides static methods to create instances of <see cref="NiceTry.Try{T}" />. Meant for
	///     using as static import (C# 6 feature).
	/// </summary>
	public static class Predef
	{
		/// <summary>
		///     Returns an new <see cref="Failure"/> wrapping the specified <paramref name="err"/>.
		/// </summary>
		/// <param name="err"></param>
		public static Try Fail(Exception err)
		{
			return NiceTry.Try.Failure(err);
		}

		/// <summary>
		///     Returns an new <see cref="Failure{T}"/> wrapping the specified <paramref name="err"/>.
		/// </summary>
		/// <param name="err"></param>
		/// <typeparam name="T"></typeparam>
		public static Try<T> Fail<T>(Exception err)
		{
			return NiceTry.Try.Failure<T>(err);
		}

		/// <summary>
		///     Returns a new <see cref="Success"/>/>.
		/// </summary>
		public static Try Ok()
		{
			return NiceTry.Try.Success();
		}

		/// <summary>
		///     Returns a new <see cref="Success{T}"/> wrapping the specified <paramref name="value"/>.
		/// </summary>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		public static Try<T> Ok<T>(T value)
		{
			return NiceTry.Try.Success(value);
		}

		/// <summary>
		///     Calls the specified <paramref name="work"/> action and returns a <see cref="NiceTry.Try{T}"/> that represents the success or failure of that operation.
		/// </summary>
		/// <param name="work"></param>
		public static Try Try(Action work)
		{
			return NiceTry.Try.To(work);
		}

		/// <summary>
		///     Calls the specified <paramref name="work"/> function and returns a <see cref="NiceTry.Try{T}"/> that represents the success or failure of that operation.
		/// </summary>
		/// <param name="work"></param>
		/// <typeparam name="T"></typeparam>
		public static Try<T> Try<T>(Func<T> work)
		{
			return NiceTry.Try.To(work);
		}

		/// <summary>
		///     Calls the specified <paramref name="work"/> function and returns the produced <see cref="NiceTry.Try{T}"/> that represents the success or failure of that operation.
		/// </summary>
		/// <param name="work"></param>
		/// <typeparam name="T"></typeparam>
		public static Try<T> Try<T>(Func<Try<T>> work)
		{
			return NiceTry.Try.To(work);
		}

		/// <summary>
		///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
		///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
		///     and returns a <see cref="NiceTry.Try{T}" /> that represents the success or failure of that operation.
		/// </summary>
		/// <param name="createDisposable"></param>
		/// <param name="useDisposable"></param>
		/// <typeparam name="Disposable"></typeparam>
		public static Try Using<Disposable>(
				Func<Disposable> createDisposable,
				Action<Disposable> useDisposable) where Disposable : IDisposable
		{
			return NiceTry.Try.Using(createDisposable, useDisposable);
		}

		/// <summary>
		///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
		///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
		///     and returns a <see cref="NiceTry.Try{T}" /> that represents the success or failure of that operation.
		/// </summary>
		/// <param name="createDisposable">Create disposable.</param>
		/// <param name="useDisposable">Use disposable.</param>
		/// <typeparam name="Disposable">The 1st type parameter.</typeparam>
		/// <typeparam name="T">The 2nd type parameter.</typeparam>
		public static Try<T> Using<Disposable, T>(
				Func<Disposable> createDisposable,
				Func<Disposable, T> useDisposable) where Disposable : IDisposable
		{
			return NiceTry.Try.Using(createDisposable, useDisposable);
		}

		/// <summary>
		///     Creates, uses and properly disposes a <see cref="IDisposable" /> specified by the
		///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
		///     and returns a <see cref="NiceTry.Try{T}" /> that represents the success or failure of that operation.
		/// </summary>
		/// <param name="createDisposable">Create disposable.</param>
		/// <param name="useDisposable">Use disposable.</param>
		/// <typeparam name="Disposable">The 1st type parameter.</typeparam>
		/// <typeparam name="T">The 2nd type parameter.</typeparam>
		public static Try<T> Using<Disposable, T>(
				Func<Disposable> createDisposable,
				Func<Disposable, Try<T>> useDisposable) where Disposable : IDisposable
		{
			return NiceTry.Try.Using(createDisposable, useDisposable);
		}
	}
}
