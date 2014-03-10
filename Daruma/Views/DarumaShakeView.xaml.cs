using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Daruma.Infrastructure;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.Helpers;
using DarumaDAL.WP.Abstraction;
using DarumaResourcesPortable.Infrastructure;
using DarumaResourcesPortable.LocalizationResources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using WPExtensions;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Daruma.Views
{
    public partial class DarumaShakeView : PhoneApplicationPage
    {
        private IDarumaStorage _darumaStorage;
        private IDarumaImageUriResolver _imageUriResolver;
        private IQuotationSource _quotationSource;
        private DarumaDomain _daruma;

        private string Quote
        {
            get
            {
                return CitationTextBlock.Text;
            }
        }

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
            var id = Guid.Parse(NavigationContext.QueryString["id"]);
            _daruma = await _darumaStorage.GetById(id);
            
            if (_daruma.HasCurrentQuoteKey && NavigationContext.QueryString.ContainsKey("hasKey"))
            {
                var quote = GetQuote(_daruma.Theme, _daruma.CurrentQuoteKey);
                FadeInQuotation(quote);
                ShowShareButton();
            }
            
            DataContext = _daruma;
            
            SetPinBar(NavigationService.Source.ToString());
            
            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
            base.OnBackKeyPress(e);
        }

        private string GetQuote(DarumaWishTheme theme, string key = null)
        {
            var quote = key == null
                ? _quotationSource.GetQuotationSourse(theme).Value
                : _quotationSource.GetQuotationByKey(theme, key);
            return quote;
        }

        private void FadeInQuotation(string quote)
        {
            GridQuoteTextBlock.Visibility = Visibility.Visible;
            GridQuoteTextBlock.Opacity = 0;
            CitationTextBlock.Text = quote;
            FadeInAnimation.Begin();
        }

        private void ShowShareButton()
        {
            ShareButton.Visibility = Visibility.Visible;
        }

        private void Share_OnClick(object sender, EventArgs eventArgs)
        {
            var quote = new DarumaInfoSharing(_daruma.Theme, Quote);
            PhoneApplicationService.Current.State.Add(DarumaInfoSharing.Key, quote);
            NavigationService.Navigate(new Uri(ViewUrlRouter.SharingViewUrl, UriKind.Relative));
        }

        private void HideShareButton()
        {
            ShareButton.Visibility = Visibility.Collapsed;
        }

        private void QuoteTextBlock_OnTap(object sender, GestureEventArgs e)
        {
            GridQuoteTextBlock.Visibility = Visibility.Collapsed;
            HideShareButton();
        }


        private async void Delete_OnClick(object sender, EventArgs eventArgs)
        {
            //TODO: for ViewModel support move this code to separate method
            var result = MessageBox.Show(string.Empty, AppResources.IsDelete, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var facade = new DarumaChangeStatusFacade(_imageUriResolver);
                facade.ChangeStatus(_daruma, DarumaStatus.TimeExpired);
                DarumaImg.Source = new BitmapImage(_daruma.ImageUri);
                await _darumaStorage.Delete(_daruma.Id);
                NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
            }
        }

        //TODO: move tile pin/unpin logic in separate class
        private void PinUnpin_OnClick(object sender, EventArgs eventArgs)
        {
            var url = ViewUrlRouter.DarumaShakeViewByIdUrl(_daruma.Id);

            if (IsTilePinned(url))
            {
                DeleteTile(url);
            }
            else
            {
                CreateTile();
            }

            SetPinBar(url);
        }

        private ShellTile GetTile(string url)
        {
            var tile = ShellTile.ActiveTiles.FirstOrDefault(sh => sh.NavigationUri.ToString().StartsWith(url));
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

        private async void CreateTile()
        {
            var tileIconUrl = _daruma.ImageUri;
            var url = ViewUrlRouter.DarumaShakeViewByIdUrlWithKey(_daruma.Id);
            var tiitleTheme = new DarumaWishThemeToLocalizationString().GetLocalizationByTheme(_daruma.Theme);
            
            var quoteSourse = _quotationSource.GetQuotationSourse(_daruma.Theme);

            var tileData = new StandardTileData()
            {
                Title = tiitleTheme,
                BackTitle = _daruma.Wish,
                BackContent = quoteSourse.Value,
                BackgroundImage = tileIconUrl
            };

            _daruma.CurrentQuoteKey = quoteSourse.Key;
            await _darumaStorage.Update(_daruma);
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

        private void CopyMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Quote);
        }

        private async void Daruma_OnTap(object sender, GestureEventArgs e)
        {
            if (_daruma.Status == DarumaStatus.MakedWish)
            {
                var result = MessageBox.Show(string.Empty, AppResources.IsWishComeTrue,
                    MessageBoxButton.OKCancel);
               
                if (result == MessageBoxResult.OK)
                {
                    var executer = new DarumaWishExecuter(_darumaStorage, _imageUriResolver);
                    var isExecute = await executer.TryExecuteWish(_daruma);
                    if (isExecute)
                    {
                        DarumaImg.Source = new BitmapImage(_daruma.ImageUri);    
                    }
                }
            }
        }

        private void GestureListener_OnFlick(object sender, FlickGestureEventArgs e)
        {
            if (e.HorizontalVelocity > 200 || e.HorizontalVelocity < -200)
            {
                DarumaAnimation.From = e.HorizontalVelocity / 36;
                DarumaStoryboard.Begin();

                var quote = GetQuote(_daruma.Theme);
                FadeInQuotation(quote);
                ShowShareButton();
            }
        }
    }
}