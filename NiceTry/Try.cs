using System;
using System.Reactive;

namespace NiceTry {
    public static class Try {
        public static ITry<Unit> To(Action work) {
            try {
                work();
                return new Success<Unit>(Unit.Default);
            }
            catch (Exception error) {
                return new Failure<Unit>(error);
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