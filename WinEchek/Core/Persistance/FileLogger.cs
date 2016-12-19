using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using WinEchek.Model;

namespace WinEchek.Core.Persistance
{
    class FileLogger
    {
        private Container _container;
        private ISaver _saver = new BinarySaver();

        public FileLogger(Game game)
        {
            _container = game.Container;
            if (File.Exists("log.temp"))
            {
                //TODO file handling
                int i = 0;
                while (File.Exists("crashlog" + i)) i++;
                File.Copy("log.temp", "crashlog_" + i + ".we");
            }

            game.StateChanged += state =>
            {
                _saver.Save(_container, "log.temp");
            };
        }

        public async void SendMailAsync()
        {
            MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            SmtpClient client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.google.com"
            };
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            Task.Run(client.Send(mail));
        }
    }
}