using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Daruma.ViewModels;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.ViewModels;
using Microsoft.Phone.Controls;

namespace Daruma.Views
{
    public partial class MainView : PhoneApplicationPage
    {
        private readonly ISettingsStorage _settings;
        private MainViewModel _viewModel;

        // Constructor
        public MainView() 
        {
            InitializeComponent();

            _settings = IoCContainter.Get<ISettingsStorage>();
            _viewModel = new MainViewModel(IoCContainter.Get<IDarumaStorage>(), IoCContainter.Get<IDarumaImageUriResolver>());
            DataContext = _viewModel;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void NewDaruma_OnTap(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.NewDarumaViewUrl, UriKind.Relative));
        }

        private void InfoDaruma_OnTap(object sender, EventArgs e)
        {
            NavigateToInfoPivotItem();
        }

        private void NavigateToInfoPivotItem()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.InfoDarumaViewUrl, UriKind.Relative));
        }

        private void InitializeSettings()
        {
            var handler = new FirstStartHandler(_settings);
            handler.HandleFirstStart(NavigateToInfoPivotItem);  
        }
        
        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeSettings();

        }
    }
}