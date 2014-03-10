﻿
using DarumaBLLPortable.Domain;

namespace Daruma.Infrastructure
{
    class  DarumaInfoSharing
    {

        public DarumaWishTheme WishTheme { get; private set; }
        public  string Quote { get; private set; }

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