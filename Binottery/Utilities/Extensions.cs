using System;
using System.Linq;

namespace Binottery.Utilities
{
    public static class Extensions
    {
        public static bool In<T>(this T val, params T[] values) where T : struct
        {
            return values.Contains(val);
        }
    }
}
