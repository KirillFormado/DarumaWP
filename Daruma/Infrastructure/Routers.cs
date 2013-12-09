namespace Daruma.Infrastructure
{
    public class ViewUrlRouter
    {
        private const string RouteToViews = "/Views/";

        public static string MainViewUrl
        {
            get { return RouteToViews + "MainView.xaml"; }
        }

        public static string DarumaShakeViewUrl
        {
            get { return RouteToViews + "DarumaShakeView.xaml"; }
        }

        public static string NewDarumaViewUrl
        {
            get { return RouteToViews + "NewDarumaView.xaml"; }
        }

        public static string InfoDarumaViewUrl
        {
            get { return RouteToViews + "InfoDarumaView.xaml"; }
        }
    }

    public class ImageUrlRouter
    {
        private const string RouteToImages = "/Assets/Images/";

        public static string BurnedDaurmaImageUrl
        {
            get { return RouteToImages + "BurnedDaruma.png"; }
        }

        public static string NewDaurmaImageUrl
        {
            get { return RouteToImages + "NewDaruma.png"; }
        }

        public static string ResolvedDaurmaImageUrl
        {
            get { return RouteToImages + "ResolvedDaruma.png"; }
        }

        public static string WishedDaurmaImageUrl
        {
            get { return RouteToImages + "WishedDaruma.png"; }
        }
    }

    public class IconsUrlRouter
    {
        private const string RouteToIcons = "/Assets/Icons/";

        public static string AddIconUrl
        {
            get { return RouteToIcons + "add.png"; }
        }

        public static string BackIconUrl
        {
            get { return RouteToIcons + "back.png"; }
        }

        public static string CheckIconUrl
        {
            get { return RouteToIcons + "check.png"; }
        }

        public static string DeleteIconUrl
        {
            get { return RouteToIcons + "delete.png"; }
        }

        public static string QuestionMarkIconUrl
        {
            get { return RouteToIcons + "questionmark.png"; }
        }
    }
}