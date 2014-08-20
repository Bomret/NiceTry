using System;

namespace NiceTry
{
    public static class Retry
    {
        public static Try<T> To<T>(Func<T> work, int retries = 1)
        {
            var remainingRetries = retries;

            while (true)
            {
                var result = Try.To(work);

                if (result.IsSuccess || remainingRetries < 1)
                    return result;

                remainingRetries -= 1;
            }
        }

        public static Try<Unit> To(Action work, int retries = 1)
        {
            return To(() =>
            {
                work();

                return Unit.Type;
            });
        }
    }
}