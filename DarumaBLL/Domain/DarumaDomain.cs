using System;
using DarumaBLL.Common.Abstractions;

namespace DarumaBLL.Domain
{
    public class DarumaDomain
    {
        public Guid Id { get; set; }
        public string Wish { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public DateTime CreateDate { get; set; }
        public DarumaStatus Status { get; set; }
        public Uri ImageUri { get; set; }

        //TODO: refactor getting url addres for Daruma image
        public DarumaDomain (string wish, DarumaWishTheme theme, Uri imageUri)
        {
            Id = Guid.NewGuid();
            Wish = wish;
            Theme = theme;
            CreateDate = DateTime.Now;
            ChangeStatus(DarumaStatus.MakedWish, imageUri);
        }

        public void ChangeStatus(DarumaStatus newStatus, Uri newImageUri)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
            ImageUri = newImageUri;
        }
    }
}
