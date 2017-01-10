using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.IO;
using WinEchek.Model;

namespace WinEchek
{
    public class SMTPLogger
    {
        private Container _container;
        private ISaver _saver = new BinarySaver();

        public SMTPLogger(Game game)
        {
            _container = game.Container;
            if (File.Exists("log.temp"))
            {
                int i = 0;
                while (File.Exists("crashlog_" + i + ".we")) i++;
                File.Copy("log.temp", "crashlog_" + i + ".we");

                Task.Run(() =>
                {
                    MailMessage msg = new MailMessage
                    {
                        From = new MailAddress("winechektechnic@gmail.com"),
                        Subject = "Crash report ",
                        Body =
                            DateTime.Now.ToString(CultureInfo.InvariantCulture) + '\n' +
                            Environment.MachineName,
                        Attachments = {new Attachment("crashlog_" + i + ".we")}
                    };
                    msg.To.Add("winechektechnic@gmail.com");
                    SmtpClient client = new SmtpClient
                    {
                        UseDefaultCredentials = true,
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential("winechektechnic@gmail.com", "winechek1234"),
                        Timeout = 20000
                    };
                    try
                    {
                        client.Send(msg);
                        return "Mail has been successfully sent!";
                    }
                    catch (Exception ex)
                    {
                        return "Fail Has error" + ex.Message;
                    }
                    finally
                    {
                        msg.Dispose();
                    }
                });
            }

            game.StateChanged += state => { _saver.Save(_container, "log.temp"); };
        }
    }
}