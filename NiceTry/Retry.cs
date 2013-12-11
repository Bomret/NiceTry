using System;
using System.Reactive;

namespace NiceTry {
    public static class Retry {
        public static ITry<TValue> To<TValue>(Func<TValue> work, int retries = 1) {
            while (true) {
                var r = Try.To(work);

                if (r.IsSuccess || retries < 1)
                    return r;

                retries -= 1;
            }
        }

        public static ITry<Unit> To(Action work, int retries = 1) {
            while (true) {
                var r = Try.To(work);

                if (r.IsSuccess || retries < 1)
                    return r;

                retries -= 1;
            }
        }
    }
}