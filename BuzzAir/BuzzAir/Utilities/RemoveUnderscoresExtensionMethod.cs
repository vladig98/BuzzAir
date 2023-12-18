namespace BuzzAir.Utilities
{
    public static class RemoveUnderscoresExtensionMethod
    {
        public static string RemoveUnderscores(this string input)
        {
            return input.Replace('_', ' ');
        }
    }
}
