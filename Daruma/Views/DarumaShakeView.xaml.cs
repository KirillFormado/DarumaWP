using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Daruma.Resources;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using Microsoft.Phone.Controls;
using DarumaBLL.RandomCitationUseCase;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var id = Guid.Parse(NavigationContext.QueryString["id"]);
            _daruma = _darumaStorage.GetById(id);

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
            var resourceSet = CitationLocalizationResources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            return new RandomCitationResolver(resourceSet).RenturnRandomCitation();
        }

        private void CitationTextBlock_OnTap(object sender, GestureEventArgs e)
        {
            CitationTextBlock.Visibility = Visibility.Collapsed;
        }

        private void Delete_OnClick(object sender, EventArgs e)
        {
            _darumaStorage.Delete(_daruma.Id);
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }
    }
}