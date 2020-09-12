using System;
using System.Net;
using System.Net.Mail;

namespace omission.api.Utility.Mail
{
    public static class MailHelper
    {

        public static bool Send(string body, string subject, string to)
        {
            // SmtpClient 
            // ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            var port = MailSettings.PORT;
            var smtpEmailHost = MailSettings.SMTP_EMAIL_HOST;
            var userName = MailSettings.USERNAME;
            var password = MailSettings.PASSWORD;

            using (var client = new SmtpClient
            {
                EnableSsl = true,
                Port = port,
                UseDefaultCredentials = false,
                Host = smtpEmailHost,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                DeliveryFormat = SmtpDeliveryFormat.International,
                Credentials = new NetworkCredential
                {
                    UserName = userName,
                    Password = password
                },
            })
            {
                var emailMessage = new MailMessage
                {
                    IsBodyHtml = true,
                    Body = body,
                    Subject = subject,
                    From = new MailAddress(userName),
                };

                emailMessage.To.Add(to);

                try
                {
                    client.Send(emailMessage);
                    return true;
                }
                catch (Exception e)
                {

                    return false;
                }
            }

        }

    }
}