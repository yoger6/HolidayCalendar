using System;

namespace DataLib.Model
{
    public class Day
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public int HolidayReasonId { get; set; }
        public virtual HolidayReason HolidayReason { get; set; }
    }
}
