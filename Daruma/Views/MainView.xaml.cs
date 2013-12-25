using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Daruma.ViewModels;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using DarumaBLL.FirstStartUseCase;
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
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
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