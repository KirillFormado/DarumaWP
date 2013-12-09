using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Daruma.Infrastructure;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

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