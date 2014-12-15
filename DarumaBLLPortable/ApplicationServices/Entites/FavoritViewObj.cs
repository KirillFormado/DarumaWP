using System;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ApplicationServices.Entites
{
    public class FavoritViewObj
    {
        public Guid Id { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public string Text { get; set; }

        public FavoritViewObj()
        {
            
        }

        public FavoritViewObj(Favorit favorit)
        {
            Id = favorit.Id;
            Theme = favorit.Theme;
            Text = favorit.Text;
        }

        public Favorit ToDomain()
        {
            return new Favorit
            {
                Id = Id,
                Text = Text,
                Theme = Theme
            };
        }
    }
}
