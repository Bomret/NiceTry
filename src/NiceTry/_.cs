using System;
using JetBrains.Annotations;

namespace NiceTry {
    static class _ {
        [ContractAnnotation("obj:null => halt")]
        public static void ThrowIfNull<T>([CanBeNull][NoEnumeration] this T obj, [NotNull] string parameterName) {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException(parameterName);
        }

        [ContractAnnotation("obj:null => true")]
        public static bool IsNull([CanBeNull] this object obj) {
            return ReferenceEquals(obj, null);
        }

        public static void ThrowIfNullOrInvalid([NotNull] this ITry @try, string name) {
            @try.ThrowIfNull(nameof(@try));
            if (@try.IsSuccess && @try.IsFailure)
                throw new ArgumentException("The specified ITry does not represent either success or failure or represents both at once.", name);
        }
    }
}