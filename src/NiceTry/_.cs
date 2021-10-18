using System;

namespace NiceTry
{
  internal static class _
  {
    public static T Id<T>(T x)
    {
      return x;
    }

    public static void ThrowIfNull<T>(this T obj, string parameterName)
    {
      if (obj is null)
      {
        throw new ArgumentNullException(parameterName);
      }
    }
  }
}
