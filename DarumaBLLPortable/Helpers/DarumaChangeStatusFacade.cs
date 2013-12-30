using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Helpers
{
    class DarumaChangeStatusFacade
    {
        private IDarumaImageUriResolver _urlResolver;

        public DarumaChangeStatusFacade(IDarumaImageUriResolver urlResolver)
        {
            _urlResolver = urlResolver;
        }

        public void ChangeStatus(DarumaDomain daruma, DarumaStatus status)
        {
            var url = _urlResolver.ResolveImageUri(status);
            daruma.ChangeStatus(status, url);
        }
    }
}
