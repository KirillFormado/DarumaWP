using System;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Common
{
    public class DarumaDTO
    {
        public Guid Id { get; set; }
        public string Wish { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public DateTime CreateDate { get; set; }
        public DarumaStatus Status { get; set; }
        public Uri ImageUri { get; set; }
        public string CurrentQuoteKey { get; set; }
    }
}
