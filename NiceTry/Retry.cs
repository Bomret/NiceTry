using System;

namespace NiceTry {
    public static class Retry {
        public static Try<T> To<T>(Func<T> work, int retries = 1) {
            while (true) {
                var result = Try.To(work);

                if (result.IsSuccess || retries < 1)
                    return result;

                retries -= 1;
            }
        }

        public static Try<Unit> To(Action work, int retries = 1) {
            return To(() => {
                work();

                return Unit.Default;
            });
        }
    }
}