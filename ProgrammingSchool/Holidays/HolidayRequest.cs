using System;
using Holidays.Company;
using Holidays.Resources;
using Holidays.Lib;
using System.Configuration;

namespace Holidays
{
    public enum Status
    {
        New,
        Pending,
        Approved,
        Rejected
    }

    public class HolidayRequest
    {
        
        private Guid requestId;
        private Employee requester;
        private Employee approver;
        private Period holidayPeriod;
        private Status status;

        public HolidayRequest(Employee requester, Employee approver, Period holidayPeriod)
        {
            requestId = Guid.NewGuid();
            this.requester = requester;
            this.approver = approver;
            this.holidayPeriod = holidayPeriod;
            status = Status.New;
        }

        public void Submit()
        {
            SendEmailForApprove();
            status = Status.Pending;
            SaveRequest();
        }

        public void Approve()
        {
            SendAcceptEmail();
            status = Status.Approved;
            SaveRequest();
        }

        public void Reject()
        {
            SendRejectEmail();
            status = Status.Rejected;
            SaveRequest();
        }

        // CR: we could improve clarity by moving these private methods below the unit exposed contract
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
            // CR: why not employ the same formatting as for the subject? It is more likely for 
            // the body to contain all details.
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

        private void SaveRequest()
        {
            // CR: cute but tricky :) Mixed feelings about this one; feels like a hack. 
            // You are basically using a new instance as an alternative to injection or locator 
            // but not much gain gets out of it except delegating the need for locating the storage. 
            //
            // At this time you do not have costly initializations for the repository but that is not 
            // always the case in real life scenarios. The point I'm trying to make is that it feels
            // unnatural using a repository instance like this.
            var holidayRequestRepository = new HolidayRequestRepository();
            holidayRequestRepository.Store(this);
        }

        public override string ToString()
        {
            return String.Format("From:{0} To:{1} Status: {2} ID:{3}", requester.Name, approver.Name,status,requestId);
        }

        // CR: isPendingForApproval()
        public bool IsPendingForApprove(string approverEmail)
        {
            return approver.Email == approverEmail && status == Status.Pending;
        }
    }
}