namespace Holidays.Company
{
    public class Employee
    {

        public string Name { private set; get; }
        public string Email { private set; get; }

        public Employee(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public HolidayRequest AskForHoliday(Employee manager, Period period)
        {
            var request = new HolidayRequest(this, manager, period);
            request.Submit();

            return request;
        }
    }
}