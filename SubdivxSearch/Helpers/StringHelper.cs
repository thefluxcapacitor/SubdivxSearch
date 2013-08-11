namespace SubdivxSearch.Helpers
{
    public class StringHelper
    {
        public static string GetDefault(string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public static string GetDefault(string value)
        {
            return GetDefault(value, "-");
        }
    }
}