namespace Holidays.Company
{
    public class Manager : Employee
    {
        public Manager(string name, string email)
            : base(name, email)
        {
        }

        public void ApproveHoliday(HolidayRequest request)
        {
            request.Approve();
        }

        public void RejectHoliday(HolidayRequest request)
        {
            request.Reject();
        }
    }
}