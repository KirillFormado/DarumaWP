using DarumaResourcesPortable.LocalizationResources;


namespace DarumaResourcesPortable.Infrastructure
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();
        private static CitationLocalizationResources _citationResources = new CitationLocalizationResources();
        private static Friendship _frendshipResources = new Friendship();
        //private static Funy _funyResources = new Funy();
        private static Health _healthResources = new Health();
        private static Love _loveResources = new Love();
        private static Luck _luckResources = new Luck();
        private static Rich _richResources = new Rich();

        public static AppResources LocalizedResources { get { return _localizedResources; } }
        public static CitationLocalizationResources CitationResources { get { return _citationResources; } }
        public static Friendship FrendshipResources { get { return _frendshipResources; } }
        //public static Funy FunyResources { get { return _funyResources; } }
        public static Health HealthResources { get { return _healthResources; } }
        public static Love LoveResources { get { return _loveResources; } }
        public static Luck LuckResources { get { return _luckResources; } }
        public static Rich RichResources { get { return _richResources; } }
    }
}