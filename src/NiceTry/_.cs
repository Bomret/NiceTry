using System;
using JetBrains.Annotations;

namespace NiceTry {
    static class _ {
        [ContractAnnotation("obj:null => halt")]
        public static void ThrowIfNull<T>([CanBeNull] this T obj, [NotNull] string parameterName) {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException(parameterName);
        }

        [ContractAnnotation("obj:null => true")]
        public static bool IsNull([CanBeNull] this object obj) {
            return ReferenceEquals(obj, null);
        }
    }
}