using System;

namespace DarumaDAL.WP.Infrastructure
{
    public class ViewUrlRouter
    {
        private const string RouteToViews = "/Views/";

        public static string MainViewUrl
        {
            get { return RouteToViews + "MainView.xaml"; }
        }

        public static string FavoritsViewUrl
        {
            get { return RouteToViews + "FavoritsView.xaml"; }
        }

        public static string DarumaShakeViewUrl
        {
            get { return RouteToViews + "DarumaShakeView.xaml"; }
        }

        public static string DarumaShakeViewByIdUrl(Guid id)
        {
            return string.Format("{0}?id={1}", DarumaShakeViewUrl, id);
        }

        public static string DarumaShakeViewByIdUrlWithKey(Guid id)
        {
            return DarumaShakeViewByIdUrl(id) + "&hasKey";
        }

        public static string NewDarumaViewUrl
        {
            get { return RouteToViews + "NewDarumaView.xaml"; }
        }

        public static string InfoDarumaViewUrl
        {
            get { return RouteToViews + "InfoDarumaView.xaml"; }
        }  
        
        public static string SharingViewUrl
        {
            get { return RouteToViews + "SharingView.xaml"; }
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

    public class TilesUrlRouter
    {
        private const string RouteToTile = "/Assets/Tiles/";
        public static string DarumaSecondaryTileImageUrl
        {
            get { return RouteToTile + "DarumaSecondaryTile.png"; }
        }
    }

    public class IconsUrlRouter
    {
        private const string RouteToIcons = "/Assets/Icons/";

        public static string AddIconUrl
        {
            get { return RouteToIcons + "add.png"; }
        }
        
        public static string PinIconUrl
        {
            get { return RouteToIcons + "pin.png"; }
        }  
        
        public static string UnpinIconUrl
        {
            get { return RouteToIcons + "unpin.png"; }
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
        public static string ShareIconUrl
        {
            get { return RouteToIcons + "share.png"; }
        }

        public static string FavoritesIconUrl
        {
            get { return RouteToIcons + "starAdd.png"; }
        }

        public static string CloseIconUrl
        {
            get { return RouteToIcons + "close.png"; }
        }

        public static string RefreshIconUrl
        {
            get { return RouteToIcons + "refresh.png"; }
        }

        public static string HomeIconUrl
        {
            get { return RouteToIcons + "home.png"; }
        }
    }
}