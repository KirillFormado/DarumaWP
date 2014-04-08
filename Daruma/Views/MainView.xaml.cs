using System;
using System.ComponentModel;
using System.Windows;
using Daruma.Infrastructure;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.ViewModels;
using Microsoft.Phone.Controls;

namespace Daruma.Views
{
    public partial class MainView : PhoneApplicationPage
    {
        private MainViewModel _viewModel;

        // Constructor
        public MainView() 
        {
            InitializeComponent();
            InitializeViewModel();
            DataContext = _viewModel;
        }

        private void InitializeViewModel()
        {
            _viewModel = new MainViewModel(IoCContainter.Get<IDarumaStorage>()
                , IoCContainter.Get<IDarumaImageUriResolver>()
                , IoCContainter.Get<ISettingsStorage>());
            //initialize commands
            _viewModel.NavigateToInfoAction = NavigateToInfoPivotItem;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void NewDaruma_OnTap(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.NewDarumaViewUrl, UriKind.Relative));
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.FirstSrartHandleCommand.Execute(null);
        }

        private void InfoDaruma_OnTap(object sender, EventArgs e)
        {
            NavigateToInfoPivotItem();
        }

        private void NavigateToInfoPivotItem()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.InfoDarumaViewUrl, UriKind.Relative));
        }
    }
}