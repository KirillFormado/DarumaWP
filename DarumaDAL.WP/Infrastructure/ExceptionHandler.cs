using System.Windows;
using Microsoft.Phone.Tasks;

namespace DarumaDAL.WP.Infrastructure
{
    public class ExceptionHandler
    {
        public static void ExceptionEmail(ApplicationUnhandledExceptionEventArgs e)
        {
            //Email exception to developer
            var exceptionEmail = new EmailComposeTask
            {
                Body = e.ExceptionObject.ToString(),
                Subject = e.ExceptionObject.Message,
                To = "kirillformado@gmail.com"
            };

            exceptionEmail.Show();
        }
    }
}