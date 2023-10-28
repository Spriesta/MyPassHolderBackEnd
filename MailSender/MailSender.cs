using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MyPassHolder.Common;
using HelperLib;

namespace MailSender
{
    public static class MailSender
    {
        public static ResponseHandle send(MailRequest req)
        {
            ResponseHandle response = new ResponseHandle();

			try
			{
                MimeMessage message = new MimeMessage();

                message.From.Add(new MailboxAddress("My Holder Admin", "dertlesh41@gmail.com"));
                message.To.Add(MailboxAddress.Parse(req.toMailAdress));
                message.Subject = req.messageSubject;
                message.Body = new TextPart("plain")
                {
                    Text = req.messageBody
                };

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("dertlesh41@gmail.com", "rrecghdrhudfznzv");
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
			catch (Exception ex)
			{
                response.success = false;
                response.errorMesssage = ex.Message;
            }

            return response;
        }
    }
}