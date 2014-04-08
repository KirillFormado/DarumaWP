using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ViewModels
{
    public class DarumaShakeViewModel : ViewModelBase
    {
        private DarumaDomain _daruma;
        private IDarumaStorage _darumaStorage;

        public DarumaShakeViewModel(IDarumaStorage darumaStorage)
        {
            _darumaStorage = darumaStorage;
        }

        public DarumaDomain Daruma
        {
            get
            {
                return _daruma;
            }
            set
            {
                _daruma = value;
                OnPropertyChanged();
            }
        }


    }
}
