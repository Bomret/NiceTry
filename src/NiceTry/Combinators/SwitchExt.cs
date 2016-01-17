using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class SwitchExt {
        [NotNull]
        public static ITry<T> Switch<T>(this ITry<T> @try, [NotNull] IEnumerable<ITry<T>> enumerable) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            enumerable.ThrowIfNull(nameof(enumerable));

            if (@try.IsSuccess) return @try;

            return Try.To(() => {
                var success = enumerable.FirstOrDefault(t => t.IsSuccess);
                return success.IsNull()
                    ? @try
                    : success;
            });
        }
        
        [NotNull]
        public static ITry<T> SwitchOn<T>([NotNull] this IEnumerable<ITry<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return Try.To(() => {
                var success = enumerable.FirstOrDefault(t => t.IsSuccess);
                return success.IsNull()
                    ? Try.Failure<T>(new Exception("The specified enumerable is empty or contained no success."))
                    : success;
            });
        }
    }
}