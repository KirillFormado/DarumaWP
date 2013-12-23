using System;
using DarumaBLL.Common.Abstractions;

namespace DarumaBLL.Domain
{
    public class DarumaDomain
    {
        private IDarumaImageUriResolver _resolver;
        private DarumaStatus _status;

        public Guid Id { get; set; }
        public string Wish { get; set; }
        public DateTime CreateDate { get; set; }
        public DarumaStatus Status { 
            get { return _status; }
            set 
            {
                if (_status == value) 
                    return;
                
                ImageUri = _resolver.ResolveImageUri(value);
                _status = value;
            }
        }

        public Uri ImageUri { get; private set; }

        public DarumaDomain (string wish, IDarumaImageUriResolver resolver)
        {
            _resolver = resolver;
            Id = Guid.NewGuid();
            Wish = wish;
            CreateDate = DateTime.Now;
            Status = DarumaStatus.MakedWish;
        }
    }
}
