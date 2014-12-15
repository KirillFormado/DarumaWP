using System;

namespace DarumaBLLPortable.Domain
{
    public class Favorit
    {
        public Guid Id { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public string Text { get; set; }
    }
}
