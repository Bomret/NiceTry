using System;

namespace NiceTry {
    public static class Combinators {
        public static ITry OrElse(this ITry result,
                                  Action orElse) {
            return result.IsFailure
                       ? Try.To(orElse)
                       : result;
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  Func<TValue> orElse) {
            return result.IsFailure
                       ? Try.To(orElse)
                       : result;
        }

        public static ITry<TValue> OrElse<TValue>(this ITry<TValue> result,
                                                  TValue orElse) {
            return result.IsFailure
                       ? new Success<TValue>(orElse)
                       : result;
        }

        public static ITry<TNewValue> Map<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, TNewValue> func) {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : Try.To(() => func(t.Value));
        }

        public static ITry<TNewValue> FlatMap<TValue, TNewValue>(this ITry<TValue> t, Func<TValue, ITry<TNewValue>> func) {
            return t.IsFailure
                       ? new Failure<TNewValue>(t.Error)
                       : func(t.Value);
        }

        public static ITry Recover(this ITry t, Action<Exception> recover) {
            return t.IsFailure
                       ? Try.To(() => recover(t.Error))
                       : t;
        }

        public static ITry<TValue> Recover<TValue>(this ITry<TValue> t, Func<Exception, TValue> func) {
            return t.IsFailure
                       ? Try.To(() => func(t.Error))
                       : t;
        }

        public static ITry Transform(this ITry result,
                                     Func<ITry> whenSuccess,
                                     Func<Exception, ITry> whenFailure) {
            return result.IsSuccess
                       ? whenSuccess()
                       : whenFailure(result.Error);
        }

        public static ITry<TNewValue> Transform<TValue, TNewValue>(this ITry<TValue> result,
                                                                   Func<TValue, ITry<TNewValue>> whenSuccess,
                                                                   Func<Exception, ITry<TNewValue>> whenFailure) {
            return result.IsSuccess
                       ? whenSuccess(result.Value)
                       : whenFailure(result.Error);
        }

        public static ITry<TValue> Filter<TValue>(this ITry<TValue> result,
                                                  Func<TValue, bool> predicate) {
            if (result.IsFailure)
                return result;

            return result.FlatMap(
                v => predicate(v)
                         ? result
                         : new Failure<TValue>(
                               new InvalidOperationException("The given predicate does not hold for this try.")));
        }
    }
}