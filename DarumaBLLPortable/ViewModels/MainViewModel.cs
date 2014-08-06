using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.Commands;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.ApplicationServices.Entites;

namespace DarumaBLLPortable.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISettingsStorage _settings;

        private Dictionary<DarumaStatus, ObservableCollection<DarumaView>> _darumaDict;
        private ObservableCollection<DarumaView> _darumaList = new ObservableCollection<DarumaView>();
        private IDarumaApplicationService _darumaService;

        public Dictionary<DarumaStatus, ObservableCollection<DarumaView>> DarumaDict
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

        public ObservableCollection<DarumaView> DarumaList
        {
            get { return _darumaList; }
        }

        public RelayCommand FirstStartHandleCommand
        {
            get; private set;
        }

        public Action NavigateToInfoAction
        {
            get; set;
        }

        public MainViewModel(ISettingsStorage settings, IDarumaApplicationService darumaService) 
        {
            _settings = settings;
            _darumaService = darumaService;
            _darumaDict = new Dictionary<DarumaStatus, ObservableCollection<DarumaView>>
            {
                {DarumaStatus.MakedWish, new ObservableCollection<DarumaView>()},
                {DarumaStatus.ExecutedWish, new ObservableCollection<DarumaView>()},
                {DarumaStatus.TimeExpired, new ObservableCollection<DarumaView>()},
            };
            LoadDaruma();
            InitCommands();
        }

        private void InitCommands()
        {
            FirstStartHandleCommand = new RelayCommand(FirstStartHandle);
        }

        private void FirstStartHandle(object obj)
        {
            var handler = new FirstStartHandler(_settings);
            handler.HandleFirstStart(NavigateToInfoAction);  
        }

        private async void LoadDaruma()
        {
            IEnumerable<DarumaView> darumaList = (await _darumaService.ListAll()).ToList();
            _darumaService.CheckExpiredStatus(darumaList);
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
                            _darumaDict.Add(status, new ObservableCollection<DarumaView>());
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
