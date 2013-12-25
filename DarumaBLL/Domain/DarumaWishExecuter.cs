using DarumaBLL.Common.Abstractions;

namespace DarumaBLL.Domain
{
    public class DarumaWishExecuter
    {
        private IDarumaStorage _storage;

        public DarumaWishExecuter(IDarumaStorage storage)
        {
            _storage = storage;
        }

        public bool TryExecuteWish(DarumaDomain daruma)
        {
            if (daruma.Status != DarumaStatus.MakedWish)
                return false;

            daruma.Status = DarumaStatus.ExecutedWish;

            _storage.Update(daruma);

            return true;
        }
    }
}
