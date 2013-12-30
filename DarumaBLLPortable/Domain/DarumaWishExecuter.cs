using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Helpers;

namespace DarumaBLLPortable.Domain
{
    public class DarumaWishExecuter
    {
        private IDarumaStorage _storage;
        private IDarumaImageUriResolver _urlResolver;

        public DarumaWishExecuter(IDarumaStorage storage, IDarumaImageUriResolver urlResolver)
        {
            _storage = storage;
            _urlResolver = urlResolver;
        }

        public bool TryExecuteWish(DarumaDomain daruma)
        {
            if (daruma.Status != DarumaStatus.MakedWish)
                return false;

            var facade = new DarumaChangeStatusFacade(_urlResolver);
            facade.ChangeStatus(daruma, DarumaStatus.ExecutedWish);

            _storage.Update(daruma);

            return true;
        }
    }
}
