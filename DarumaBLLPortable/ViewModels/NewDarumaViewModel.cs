using System.Collections.Generic;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.ViewModels;

namespace Daruma.ViewModels
{
    public class NewDarumaViewModel : ViewModelBase
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