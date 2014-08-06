using System;
using System.Collections.Generic;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;

namespace DarumaDAL.WP.Infrastructure
{
    public class DarumaImageUriResolver : IDarumaImageUriResolver
    {
        private readonly Dictionary<DarumaStatus, Uri> Dict = new Dictionary<DarumaStatus, Uri>();

        public DarumaImageUriResolver()
        {
            Dict.Add(DarumaStatus.Empty, new Uri(ImageUrlRouter.NewDaurmaImageUrl, UriKind.Relative));
            Dict.Add(DarumaStatus.ExecutedWish, new Uri(ImageUrlRouter.ResolvedDaurmaImageUrl, UriKind.Relative));
            Dict.Add(DarumaStatus.MakedWish, new Uri(ImageUrlRouter.WishedDaurmaImageUrl, UriKind.Relative));
            Dict.Add(DarumaStatus.TimeExpired, new Uri(ImageUrlRouter.BurnedDaurmaImageUrl, UriKind.Relative));                    
        }

        public Uri ResolveImageUri(DarumaStatus status)
        {
            return Dict[status];
        }
    }
}
