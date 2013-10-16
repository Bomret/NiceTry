using System;

namespace NiceTry
{
    public static class Try
    {
        public static ITry To(Action work)
        {
            try
            {
                work();
                return new Success();
            }
            catch (Exception error)
            {
                return new Failure(error);
            }
        }

        public static ITry<TValue> To<TValue>(Func<TValue> work)
        {
            try
            {
                var result = work();
                return new Success<TValue>(result);
            }
            catch (Exception error)
            {
                return new Failure<TValue>(error);
            }
        }

        public static Func<ITry<TA>, ITry<TB>, ITry<TC>> Lift<TA, TB, TC>(Func<TA, TB, TC> func)
        {
            return (ta, tb) => ta.FlatMap(a => tb.Map(b => func(a, b)));
        }

        public static Func<ITry<TA>, ITry<TB>, ITry> Lift<TA, TB>(Action<TA, TB> func)
        {
            return (ta, tb) => To(() => func(ta.Value, tb.Value));
        }
    }
}