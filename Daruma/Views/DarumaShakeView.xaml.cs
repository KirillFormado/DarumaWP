using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;
using DarumaResourcesPortable.Infrastructure;
using DarumaResourcesPortable.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Daruma.Views
{
    public partial class DarumaShakeView : PhoneApplicationPage
    {
        private IDarumaStorage _darumaStorage;
        private IDarumaImageUriResolver _imageUriResolver;
        private IQuotationSource _quotationSource;
        private DarumaDomain _daruma;

        public DarumaShakeView()
        {
            InitializeComponent();
            _darumaStorage = IoCContainter.Get<IDarumaStorage>();
            _imageUriResolver = IoCContainter.Get<IDarumaImageUriResolver>();
            _quotationSource = IoCContainter.Get<IQuotationSource>();
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

            GridCitationTextBlock.Visibility = Visibility.Visible;
            GridCitationTextBlock.Opacity = 0;
            CitationTextBlock.Text = _quotationSource.GetCitationSourse(_daruma.Theme);
            FadeInAnimation.Begin();
        }

        private double GetPosition(MouseEventArgs e)
        {
            var position = e.GetPosition(ContentPanel);
            return position.X;
        }

        private void CitationTextBlock_OnTap(object sender, GestureEventArgs e)
        {
            GridCitationTextBlock.Visibility = Visibility.Collapsed;
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

            var tileIconUrl = _daruma.ImageUri;
            var tiitleTheme = new DarumaWishThemeToLocalizationString().GetLocalizationByTheme(_daruma.Theme);

            var tileData = new StandardTileData()
            {
                Title = tiitleTheme,
                BackTitle = _daruma.Wish,
                BackContent = _quotationSource.GetCitationSourse(_daruma.Theme),
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

        private void Daruma_OnTap(object sender, GestureEventArgs e)
        {
            if (_daruma.Status == DarumaStatus.MakedWish)
            {
                var result = MessageBox.Show(string.Empty, AppResources.IsWishComeTrue,
                    MessageBoxButton.OKCancel);
               
                if (result == MessageBoxResult.OK)
                {
                    var executer = new DarumaWishExecuter(_darumaStorage, _imageUriResolver);
                    var isExecute = executer.TryExecuteWish(_daruma);
                    if (isExecute)
                    {
                        DarumaImg.Source = new BitmapImage(_daruma.ImageUri);    
                    }
                }
            }
        }
    }
}