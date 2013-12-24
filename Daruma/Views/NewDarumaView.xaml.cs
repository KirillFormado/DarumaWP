using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Daruma.Infrastructure;
using Daruma.Resources;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using Microsoft.Phone.Controls;

namespace Daruma.Views
{
    public partial class NewDarumaView : PhoneApplicationPage
    {
        private readonly IDarumaStorage _darumaStorage;

        public NewDarumaView()
        {
            InitializeComponent();
            _darumaStorage = IoCContainter.Get<IDarumaStorage>();
        }

        private void WishDaruma_OnTap(object sender, GestureEventArgs e)
        {
            WishStack.Visibility =
                WishStack.Visibility == Visibility.Visible
                    ? Visibility.Collapsed
                    : Visibility.Visible;

            StackOpacityStoryboard.Begin();
            NewDarumaImg.Source = new BitmapImage(new Uri(ImageUrlRouter.WishedDaurmaImageUrl, UriKind.Relative));
        }

        private async void WishButton_OnTap(object sender, GestureEventArgs e)
        {
            var wish = WishTextBox.Text;
            if (string.IsNullOrEmpty(wish))
            {
                MessageBox.Show(AppResources.EmptyWishTextBox);
                return;
            }

            //TODO: for ViewModel support move this code to separate method
            var daruma = CreateDaruma(wish);
            await _darumaStorage.Add(daruma);
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }

        private DarumaDomain CreateDaruma(string wish)
        {            
            //TODO: refactor getting url addres for Daruma image
            return new DarumaDomain(wish, IoCContainter.Get<IDarumaImageUriResolver>().ResolveImageUri(DarumaStatus.MakedWish));
        }

        private void WishTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            //For user help
            WishTextBox.SelectAll();
        }
    }
}