using System.Collections.Generic;
using DataLib.Model;
using DataLib.Repositories;

namespace HolidayCalendarTests.Stubs
{
    public class HolidayRepositoryStub : RepositoryStub<HolidayReason>, IHolidayReasonRepository
    {
        public HolidayRepositoryStub(List<HolidayReason> items)
            : base(items)
        {
        }
    }
}
