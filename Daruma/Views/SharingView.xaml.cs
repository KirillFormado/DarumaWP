using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.Infrastructure;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using DarumaDAL.WP.Infrastructure;

namespace Daruma.Views
{
    public partial class SharingView : BaseDarumaPage
    {
        private DarumaInfoSharing _infoSharing;

        public SharingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _infoSharing = DarumaInfoSharing.GetQuote();
            base.OnNavigatedTo(e);
        }

        private void Messages_OnTap(object sender, GestureEventArgs e)
        {
            var shareStatusTask = new SmsComposeTask
            {
                Body = _infoSharing.Quote
            };
            shareStatusTask.Show();
        }

        private void Mail_OnTap(object sender, GestureEventArgs e)
        {
            var s = new DarumaWishThemeToLocalizationString();
            var shareStatusTask = new EmailComposeTask
            {
                Body = _infoSharing.Quote
            };

            if (_infoSharing.WishTheme != DarumaWishTheme.NoSet)
            {
                shareStatusTask.Subject = s.GetLocalizationByTheme(_infoSharing.WishTheme);
            }

            //new ShareStatusTask { Status = Quote };
            shareStatusTask.Show();
        }

        private void SocialNetwork_OnTap(object sender, GestureEventArgs e)
        {
            var shareStatusTask = new ShareStatusTask
            {
                Status = _infoSharing.Quote
            };

            shareStatusTask.Show();
        }

        private void ToClipboard_OnTap(object sender, GestureEventArgs e)
        {
            Clipboard.SetText(_infoSharing.Quote);
            NavigationService.GoBack();
        }
    }
}