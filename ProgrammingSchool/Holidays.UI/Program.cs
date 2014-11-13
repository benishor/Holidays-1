using System;
using Holidays.Company;
using Holidays.Mocks;
using Holidays.Lib;

namespace Holidays.UI
{
    class Program
    {
        static void Main()
        {
            Setup();

            var employeesRepository = new EmployeesRepository();

            Manager manager = employeesRepository.CreateManager("Manager", "manager@company.com");
            Employee employee = employeesRepository.CreateEmployee("Employee", "employee@company.com");
            
            var holidayPeriod = new Period(new DateTime(2014, 11, 25), new DateTime(2014, 11, 28));
            var holidayRequest = employee.AskForHoliday(manager, holidayPeriod);

            manager.ApproveHoliday(holidayRequest);
            manager.RejectHoliday(holidayRequest);            
        }

        private static void Setup()
        {
            EmailServerLocator.EmailServer = new EmailServerMock();
        }
    }
}

/* Sample output
 * 
 *
 * 
Email sent
        From    : employee@company.com
        To      : manager@company.com
        Subject : Employee asked a holiday for [25.11.2014 - 28.11.2014]
        Body    : Please accept/reject the request

Email sent
        From    : manager@company.com
        To      : hr@company.com
        Subject : Manager approved a holiday for Employee btw [25.11.2014 - 28.11.2014]
        Body    :

Email sent
        From    : manager@company.com
        To      : employee@company.com
        Subject : Manager rejected your holiday request btw [25.11.2014 - 28.11.2014]
        Body    :
 * 
 * 
 */
