using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Daruma.Infrastructure;
using Daruma.ViewModels;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.LocalizationResources;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Daruma.Views
{
    public partial class NewDarumaView : PhoneApplicationPage
    {
        private readonly IDarumaStorage _darumaStorage;

        public NewDarumaView()
        {
            InitializeComponent();
            _darumaStorage = IoCContainter.Get<IDarumaStorage>();
           
            var vm = new NewDarumaViewModel();
            DataContext = vm;
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

            DarumaWishTheme theme;
           
            Enum.TryParse(WishThemePicker.SelectedItem.ToString(), out theme);

            if (string.IsNullOrEmpty(wish) 
                || !Enum.IsDefined(typeof(DarumaWishTheme), theme) 
                || theme == DarumaWishTheme.NoSet)
            {
                MessageBox.Show(AppResources.EmptyWishTextBox);
                return;
            }
            
            //TODO: for ViewModel support move this code to separate method
            var daruma = CreateDaruma(wish, theme);
            await _darumaStorage.Add(daruma);
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }

        private DarumaDomain CreateDaruma(string wish, DarumaWishTheme theme)
        {            
            //TODO: refactor getting url addres for Daruma image
            var imageUrl = IoCContainter.Get<IDarumaImageUriResolver>().ResolveImageUri(DarumaStatus.MakedWish);
            return new DarumaDomain(wish, theme, imageUrl);
        }

        private void WishTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            //For user help
            WishTextBox.SelectAll();
        }
    }
}