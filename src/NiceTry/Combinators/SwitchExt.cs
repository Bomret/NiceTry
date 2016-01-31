using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class SwitchExt {
        [NotNull]
        public static Try<T> Switch<T>(this Try<T> @try, [NotNull] params Try<T>[] candidates) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            candidates.ThrowIfNull(nameof(candidates));

            return SwitchOn(new[] { @try }.Concat(candidates));
        }

        [NotNull]
        public static Try<T> Switch<T>(this Try<T> @try, [NotNull] IEnumerable<Try<T>> enumerable) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            enumerable.ThrowIfNull(nameof(enumerable));

            return SwitchOn(new[] { @try }.Concat(enumerable));
        }

        [NotNull]
        public static Try<T> SwitchOn<T>([NotNull] this IEnumerable<Try<T>> enumerable) {
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