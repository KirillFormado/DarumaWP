using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Daruma.Views
{
    public class BaseDarumaPage : PhoneApplicationPage
    {
        public BaseDarumaPage()
        {
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            SystemTray.SetIsVisible(this, false);
            TiltEffect.SetIsTiltEnabled(this, true);
        }
    }
}
