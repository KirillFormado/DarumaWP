using System;
using DarumaBLLPortable.Common;

namespace DarumaBLLPortable.Domain
{
    public class DarumaDomain
    {
        private string _currentQuoteKey;

        public Guid Id { get; private set; }
        public string Wish { get; private set; }
        public DarumaWishTheme Theme { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DarumaStatus Status { get; private set; }
        public Uri ImageUri { get; private set; }

        public string CurrentQuoteKey
        {
            get
            {
                var key = _currentQuoteKey;
                //Can get key only one time
                //_currentQuoteKey = string.Empty;
                return key;
            }
            set
            {
                _currentQuoteKey = value;
            }
        }

        public bool HasCurrentQuoteKey 
        {
            get
            {
                return !string.IsNullOrEmpty(CurrentQuoteKey);
            } 
        }

        public DarumaDomain()
        {
        }

        //TODO: refactor getting url addres for Daruma image
        public DarumaDomain (string wish, DarumaWishTheme theme, Uri imageUri)
        {
            Id = Guid.NewGuid();
            Wish = wish;
            Theme = theme;
            CreateDate = DateTime.Now;
            ChangeStatus(DarumaStatus.MakedWish, imageUri);
        }

        public DarumaDomain(DarumaDTO dto)
        {
            Id = dto.Id;
            Wish = dto.Wish;
            Theme = dto.Theme;
            CreateDate = dto.CreateDate;
            Status = dto.Status;
            ImageUri = dto.ImageUri;
            CurrentQuoteKey = dto.CurrentQuoteKey;
        }

        public DarumaDTO GetDTO()
        {
            return new DarumaDTO
            {
                Id = Id,
                Wish = Wish,
                Theme = Theme,
                CreateDate = CreateDate,
                Status = Status,
                ImageUri = ImageUri,
                CurrentQuoteKey = CurrentQuoteKey
            };
        }

        internal void ChangeStatus(DarumaStatus newStatus, Uri newImageUri)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
            ImageUri = newImageUri;
        }
    }
}
