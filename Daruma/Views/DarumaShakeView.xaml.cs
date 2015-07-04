using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Phone.Devices.Notification;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ViewModels;
using DarumaResourcesPortable.Infrastructure;
using DarumaResourcesPortable.LocalizationResources;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using DarumaDAL.WP.Infrastructure;
using DarumaBLLPortable.ApplicationServices.Entites;
using ShakeGestures;

namespace Daruma.Views
{
    public partial class DarumaShakeView : BaseDarumaPage
    {
        private DarumaShakeViewModel _viewModel;
        private ShakeGesturesHelper _shake;

        private string Quote
        {
            get
            {
                return CitationTextBlock.Text;
            }
        }

        private string DarumaUrl
        {
            get
            {
                var url = ViewUrlRouter.DarumaShakeViewByIdUrl(Daruma.Id);
                return url;
            }
        }

        public DarumaView Daruma
        {
            get
            {
                return _viewModel.Daruma;
            }
        }

        public DarumaShakeView()
        {
            InitializeComponent();
            _viewModel = new DarumaShakeViewModel(IoCContainter.Get<IDarumaApplicationService>(), IoCContainter.Get<IFavoritApplicationService>());
            DataContext = _viewModel;
            _shake = ShakeGesturesHelper.Instance;
            _shake.MinimumRequiredMovesForShake = 3;
            _shake.MinimumShakeVectorsNeededForShake = 20;
            _shake.StillMagnitudeWithoutGravitationThreshold = 1;
            _shake.ShakeGesture += OnShakeOnShakeGesture;
            _shake.Active = true;
        }

        private void OnShakeOnShakeGesture(object sender, ShakeGestureEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                VibrationDevice.GetDefault().Vibrate(TimeSpan.FromSeconds(0.5));
                ShakeDaruma(2000);
            });
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Get Daruma by id from storage
            var id = Guid.Parse(NavigationContext.QueryString["id"]);
            await _viewModel.GetDarumaById(id);
            
            if (Daruma.HasCurrentQuoteKey && NavigationContext.QueryString.ContainsKey("hasKey"))
            {
                var quote =  _viewModel.GetQuote(Daruma.Theme, Daruma.CurrentQuoteKey);
                FadeInQuotation(quote);
                ShowButtons();
            }
            
            DataContext = Daruma;
            
            SetPinBar(NavigationService.Source.ToString());
            
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _shake.Active = false;
            base.OnNavigatedFrom(e);
        }

        //protected override void OnBackKeyPress(CancelEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        //    base.OnBackKeyPress(e);
        //}

        private void FadeInQuotation(string quote)
        {
            GridQuoteTextBlock.Visibility = Visibility.Visible;
            GridQuoteTextBlock.Opacity = 0;
            CitationTextBlock.Text = quote;
            FadeInAnimation.Begin();
        }

        private void ShowButtons()
        {
            HomeButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
            PinUnpinButton.Visibility = Visibility.Collapsed;

            ShareButton.Visibility = Visibility.Visible;
            AddToFavoritQuotButton.Visibility = Visibility.Visible;
            CloseQuotButton.Visibility = Visibility.Visible;
        }

        private void Share_OnClick(object sender, EventArgs eventArgs)
        {
            var quote = new DarumaInfoSharing(Daruma.Theme, Quote);
            DarumaInfoSharing.SetQuote(quote);
            
            NavigationService.Navigate(new Uri(ViewUrlRouter.SharingViewUrl, UriKind.Relative));
        }

        private void HideButtons()
        {
            GridQuoteTextBlock.Visibility = Visibility.Collapsed;

            ShareButton.Visibility = Visibility.Collapsed;
            AddToFavoritQuotButton.Visibility = Visibility.Collapsed;
            CloseQuotButton.Visibility = Visibility.Collapsed;

            HomeButton.Visibility = Visibility.Visible;
            PinUnpinButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        //private void QuoteTextBlock_OnTap(object sender, GestureEventArgs e)
        //{
        //    //GridQuoteTextBlock.Visibility = Visibility.Collapsed;
        //    HideButtons();
        //}


        private async void Delete_OnClick(object sender, EventArgs eventArgs)
        {
            //TODO: for ViewModel support move this code to separate method
            var result = MessageBox.Show(string.Empty, AppResources.IsDelete, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                await _viewModel.ChangeToTimeExpired();
                DarumaImg.Source = new BitmapImage(Daruma.ImageUri);

                bool isDelete = await _viewModel.Delete(Daruma);

                if (isDelete)
                {
                    DeleteTile(DarumaUrl);
                    NavigateToMain();
                }
            }
        }

        private void NavigateToMain()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }

        //TODO: move tile pin/unpin logic in separate class
        private async void PinUnpin_OnClick(object sender, EventArgs eventArgs)
        {
            var url = DarumaUrl;

            if (IsTilePinned(url))
            {
                DeleteTile(url);
            }
            else
            {
                await CreateTile();
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
            if (tile != null)
            {
                tile.Delete();
            }
        }

        private async Task CreateTile()
        {
            var tileIconUrl = Daruma.ImageUri;
            var url = ViewUrlRouter.DarumaShakeViewByIdUrlWithKey(Daruma.Id);
            var tiitleTheme = new DarumaWishThemeToLocalizationString().GetLocalizationByTheme(Daruma.Theme);

            string quote = await _viewModel.UpdateDarumaQuotation();           

            var tileData = new StandardTileData()
            {
                Title = tiitleTheme,
                BackTitle = Daruma.Wish,
                BackContent = quote,
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

        private void CopyMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Quote);
        }

        private async void Daruma_OnTap(object sender, GestureEventArgs e)
        {          
            await _viewModel.TryExecuteWish(() =>
            {                       
                MessageBoxResult result = MessageBox.Show(string.Empty, AppResources.IsWishComeTrue, MessageBoxButton.OKCancel);
                return result == MessageBoxResult.OK;
            });

            DarumaImg.Source = new BitmapImage(_viewModel.Daruma.ImageUri);
        }

        private void GestureListener_OnFlick(object sender, FlickGestureEventArgs e)
        {
            ShakeDaruma(e.HorizontalVelocity);
        }

        private void ShakeDaruma(double horizontalVelocity)
        {
            if (horizontalVelocity > 10 || horizontalVelocity < -10)
            {
                DarumaAnimation.From = horizontalVelocity/36;
                DarumaStoryboard.Begin();

                var quote = _viewModel.GetQuote(Daruma.Theme);
                FadeInQuotation(quote);
                
                ShowButtons();
            }
        }

        private void AddToFavoritQuot_OnClick(object sender, EventArgs eventArgs)
        {
            _viewModel.AddFavorit(Quote);
            HideButtons();
        }

        private void Close_OnClick(object sender, EventArgs eventArgs)
        {
            HideButtons();
        }

        private void Refresh_OnClick(object sender, EventArgs e)
        {
            ShakeDaruma(2000);
        }
        
        private void Home_OnClick(object sender, EventArgs e)
        {
            NavigateToMain();
        }
    }
}