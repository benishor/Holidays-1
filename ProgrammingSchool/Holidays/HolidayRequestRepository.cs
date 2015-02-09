using System;
using System.Collections.Generic;
using System.Linq;

using NDatabase;
using NDatabase.Api;

namespace Holidays
{
    public class HolidayRequestRepository
    {
        private const string DATABASE = "HolidayRequest.db";

        private static readonly IOdb storage;

        static HolidayRequestRepository()
        {
            storage = OdbFactory.Open(DATABASE);
        }
       
        public void Store(HolidayRequest holidayRequest)
        {
            storage.Store(holidayRequest);
            storage.Commit();
        }

        public List<HolidayRequest> GetAll()
        {
            return storage.Query<HolidayRequest>().Execute<HolidayRequest>().ToList();    
            
        }

        public List<HolidayRequest> GetNewRequestsForApprover(string approver)
        {
            return storage.AsQueryable<HolidayRequest>().Where(r => r.IsPendingForApprove(approver)).ToList();
        }
    }
}
