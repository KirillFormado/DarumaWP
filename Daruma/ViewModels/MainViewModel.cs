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
            get { return new ObservableCollection<DarumaDomain>(FakeDarumaSourse()); }
        }
        
        public MainViewModel() 
        {
            _darumaStorage = IoCContainter.Get<IDarumaStorage>();
            _darumaDict = new Dictionary<DarumaStatus, ObservableCollection<DarumaDomain>>();
            LoadDaruma();            
        }


        private void LoadDaruma()
        {
            var darumaList = FakeDarumaSourse(); //await _darumaStorage.ListAllAsync(); 
            var lookup = darumaList.ToLookup(d => d.Status);

            var orderList = GetOrderList();

            foreach (var status in orderList)
            {
                var observList = new ObservableCollection<DarumaDomain>(lookup[status]);
                if(observList.Count > 0)
                    _darumaDict.Add(status, observList);
            }
        }

        private IEnumerable<DarumaStatus> GetOrderList()
        {
            return new List<DarumaStatus>
            {
                DarumaStatus.MakedWish,
                DarumaStatus.TimeExpired,
                DarumaStatus.ExecutedWish,
                DarumaStatus.Empty
            };
        }
        

        private IEnumerable<DarumaDomain> FakeDarumaSourse()
        {
            var resolver = IoCContainter.Get<IDarumaImageUriResolver>();
            var list = new List<DarumaDomain>
            {
                new DarumaDomain("aaaa", resolver),
                new DarumaDomain("bbbb", resolver),
                new DarumaDomain("ccccccccccccccccccccccccccccccccc", resolver),
                new DarumaDomain("dddd", resolver),
                new DarumaDomain("eeee", resolver)
            };

            list[0].Status = DarumaStatus.Empty;
            list[1].Status = DarumaStatus.ExecutedWish;
            list[2].Status = DarumaStatus.TimeExpired;

            return list;
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
