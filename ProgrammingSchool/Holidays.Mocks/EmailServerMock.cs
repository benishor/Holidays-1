using System;
using Holidays.Lib;

namespace Holidays.Mocks
{
    public class EmailServerMock : IEmailServer
    {
        public void SendEmail(Email email)
        {
            Console.WriteLine("Email sent");
            Console.WriteLine("\tFrom    : {0}", email.From);
            Console.WriteLine("\tTo      : {0}", email.To);
            Console.WriteLine("\tSubject : {0}", email.Subject);
            Console.WriteLine("\tBody    : {0}", email.Body);
            Console.WriteLine();
        }
    }
}