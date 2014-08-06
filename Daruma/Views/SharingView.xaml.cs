using System.Windows.Navigation;
using DarumaResourcesPortable.Infrastructure;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using DarumaDAL.WP.Infrastructure;

namespace Daruma.Views
{
    public partial class SharingView : PhoneApplicationPage
    {
        private DarumaInfoSharing _infoSharing;

        public SharingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey(DarumaInfoSharing.Key))
            {
                _infoSharing = PhoneApplicationService.Current.State[DarumaInfoSharing.Key] as DarumaInfoSharing;
            }
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
                Subject = s.GetLocalizationByTheme(_infoSharing.WishTheme),
                Body = _infoSharing.Quote
            };
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
    }
}