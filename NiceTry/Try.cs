using System;

namespace NiceTry {
    public static class Try {
        public static ITry To(Action work) {
            try {
                work();
                return new Success();
            }
            catch (Exception error) {
                return new Failure(error);
            }
        }

        public static ITry<TValue> To<TValue>(Func<TValue> work) {
            try {
                var result = work();
                return new Success<TValue>(result);
            }
            catch (Exception error) {
                return new Failure<TValue>(error);
            }
        }
    }
}