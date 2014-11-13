using System.Net.Mail;

namespace Holidays.Lib
{
    public class EmailServer : IEmailServer
    {
        private SmtpClient mailClient;
        
        public EmailServer(string smtpServer, int smtpServerPort)
        {
            mailClient = new SmtpClient(smtpServer, smtpServerPort);
        }

        public void SendEmail(Email email)
        {
            mailClient.Send(email.From, email.To, email.Subject, email.Body);
        }
    }
}