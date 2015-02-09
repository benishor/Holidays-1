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

        private void SaveRequest()
        {
            var holidayRequestRepository = new HolidayRequestRepository();
            holidayRequestRepository.Store(this);    
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

        public override string ToString()
        {
            return String.Format("From:{0} To:{1} Status: {2} ID:{3}", requester.Name, approver.Name,status,requestId);
        }

        internal bool IsPendingForApprove(string approverEmail)
        {
            return approver.Name == approverEmail && status == Status.Pending;
        }
    }
}