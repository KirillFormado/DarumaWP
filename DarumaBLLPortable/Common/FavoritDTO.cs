using System;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Common
{
    public class FavoritDTO
    {
        public Guid Id { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public string Text { get; set; }
    }
}
