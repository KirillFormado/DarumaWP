using Daruma.Resources;

namespace Daruma.Infrastructure
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();
        private static CitationLocalizationResources _citationResources = new CitationLocalizationResources();

        public static AppResources LocalizedResources { get { return _localizedResources; } }
        public static CitationLocalizationResources CitationResources { get { return _citationResources; } }
    }
}