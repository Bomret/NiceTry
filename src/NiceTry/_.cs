using System;
using JetBrains.Annotations;

namespace NiceTry {
    static class _ {
        [ContractAnnotation("obj:null => halt")]
        public static void ThrowIfNull<T>([CanBeNull][NoEnumeration] this T obj, [NotNull] string parameterName) {
            if(ReferenceEquals(obj, null))
                throw new ArgumentNullException(parameterName);
        }

        [ContractAnnotation("obj:null => true")]
        public static bool IsNull([CanBeNull] this object obj) => ReferenceEquals(obj, null);

        [ContractAnnotation("obj:null => false")]
        public static bool IsNotNull([CanBeNull] this object obj) => !ReferenceEquals(obj, null);

        public static void ThrowIfNullOrInvalid<T>([NotNull] this ITry<T> @try, string name) {
            @try.ThrowIfNull(nameof(@try));
            if(@try.IsSuccess && @try.IsFailure)
                throw new ArgumentException("The specified ITry<T> does not represent either success or failure or represents both at once.", name);
        }

        public static int CombineHashCodes(int h1, int h2) => ((h1 << 5) + h1) ^ h2;
    }
}