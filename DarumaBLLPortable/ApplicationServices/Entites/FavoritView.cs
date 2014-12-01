using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ApplicationServices.Entites
{
    public class FavoritView
    {
        public DarumaWishTheme Theme { get; set; }
        public string Text { get; set; }

        public FavoritView()
        {
            
        }

        public FavoritView(Favorit favorit)
        {
            Theme = favorit.Theme;
            Text = favorit.Text;
        }

        public Favorit ToDomain()
        {
            return new Favorit
            {
                Text = Text,
                Theme = Theme
            };
        }
    }
}
