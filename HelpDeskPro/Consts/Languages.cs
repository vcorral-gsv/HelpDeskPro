namespace HelpDeskPro.Consts
{
    public class Languages
    {
        private static readonly HashSet<string> Supported = new(StringComparer.OrdinalIgnoreCase)
        {
            "en", "es", "it"
        };

        public static IReadOnlyCollection<string> GetSupportedLanguageCodes() => Supported;

        public static bool IsLanguageSupported(string code)
            => !string.IsNullOrWhiteSpace(code) && Supported.Contains(code);
    }
}
