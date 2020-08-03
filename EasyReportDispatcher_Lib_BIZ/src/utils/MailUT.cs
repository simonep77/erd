using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_Lib_BIZ.src.utils
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
        public static void SendMailFromDefaultConf(string to, string cc, string subj, string body, IEnumerable<string> files)
        {
            if (!string.IsNullOrWhiteSpace(to))
                return;

            using (var smtp = new System.Net.Mail.SmtpClient())
            {
                var msg = new System.Net.Mail.MailMessage();
                msg.To.Add(to);

                if (!string.IsNullOrWhiteSpace(cc))
                    msg.CC.Add(cc);

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
