using System;
using System.Text;
using System.Windows;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.ViewModels;
using DarumaDAL.WP.Infrastructure;
using DarumaResourcesPortable.Infrastructure;
using DarumaResourcesPortable.LocalizationResources;
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
            NavigateToSharingView(infoSharing);
        }

        private void NavigateToSharingView(DarumaInfoSharing infoSharing)
        {
            DarumaInfoSharing.SetQuote(infoSharing);
            NavigationService.Navigate(new Uri(ViewUrlRouter.SharingViewUrl, UriKind.Relative));
        }

        private void ShareList_OnClick(object sender, EventArgs e)
        {
            var text = new StringBuilder();

            foreach (FavoritViewObj favorit in _viewModel.Favorits)
            {
                text.AppendLine(AppResources.QuoteSeparator);
                text.AppendLine(favorit.Text);
            }

            NavigateToSharingView(new DarumaInfoSharing(text.ToString()));
        }
    }
}