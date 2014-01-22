#define DEBUG_AGENT
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Resources;
using System.Windows;
using System.Windows.Navigation;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP;
using DarumaDAL.WP.Storages;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;


namespace DarumaTileUpdatePeriodicAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            var quotationSource = new QuotationSource();
            var storage = new DarumaStorage();
            var appTile = ShellTile.ActiveTiles.FirstOrDefault();
            if (appTile != null)
            {
                var tileData = new StandardTileData()
                {
                    BackContent = quotationSource.GetCitationSourse(DarumaWishTheme.NoSet)
                };

                appTile.Update(tileData);
            }

            var secondaryTiles = ShellTile.ActiveTiles.Where(t => t.NavigationUri.ToString().Contains("id"));

            foreach (var shellTile in secondaryTiles)
            {
                var url = shellTile.NavigationUri.ToString();
                var str = "id=";
                var guidStr = url.Substring(url.IndexOf(str) + str.Length);
                var guid = Guid.Parse(guidStr);
                var daruma = await storage.GetById(guid);
                
                var data = new FlipTileData
                {
                    BackContent = quotationSource.GetCitationSourse(daruma.Theme),
                    BackgroundImage = daruma.ImageUri
                };

                shellTile.Update(data);
            }

#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(10));
#endif

            NotifyComplete();
        }
    }
}