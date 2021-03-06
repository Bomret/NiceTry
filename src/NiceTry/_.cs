using System;

namespace NiceTry {

    static class _ {

        public static T GetOrThrowIfNull<T>(this T obj, string parameterName) {
            obj.ThrowIfNull (parameterName);

            return obj;
        }

        public static T Id<T>(T x) => x;

        public static bool IsNotNull(this object obj) => !ReferenceEquals (obj, null);

        public static bool IsNull(this object obj) => ReferenceEquals (obj, null);

        public static void ThrowIfNull<T>(this T obj, string parameterName) {
            if (ReferenceEquals (obj, null))
                throw new ArgumentNullException (parameterName);
        }
    }
}