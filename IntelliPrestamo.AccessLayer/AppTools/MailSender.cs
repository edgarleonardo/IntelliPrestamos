using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.AppTools
{
    public class MailSender
    {
        private string Server = "";
        private string user = "";
        private string pass = "";
        private int port = 0;

        public MailSender(string server, string user, string pass, int port)
        {
            this.Server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
        }

        public void SendMail(string body, string subject, string email, string mailFrom)
        {
            string to = email;
            string from = mailFrom;
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            SmtpClient client = new SmtpClient(Server);
            client.Port = port;
            // Credentials are necessary if the server requires the client  
            // to authenticate before it will send e-mail on the client's behalf.
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(user, pass);
            //client.EnableSsl = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
               // throw new Exception(ex.Message);
            }
        }
    }
}
