using System;

namespace NiceTry
{
    public static class Combinators
    {
        public static ITry<TNextResult> AndThen<TResult, TNextResult>(this ITry<TResult> result,
                                                                      Func<ITry<TResult>, ITry<TNextResult>>
                                                                          continuation)
        {
            return result.IsFailure
                       ? new Failure<TNextResult>(result.Error)
                       : continuation(result);
        }

        public static ITry<TNextResult> AndThen<TNextResult>(this ITry result,
                                                             Func<ITry, ITry<TNextResult>> continuation)
        {
            return result.IsFailure
                       ? new Failure<TNextResult>(result.Error)
                       : continuation(result);
        }

        public static ITry AndThen<TResult>(this ITry<TResult> result,
                                            Func<ITry<TResult>, ITry> continuation)
        {
            return result.IsFailure
                       ? new Failure(result.Error)
                       : continuation(result);
        }

        public static ITry AndThen(this ITry result,
                                   Func<ITry, ITry> continuation)
        {
            return result.IsFailure
                       ? new Failure(result.Error)
                       : continuation(result);
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  Func<ITry<TValue>> orElse)
        {
            return result.IsFailure
                       ? orElse()
                       : result;
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  ITry<TValue> orElse)
        {
            return result.IsFailure
                       ? orElse
                       : result;
        }

        public static ITry Apply<TValue>(this ITry<TValue> t, Action<TValue> action)
        {
            return t.IsFailure
                       ? new Failure(t.Error)
                       : Try.To(() => action(t.Value));
        }

        public static ITry<TNewValue> Map<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, TNewValue> func)
        {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : Try.To(() => func(t.Value));
        }

        public static ITry<TNewValue> FlatMap<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, ITry<TNewValue>> func)
        {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : func(t.Value);
        }

        public static ITry<TResult> LiftMap<TValue, TNextValue, TResult>(this ITry<TValue> ta,
                                                                         ITry<TNextValue> tb,
                                                                         Func<TValue, TNextValue, TResult> func)
        {
            return ta.IsFailure
                       ? new Failure<TResult>(ta.Error)
                       : ta.FlatMap(a => tb.Map(b => func(a, b)));
        }

        public static ITry<TValue> Flatten<TValue>(this ITry<ITry<TValue>> t)
        {
            return t.IsFailure
                       ? new Failure<TValue>(t.Error)
                       : t.Value;
        }

        public static ITry Flatten(this ITry<ITry> t)
        {
            return t.IsFailure
                       ? new Failure(t.Error)
                       : t.Value;
        }

        public static ITry Recover(this ITry t, Action<Exception> recover)
        {
            return t.IsFailure
                       ? Try.To(() => recover(t.Error))
                       : t;
        }

        public static ITry<TValue> Recover<TValue>(this ITry<TValue> t, Func<Exception, TValue> func)
        {
            return t.IsFailure
                       ? Try.To(() => func(t.Error))
                       : t;
        }

        public static ITry<TValue> RecoverWith<TValue>(this ITry<TValue> t, Func<Exception, ITry<TValue>> func)
        {
            return t.IsFailure
                       ? func(t.Error)
                       : t;
        }

        public static ITry RecoverWith(this ITry t, Func<Exception, ITry> func)
        {
            return t.IsFailure
                       ? func(t.Error)
                       : t;
        }

        public static ITry Transform(this ITry result,
                                     Func<ITry> whenSuccess,
                                     Func<Exception, ITry> whenFailure)
        {
            return result.IsSuccess
                       ? whenSuccess()
                       : whenFailure(result.Error);
        }

        public static ITry<TNewValue> Transform<TValue, TNewValue>(this ITry<TValue> result,
                                                                   Func<TValue, ITry<TNewValue>> whenSuccess,
                                                                   Func<Exception, ITry<TNewValue>> whenFailure)
        {
            return result.IsSuccess
                       ? whenSuccess(result.Value)
                       : whenFailure(result.Error);
        }

        public static ITry<TValue> Filter<TValue>(this ITry<TValue> result,
                                                  Func<TValue, bool> predicate)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return result.FlatMap(
                v => predicate(v)
                         ? result
                         : new Failure<TValue>(
                               new ArgumentException("The given predicate does not hold for this Try.")));
        }
    }
}