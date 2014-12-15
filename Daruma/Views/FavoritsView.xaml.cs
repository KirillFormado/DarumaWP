using System;
using System.Windows;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.ViewModels;
using DarumaDAL.WP.Infrastructure;
using Microsoft.Phone.Controls;

namespace Daruma.Views
{
    public partial class FavoritsView : BaseDarumaPage
    {
        private readonly FavoritsViewModel _viewModel;

        public FavoritsView()
        {
            InitializeComponent();
            _viewModel = new FavoritsViewModel(IoCContainter.Get<IFavoritApplicationService>());
            DataContext = _viewModel;
        }

        private async void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            string guid = ((MenuItem)sender).Tag.ToString();
            await _viewModel.Delete(guid);
        }

        private void Copy_OnClick(object sender, RoutedEventArgs e)
        {
            string quote = ((MenuItem)sender).Tag.ToString();
            Clipboard.SetText(quote);
        }

        private async void Share_OnClick(object sender, RoutedEventArgs e)
        {
            var favorit = ((MenuItem) sender).DataContext as FavoritViewObj;
            var infoSharing = new DarumaInfoSharing(favorit.Theme, favorit.Text);
            DarumaInfoSharing.SetQuote(infoSharing);
            NavigationService.Navigate(new Uri(ViewUrlRouter.SharingViewUrl, UriKind.Relative));
        }
    }
}