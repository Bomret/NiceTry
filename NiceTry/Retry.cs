using System;
using System.Reactive;

namespace NiceTry {
    public static class Retry {
        public static ITry<T> To<T>(Func<T> work, int retries = 1) {
            while (true) {
                var result = Try.To(work);

                if (result.IsSuccess || retries < 1)
                    return result;

                retries -= 1;
            }
        }

        public static ITry<Unit> To(Action work, int retries = 1) {
            while (true) {
                var result = Try.To(work);

                if (result.IsSuccess || retries < 1)
                    return result;

                retries -= 1;
            }
        }
    }
}