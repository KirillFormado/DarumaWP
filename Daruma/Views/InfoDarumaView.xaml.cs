using System;
using System.ComponentModel;
using System.Windows.Navigation;
using DarumaDAL.WP.Infrastructure;
using Microsoft.Phone.Controls;

namespace Daruma.Views
{
    public partial class InfoDarumaView : PhoneApplicationPage
    {
        public InfoDarumaView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Back();
            base.OnBackKeyPress(e);
        }

        private void Back_OnTap(object sender, EventArgs e)
        {
            Back();
        }

        private void Back()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }
    }
}