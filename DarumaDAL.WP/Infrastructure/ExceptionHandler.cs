using System.Text;
using System.Windows;
using DarumaBLLPortable.Common.Abstractions;
using DarumaDAL.WP.Storages;
using Microsoft.Phone.Tasks;

namespace DarumaDAL.WP.Infrastructure
{
    public class ExceptionHandler
    {
        private const string _splitter = "=========================";

        public static void ExceptionEmail(ApplicationUnhandledExceptionEventArgs e)
        {
            
        }

        public async static void SendData()
        {
            var sb = new StringBuilder();
            var storage = (DarumaStorage)IoCContainter.Get<IDarumaStorage>();
            var list = await storage.ListAllString();
            foreach (var text in list)
            {
                sb.AppendLine(text);
                sb.AppendLine(_splitter);
            }

            //Email exception to developer
            var exceptionEmail = new EmailComposeTask
            {
                Body = sb.ToString(),
                To = "kirillformado@gmail.com"
            };

            exceptionEmail.Show();
        }
    }
}