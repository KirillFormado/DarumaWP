using DarumaBLLPortable.Common;
using System;

namespace DarumaBLLPortable.Domain
{
    public class DarumaDomain
    {
        private string _currentQuoteKey;

        public Guid Id { get; protected set; }
        public string Wish { get; protected set; }
        public DarumaWishTheme Theme { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public DarumaStatus Status { get; protected set; }
        
        public Color Color { get; set; }

        public string CurrentQuoteKey
        {
            get
            {
               return _currentQuoteKey;
            }
            set
            {
                _currentQuoteKey = value;
            }
        }

        protected DarumaDomain()
        {
        }

        public DarumaDomain (string wish, DarumaWishTheme theme)
        {
            Id = Guid.NewGuid();
            Wish = wish;
            Theme = theme;
            //TODO: remove datetime Now for more testable code
            CreateDate = DateTime.Now;
            ChangeStatus(DarumaStatus.MakedWish);
        }

        public void ChangeStatus(DarumaStatus newStatus)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
        }

        public bool IsExpired()
        {
            //TODO: remove datetime Now for more testable code
            var currentDate = DateTime.Now;
            var createDate = CreateDate;

            return createDate.AddYears(1) < currentDate 
                && Status != DarumaStatus.ExecutedWish;           
        }

        public bool TryExecuteWish()
        {
            if (Status != DarumaStatus.MakedWish)
                return false;

            ChangeStatus(DarumaStatus.ExecutedWish);
            return true;
        }        
    }
}
