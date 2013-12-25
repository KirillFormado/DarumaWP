using System.Collections.Generic;
using DarumaBLL.Domain;

namespace Daruma.ViewModels
{
    class NewDarumaViewModel
    {
        public List<DarumaWishTheme> ThemeList { get; set; }

        public NewDarumaViewModel()
        {
            ThemeList = new List<DarumaWishTheme>
            {
                DarumaWishTheme.Friendship,
                DarumaWishTheme.Funny,
                DarumaWishTheme.Health,
                DarumaWishTheme.Love,
                DarumaWishTheme.Luck,
                DarumaWishTheme.Rich
            };
        }
    }
}