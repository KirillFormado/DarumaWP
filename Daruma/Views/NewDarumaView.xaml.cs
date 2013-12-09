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

        private void WishButton_OnTap(object sender, GestureEventArgs e)
        {
            var wish = WishTextBox.Text;
            if (string.IsNullOrEmpty(wish))
            {
                MessageBox.Show(AppResources.EmptyWishTextBox);
                return;
            }

            var daruma = CreateDaruma(wish);
            SaveNewWishDaruma(daruma);
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }

        private DarumaDomain CreateDaruma(string wish)
        {            
            return new DarumaDomain(wish, IoCContainter.Get<IDarumaImageUriResolver>());
        }

        private void SaveNewWishDaruma(DarumaDomain daruma)
        {
            _darumaStorage.Add(daruma);
        }

        private void WishTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            //For user help
            WishTextBox.SelectAll();
        }
    }
}