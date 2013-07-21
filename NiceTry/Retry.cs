using System;

namespace NiceTry {
    public static class Retry {
        public static ITry<TValue> To<TValue>(Func<TValue> work, int retries = 1) {
            var r = Try.To(work);

            if (r.IsSuccess || retries == 0)
                return r;

            var remainingTries = retries - 1;
            return To(work, remainingTries);
        }

        public static ITry To(Action work, int retries = 1) {
            var r = Try.To(work);

            if (r.IsSuccess || retries == 0)
                return r;

            var remainingTries = retries - 1;
            return To(work, remainingTries);
        }
    }
}