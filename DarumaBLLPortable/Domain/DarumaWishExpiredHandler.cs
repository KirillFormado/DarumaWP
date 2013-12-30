using System;
using System.Collections.Generic;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Helpers;

namespace DarumaBLLPortable.Domain
{
    public class DarumaWishExpiredHandler
    {
        private IDarumaStorage _storage;
        private IDarumaImageUriResolver _resolver;

        public DarumaWishExpiredHandler(IDarumaStorage storage, IDarumaImageUriResolver resolver)
        {
            _storage = storage;
            _resolver = resolver;
        }

        public void CheckExpiredStatus(IEnumerable<DarumaDomain> darumaList)
        {
            var facade = new DarumaChangeStatusFacade(_resolver);

            foreach (var daruma in darumaList)
            {
                var currentDate = DateTime.Now;
                var createDate = daruma.CreateDate;
                if (createDate.AddYears(1) < currentDate && daruma.Status != DarumaStatus.ExecutedWish)
                {
                    facade.ChangeStatus(daruma, DarumaStatus.TimeExpired);
                    _storage.Update(daruma);
                }
            }
        }
    }
}
