namespace Holidays.Lib
{
    public class Email
    {
        public string From {get;private set;}
        public string To {get;private set;}
        public string Subject;
        public string Body;
        public Email(string from, string to)
        {
            From = from;
            To = to;
        }
        public void Send()
        {
            EmailServerLocator.EmailServer.SendEmail(this);
        }
    }
}
