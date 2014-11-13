namespace Holidays.Company
{
    public class EmployeesRepository
    {
        public Employee CreateEmployee(string name, string email)
        {
            return new Employee(name, email);
        }

        public Manager CreateManager(string name, string email)
        {
            return new Manager(name, email);
        }
    }
}