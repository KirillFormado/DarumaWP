using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Daruma.Infrastructure.Localization;
using Daruma.Infrastructure.Storages;
using Daruma.Resources;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using Microsoft.Phone.Controls;
using DarumaBLL.RandomCitationUseCase;
using Microsoft.Phone.Shell;
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

            DataContext = _daruma;

            SetPinBar(NavigationService.Source.ToString());
            
            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
            base.OnBackKeyPress(e);
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

        private void PinUnpin_OnClick(object sender, EventArgs e)
        {
            var url = ViewUrlRouter.DarumaShakeViewByIdUrl(_daruma.Id);

            if (IsTilePinned(url))
            {
                DeleteTile(url);
            }
            else
            {
                CreateTile(url);
            }

            SetPinBar(url);
        }

        private ShellTile GetTile(string url)
        {
            var tile = ShellTile.ActiveTiles.FirstOrDefault(sh => sh.NavigationUri.ToString() == url);
            return tile;
        }

        private bool IsTilePinned(string url)
        {
            var tile = GetTile(url);
            return tile != null;
        }

        private void DeleteTile(string url)
        {
            var tile = GetTile(url);
            tile.Delete();
        }

        private void CreateTile(string url)
        {
            var tileIconUrl = new Uri(TilesUrlRouter.DarumaSecondaryTileImageUrl, UriKind.Relative);
            var tiitleTheme = new DarumaWishThemeToLocalizationString().GetLocalizationByTheme(_daruma.Theme);

            var tileData = new StandardTileData()
            {
                Title = tiitleTheme,
                BackTitle = _daruma.Wish,
                BackContent = GetCitationSourse(),
                BackgroundImage = tileIconUrl
            };

            ShellTile.Create(new Uri(url, UriKind.Relative), tileData);
        }

        private void SetPinBar(string url)
        {
            if (IsTilePinned(url))
            {
                PinUnpinButton.Text = AppResources.Unpin;
                PinUnpinButton.IconUri = new Uri(IconsUrlRouter.UnpinIconUrl, UriKind.Relative);
            }
            else
            {
                PinUnpinButton.Text = AppResources.Pin;
                PinUnpinButton.IconUri = new Uri(IconsUrlRouter.PinIconUrl, UriKind.Relative);
            }
        }
    }
}