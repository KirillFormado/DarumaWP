using DarumaBLLPortable.Domain;
using System;

namespace DarumaBLLPortable.Common
{
    public class DarumaDTO
    {
        public Guid Id { get; set; }
        public string Wish { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public DateTime CreateDate { get; set; }
        public DarumaStatus Status { get; set; }        
        public string CurrentQuoteKey { get; set; }
    }
}
