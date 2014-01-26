﻿using System;

namespace DarumaBLLPortable.Domain
{
    public class DarumaDomain
    {
        private string _currentQuoteKey;

        public Guid Id { get; set; }
        public string Wish { get; set; }
        public DarumaWishTheme Theme { get; set; }
        public DateTime CreateDate { get; set; }
        public DarumaStatus Status { get; set; }
        public Uri ImageUri { get; set; }

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

        //TODO: refactor getting url addres for Daruma image
        public DarumaDomain (string wish, DarumaWishTheme theme, Uri imageUri)
        {
            Id = Guid.NewGuid();
            Wish = wish;
            Theme = theme;
            CreateDate = DateTime.Now;
            ChangeStatus(DarumaStatus.MakedWish, imageUri);
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
