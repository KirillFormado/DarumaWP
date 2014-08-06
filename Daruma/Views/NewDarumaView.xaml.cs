using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.ViewModels;
using DarumaDAL.WP.Infrastructure;
using DarumaResourcesPortable.LocalizationResources;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Daruma.Views
{
    public partial class NewDarumaView : PhoneApplicationPage
    {
        private NewDarumaViewModel _viewModel;
        
        public NewDarumaView()
        {
            InitializeComponent();
            _viewModel = new NewDarumaViewModel(IoCContainter.Get<IDarumaApplicationService>());
            DataContext = _viewModel;
        }

        private void WishDaruma_OnTap(object sender, GestureEventArgs e)
        {
            if (WishStack.Visibility == Visibility.Visible)
            {
                WishStack.Visibility = Visibility.Collapsed;
                NewDarumaImg.Source = new BitmapImage(new Uri(ImageUrlRouter.NewDaurmaImageUrl, UriKind.Relative));
            }
            else
            {
                WishStack.Visibility = Visibility.Visible;
                NewDarumaImg.Source = new BitmapImage(new Uri(ImageUrlRouter.WishedDaurmaImageUrl, UriKind.Relative));
            }
        }

        private async void WishButton_OnTap(object sender, GestureEventArgs e)
        {
            var wish = WishTextBox.Text;

            DarumaWishTheme theme;
           
            Enum.TryParse(WishThemePicker.SelectedItem.ToString(), out theme);

            if (string.IsNullOrEmpty(wish) 
                || !Enum.IsDefined(typeof(DarumaWishTheme), theme) 
                || theme == DarumaWishTheme.NoSet)
            {
                MessageBox.Show(AppResources.EmptyWishTextBox);
                WishTextBox.Focus();
                return;
            }

            await _viewModel.CreateDaruma(wish, theme);
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }

        private void WishTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            //For user help
            WishTextBox.SelectAll();
        }
    }
}