using DarumaBLLPortable.Domain;
using Microsoft.Phone.Shell;
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

        public static DarumaInfoSharing GetQuote()
        {
            DarumaInfoSharing infoSharing = null;
            if (PhoneApplicationService.Current.State.ContainsKey(Key))
            {
                infoSharing = PhoneApplicationService.Current.State[Key] as DarumaInfoSharing;
            }

            return infoSharing;
        }

        public static void SetQuote(DarumaInfoSharing quote)
        {
            var state = PhoneApplicationService.Current.State;
            if (state.ContainsKey(Key))
            {
                state[Key] = quote;
            }
            else
            {
                state.Add(Key, quote);
            }
        }

        public DarumaInfoSharing(DarumaWishTheme theme, string quote)
        {
            WishTheme = theme;
            Quote = quote;
        }

        public DarumaInfoSharing(string quote)
        {
            WishTheme = DarumaWishTheme.NoSet;
            Quote = quote;
        }
    }
}
