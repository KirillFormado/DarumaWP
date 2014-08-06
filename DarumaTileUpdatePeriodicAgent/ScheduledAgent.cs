#define DEBUG_AGENT
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using DarumaDAL.WP.Infrastructure;

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
            var darumaService = IoCContainter.Get<IDarumaApplicationService>();          

            var appTile = ShellTile.ActiveTiles.FirstOrDefault();
            if (appTile != null)
            {
                var quote = darumaService.GetQuotationForMainTile();
                var tileData = new StandardTileData()
                {
                    BackContent = quote
                };

                appTile.Update(tileData);
            }

            var secondaryTiles = ShellTile.ActiveTiles.Where(t => t.NavigationUri.ToString().Contains("id"));

            foreach (var shellTile in secondaryTiles)
            {
                var url = shellTile.NavigationUri.ToString();
            
                var guid = GetIdFromUrl(url);
                var result = await darumaService.GetInfoForSercondaryTile(guid);

                var data = new FlipTileData
                {
                    BackContent = result.Value,
                    BackgroundImage = result.Key.ImageUri
                };

                shellTile.Update(data);
            }

            NotifyComplete();
        }

        private Guid GetIdFromUrl(string url)
        {
            var idStr = "id=";
            var guidStartIndex = url.IndexOf(idStr) + idStr.Length;
            var guidLength = url.IndexOf("&") - guidStartIndex;
            var guidStr = url.Substring(guidStartIndex, guidLength);

            var guid = Guid.Parse(guidStr);
            return guid;
        }
    }
}