using System.Collections.Generic;
using System.Linq;

using NDatabase.Api;

namespace Holidays
{
    public class HolidayRequestRepository
    {
        private readonly IOdb storage;

        public HolidayRequestRepository()
        {
            storage = StorageLocator.Get();
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
