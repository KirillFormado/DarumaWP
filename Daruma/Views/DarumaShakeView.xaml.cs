﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Daruma.Infrastructure.Storages;
using Daruma.Resources;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using Microsoft.Phone.Controls;
using DarumaBLL.RandomCitationUseCase;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Daruma.Views
{
    public partial class DarumaShakeView : PhoneApplicationPage
    {
        private IDarumaStorage _darumaStorage;
        private DarumaDomain _daruma;

        public DarumaShakeView()
        {
            InitializeComponent();
            _darumaStorage = IoCContainter.Get<IDarumaStorage>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Get Daruma by id from storage
            //TODO: fill info from Daruma to page, now only Image update
            var id = Guid.Parse(NavigationContext.QueryString["id"]);
            _daruma = await _darumaStorage.GetById(id);
            
            base.OnNavigatedTo(e);
        }

        private double _prevX;
        private const double From = 75;

        private void Daruma_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _prevX = GetPosition(e);
        }

        private void Daruma_OnMouseMove(object sender, MouseEventArgs e)
        {
            var currX = GetPosition(e);
            var result = currX - _prevX;
            double from = result > 0 ? From : -From;
            DarumaAnimation.From = from;
            DarumaStoryboard.Begin();

            CitationTextBlock.Visibility = Visibility.Visible;
            CitationTextBlock.Opacity = 0;
            CitationTextBlock.Text = GetCitationSourse();
            FadeInAnimation.Begin();
        }

        private double GetPosition(MouseEventArgs e)
        {
            var position = e.GetPosition(ContentPanel);
            return position.X;
        }

        private string GetCitationSourse()
        {
            var resourseStorage = new LocalizationResourseStorage();
            var resourceSet = resourseStorage.GetByDarumsWishTheme(_daruma.Theme);
            var quaotation = new RandomCitationResolver(resourceSet).RenturnRandomCitation();
            return quaotation;
        }

        private void CitationTextBlock_OnTap(object sender, GestureEventArgs e)
        {
            CitationTextBlock.Visibility = Visibility.Collapsed;
        }

        private async void Delete_OnClick(object sender, EventArgs e)
        {
            //TODO: for ViewModel support move this code to separate method
            await _darumaStorage.Delete(_daruma.Id);
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }
    }
}