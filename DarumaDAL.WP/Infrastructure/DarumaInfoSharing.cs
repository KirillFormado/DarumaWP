using DarumaBLLPortable.Domain;
using Newtonsoft.Json;

namespace DarumaDAL.WP.Infrastructure
{
    public class  DarumaInfoSharing
    {
        //for serialization and deserialization in PhoneApplicationService
        public DarumaInfoSharing() {}

        public DarumaWishTheme WishTheme { get;
            //for serialization and deserialization in PhoneApplicationService
            set; }
        public  string Quote { get;
            //for serialization and deserialization in PhoneApplicationService
            set; }

        public static string Key
        {
            get
            {
                return "daruma_key";
            }
        }

        public DarumaInfoSharing(DarumaWishTheme theme, string quote)
        {
            WishTheme = theme;
            Quote = quote;
        }
    }
}
