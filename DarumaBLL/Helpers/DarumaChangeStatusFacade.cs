using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;

namespace DarumaBLL.Helpers
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
