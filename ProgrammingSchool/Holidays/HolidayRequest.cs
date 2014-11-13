using Holidays.Company;
using Holidays.Resources;
using Holidays.Lib;
using System.Configuration;

namespace Holidays
{
    public class HolidayRequest
    {
        private Employee requester;
        private Employee approver;
        private Period holidayPeriod;

        public HolidayRequest(Employee requester, Employee approver, Period holidayPeriod)
        {
            this.requester = requester;
            this.approver = approver;
            this.holidayPeriod = holidayPeriod;
        }

        public void Submit()
        {
            SendEmailForApprove();
        }

        public void Approve()
        {
            SendAcceptEmail();
        }

        public void Reject()
        {
            SendRejectEmail();
        }

        private void SendAcceptEmail()
        {
            var notification = new Email(approver.Email, ConfigurationSettings.AppSettings["HREmail"]);
            notification.Subject = GetSubject(HolidaysResources.AcceptNotificationSubject);
            notification.Send();
        }

        private void SendEmailForApprove()
        {
            var notification = new Email(requester.Email, approver.Email);
            notification.Subject = GetSubject(HolidaysResources.SubmitNotificationSubject);
            notification.Body = HolidaysResources.SubmitNotificationBody;
            notification.Send();
        }

        private void SendRejectEmail()
        {
            var notification = new Email(approver.Email, requester.Email);
            notification.Subject = GetSubject(HolidaysResources.RejectNotificationSubject);
            notification.Send();
        }

        private string GetSubject(string subjectTemplate)
        {
            return string.Format(subjectTemplate,
                                    approver.Name, requester.Name,
                                    holidayPeriod.From.ToShortDateString(),
                                    holidayPeriod.To.ToShortDateString());
        }
    }
}