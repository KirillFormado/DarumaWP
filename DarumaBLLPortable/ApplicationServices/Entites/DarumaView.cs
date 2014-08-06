using System;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ApplicationServices.Entites
{
    public class DarumaView
    {
        //IDarumaImageUriResolver _uriResolver;

        public Guid Id { get; private set; }
        public string Wish { get; private set; }
        public DarumaWishTheme Theme { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DarumaStatus Status { get; private set; }
        public Uri ImageUri { get; private set; }
        public string CurrentQuoteKey { get; set; }

        public bool HasCurrentQuoteKey
        {
            get
            {
                return !string.IsNullOrEmpty(CurrentQuoteKey);
            }
        }

        //public void ChangeStatus(DarumaStatus status)
        //{
        //    if (Status == status)
        //    {
        //        return;
        //    }

        //    ImageUri = _uriResolver.ResolveImageUri(status);
        //    Status = status;
        //}

        public DarumaView(DarumaDomain daruma, IDarumaImageUriResolver uriResolver)
        {
            //_uriResolver = uriResolver;
            Id = daruma.Id;
            Wish = daruma.Wish;
            Theme = daruma.Theme;
            CreateDate = daruma.CreateDate;
            Status = daruma.Status;
            ImageUri = uriResolver.ResolveImageUri(daruma.Status);
            CurrentQuoteKey = daruma.CurrentQuoteKey;
        }      
    }
}
