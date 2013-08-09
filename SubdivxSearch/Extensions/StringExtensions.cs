namespace SubdivxSearch.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}