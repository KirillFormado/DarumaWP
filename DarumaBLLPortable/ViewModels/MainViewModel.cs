using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DarumaBLLPortable.Commands;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDarumaStorage _darumaStorage;
        private readonly IDarumaImageUriResolver _imageUriResolver;
        private readonly ISettingsStorage _settings;

        private Dictionary<DarumaStatus, ObservableCollection<DarumaDomain>> _darumaDict;
        private ObservableCollection<DarumaDomain> _darumaList= new ObservableCollection<DarumaDomain>();

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
            get { return _darumaList; }
        }

        public RelayCommand FirstSrartHandleCommand
        {
            get; private set;
        }

        public Action NavigateToInfoAction
        {
            get; set;
        }
        
        public MainViewModel(IDarumaStorage darumaStorage, IDarumaImageUriResolver imageUriResolver, ISettingsStorage settings) 
        {
            _darumaStorage = darumaStorage;
            _imageUriResolver = imageUriResolver;
            _settings = settings;
            _darumaDict = new Dictionary<DarumaStatus, ObservableCollection<DarumaDomain>>
            {
                {DarumaStatus.MakedWish, new ObservableCollection<DarumaDomain>()},
                {DarumaStatus.ExecutedWish, new ObservableCollection<DarumaDomain>()},
                {DarumaStatus.TimeExpired, new ObservableCollection<DarumaDomain>()},
            };
            LoadDaruma();
            InitCommands();
        }

        private void InitCommands()
        {
            FirstSrartHandleCommand = new RelayCommand(FirstSrartHandle);
        }

        private void FirstSrartHandle(object obj)
        {
            var handler = new FirstStartHandler(_settings);
            handler.HandleFirstStart(NavigateToInfoAction);  
        }

        private async void LoadDaruma()
        {
            var darumaList = //FakeDarumaSourse(); 
                await _darumaStorage.ListAll();
            var expiredHandler = new DarumaWishExpiredHandler(_darumaStorage, _imageUriResolver);
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
    }
}
