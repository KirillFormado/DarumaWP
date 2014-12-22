﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.ViewModels;
using DarumaDAL.WP.Infrastructure;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Daruma.Views
{
    public partial class MainView : BaseDarumaPage
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
            _viewModel = new MainViewModel(IoCContainter.Get<ISettingsStorage>(), IoCContainter.Get<IDarumaApplicationService>(), NavigateToNewDaruma);
            //initialize commands
            _viewModel.NavigateToInfoAction = NavigateToInfoPivotItem;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //in WinRT apps, app go to sleep after back button push insted exit app
            NavigationService.PauseOnBack = true;
            //Application.Current.Terminate();
        }

        private void NewDaruma_OnTap(object sender, EventArgs e)
        {
            NavigateToNewDaruma();
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.FirstStartHandleCommand.Execute(null);
            //_viewModel.CheckDarumaList(NavigateToNewDaruma);
        }

        private void InfoDaruma_OnTap(object sender, EventArgs e)
        {
            NavigateToInfoPivotItem();
        }

        private void NavigateToInfoPivotItem()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.InfoDarumaViewUrl, UriKind.Relative));
        }

        private void Favorits_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.FavoritsViewUrl, UriKind.Relative));
        }

        private void NavigateToNewDaruma()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.NewDarumaViewUrl, UriKind.Relative));
        }

        private void Review_OnClick(object sender, EventArgs e)
        {
            var marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
    }
}