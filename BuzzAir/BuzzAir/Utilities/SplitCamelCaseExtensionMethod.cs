using System.Text.RegularExpressions;

namespace BuzzAir.Utilities
{
    public static class SplitCamelCaseExtensionMethod
    {
        public static string SplitCamelCase(this string input)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);


            return r.Replace(input, " ");
        }
    }
}
