using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace MailSender
{
    public static class MailSender
    {
        public static void send(string toMailAdress, string messageBody, string messageSubject)
        {
			try
			{
                //if (string.IsNullOrEmpty(toMailAdress))
                //    return null;

                MimeMessage message = new MimeMessage();

                message.From.Add(new MailboxAddress("Holder App", "dertlesh41@gmail.com"));
                message.To.Add(MailboxAddress.Parse(toMailAdress));
                message.Subject = messageSubject;
                message.Body = new TextPart(messageBody);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("dertlesh41@gmail.com", "rrecghdrhudfznzv");
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
			catch (Exception ex)
			{
                throw new Exception(ex.Message);
            }
        }
    }
}