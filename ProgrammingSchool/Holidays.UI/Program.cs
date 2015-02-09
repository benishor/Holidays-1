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

            Employee manager = new Employee("Manager", "manager@company.com");
            Employee employee = new Employee("Employee", "employee@company.com");

            HolidayRequestRepository holidayRequestRepository = new HolidayRequestRepository();

            var holidayPeriod = new Period(new DateTime(2014, 11, 25), new DateTime(2014, 11, 28));

            employee.AskForHoliday(manager, holidayPeriod);
            holidayRequestRepository.GetNewRequestsForApprover("manager@company.com").ForEach(r => r.Approve());
            
            employee.AskForHoliday(manager, holidayPeriod);
            holidayRequestRepository.GetNewRequestsForApprover("manager@company.com").ForEach(r => r.Reject());

            
            holidayRequestRepository.GetAll().ForEach(r => Console.WriteLine(r.ToString()));
            
            Console.ReadLine();
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
