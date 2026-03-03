using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERD.Service.BIZ.Utils
{
    public static class MailUT
    {


        /// <summary>
        /// Invia i mail utilizzando i parametri prelevati dalla configurazione .NET
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="subj"></param>
        /// <param name="body"></param>
        /// <param name="files"></param>
        public static void SendMail(string host, int port, bool useauth, bool ssl, string user, string pass, string from,
            string to, string cc, string subj, string body, IEnumerable<string> files)
        {
            if (!string.IsNullOrWhiteSpace(to))
                return;

            using (var smtp = new System.Net.Mail.SmtpClient(host, port))
            {
                smtp.EnableSsl = ssl;

                if (useauth)
                {
                    smtp.Credentials = new System.Net.NetworkCredential(user, pass);
                }

                var msg = new System.Net.Mail.MailMessage();
                msg.From = new System.Net.Mail.MailAddress(from);

                msg.To.Add(to);

                to.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x =>
                {
                    if (!string.IsNullOrWhiteSpace(x))
                        msg.To.Add(x);
                });

                cc.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x =>
                {
                    if (!string.IsNullOrWhiteSpace(x))
                        msg.CC.Add(x);
                });

                msg.Subject = subj;
                msg.IsBodyHtml = true;
                msg.Body = body;

                if (files != null)
                {
                    foreach (var item in files)
                    {
                        msg.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }
                }

                smtp.Send(msg);
            }

        }

    }
}
