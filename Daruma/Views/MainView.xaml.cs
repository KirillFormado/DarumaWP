using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Daruma.ViewModels;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using DarumaBLL.FirstStartUseCase;
using Microsoft.Phone.Controls;

namespace Daruma.Views
{
    public partial class MainView : PhoneApplicationPage
    {
        private readonly ISettingsStorage _settings;
        private MainViewModel _viewModel;

        // Constructor
        public MainView() 
        {
            InitializeComponent();

            _settings = IoCContainter.Get<ISettingsStorage>();
            //InitializeSettings();
            var vm = new MainViewModel();
            DataContext = vm;
        }

        private void NewDaruma_OnTap(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.NewDarumaViewUrl, UriKind.Relative));
        }

        private void InfoDaruma_OnTap(object sender, EventArgs e)
        {
            NavigateToInfoPivotItem();
        }

        private void NavigateToInfoPivotItem()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.InfoDarumaViewUrl, UriKind.Relative));
        }

        private void InitializeSettings()
        {
            var handler = new FirstStartHandler(_settings);
            handler.HandleFirstStart(NavigateToInfoPivotItem);  
        }

        //private void DarumaContainer_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    LoadDaruma();
        //    //BindDaruma();
        //}

        //private void BindDaruma()
        //{
        //    if(_darumaList == null)
        //        return;
            
        //    var dd = _darumaList.ToLookup(d => d.Status);

        //    foreach (var gr in dd)
        //    {
        //        var key = gr.Key;
        //        ViewDarumaList(gr.ToList());
        //    }
            
        //    //var exList = _darumaList.Where(d => d.Status == DarumaStatus.ExecutedWish);
        //    //if (exList.Any())
        //    //{
        //    //    MainPivot.Items.Add(new PivotItem() {Header = "Executed Wish"});
        //    //}
        //}

        //private void ViewDarumaList(IEnumerable<DarumaDomain> darumaList)
        //{
        //    DarumaStackPanel.Children.Clear();
        //    foreach (var daruma in darumaList)
        //    {
        //        var grid = new Grid();
        //        //grid.Children.Add(CreateTextBlock(daruma));
        //        grid.Children.Add(CreateImage(daruma));
        //        grid.Children.Add(CreateHyperlinkButton(daruma));
        //        DarumaStackPanel.Children.Add(grid);
        //    }
        //}
       
        //private Image CreateImage(DarumaDomain daruma)
        //{
        //    var img = new Image
        //    {
        //        Height = 100,
        //        Width = 100,
        //        Margin = (Thickness) Resources["MarginTemplate"],
        //        Source = new BitmapImage(daruma.ImageUri)
        //    };

        //    return img;
        //}

        //private HyperlinkButton CreateHyperlinkButton(DarumaDomain daruma)
        //{
        //    var hlb = new HyperlinkButton();

        //    var url = string.Format("{0}?id={1}",
        //        ViewUrlRouter.DarumaShakeViewUrl,
        //        daruma.Id);

        //    hlb.NavigateUri = new Uri(url, UriKind.Relative);
        //    hlb.Opacity = 0;

        //    return hlb;
        //}
        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeSettings();
        }
    }
}