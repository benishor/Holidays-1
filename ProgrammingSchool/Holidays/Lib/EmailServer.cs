using System.Net.Mail;

namespace Holidays.Lib
{
    public class EmailServer : IEmailServer
    {
        // CR: perhaps smtpClient would be a clearer name; the way it is now we end up having 
        // an EmailServer making use of a mailClient which seems pretty confusing.
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