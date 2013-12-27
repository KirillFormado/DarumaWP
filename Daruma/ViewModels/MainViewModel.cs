using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Daruma.Annotations;
using Daruma.Infrastructure;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;

namespace Daruma.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDarumaStorage _darumaStorage;
        private Dictionary<DarumaStatus, ObservableCollection<DarumaDomain>> _darumaDict;

        public Dictionary<DarumaStatus, ObservableCollection<DarumaDomain>> DarumaDict
        {
            get { return _darumaDict; }
            set
            {
                if (_darumaDict != value)
                {
                    _darumaDict = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<DarumaDomain> DarumaList
        {
            get { return new ObservableCollection<DarumaDomain>(); }
        }
        
        public MainViewModel() 
        {
            _darumaStorage = IoCContainter.Get<IDarumaStorage>();
            _darumaDict = new Dictionary<DarumaStatus, ObservableCollection<DarumaDomain>>
            {
                {DarumaStatus.MakedWish, new ObservableCollection<DarumaDomain>()},
                {DarumaStatus.ExecutedWish, new ObservableCollection<DarumaDomain>()},
                {DarumaStatus.TimeExpired, new ObservableCollection<DarumaDomain>()},
            };
            LoadDaruma();            
        }


        private async void LoadDaruma()
        {
            var darumaList = //FakeDarumaSourse(); 
                await _darumaStorage.ListAll();
            var expiredHandler = new DarumaWishExpiredHandler(_darumaStorage, new DarumaImageUriResolver());
            expiredHandler.CheckExpiredStatus(darumaList);
            var lookup = darumaList.ToLookup(d => d.Status);

            var orderList = GetOrderList();

            foreach (var status in orderList)
            {
                var observList = lookup[status].ToList();
                if (observList.Any())
                {
                    foreach (var darumaDomain in observList)
                    {
                        if (!_darumaDict.ContainsKey(status))
                        {
                            _darumaDict.Add(status, new ObservableCollection<DarumaDomain>());
                        }
                        _darumaDict[status].Add(darumaDomain);
                    }
                }
            }
        }

        private IEnumerable<DarumaStatus> GetOrderList()
        {
            return new List<DarumaStatus>
            {
                DarumaStatus.MakedWish,
                DarumaStatus.TimeExpired,
                DarumaStatus.ExecutedWish,
                //DarumaStatus.Empty
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
