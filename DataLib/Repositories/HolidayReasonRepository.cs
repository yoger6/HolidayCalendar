using System;
using DataLib.Model;

namespace DataLib.Repositories
{
    public class HolidayReasonRepository : Repository<HolidayReason>, IHolidayReasonRepository
    {

        public HolidayReasonRepository()
        {}

        public HolidayReasonRepository(string connectionString)
        :base(connectionString)
        {
            
        }
    }
}
