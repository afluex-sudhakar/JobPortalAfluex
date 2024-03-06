using System;
using System.Net;
using System.Net.Mail;

namespace Utility
{
    public static class BLMail
    {
        public static string SendMail(string To, string Subject, string Body, bool IsHTML)
        {
            try
            {
                var fromAddress = new MailAddress(Constants.MAIL_FROM_EMAIL, Constants.MAIL_FROM);
                var toAddress = new MailAddress(To);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {

                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, Constants.MAIL_PASSWORD)

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = IsHTML,
                    Subject = Subject,
                    Body = Body
                })
                    smtp.Send(message);
                return Constants.MAIL_SENT_SUCCESSFUL_MESSAGE;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
