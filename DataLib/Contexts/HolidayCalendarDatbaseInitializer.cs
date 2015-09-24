using System;
using System.Data.Entity;
using System.Linq;
using DataLib.Model;

namespace DataLib.Contexts
{
    public class HolidayCalendarDatbaseInitializer : DropCreateDatabaseIfModelChanges<HolidyCalendarContext>
    {
        protected override void Seed(HolidyCalendarContext context)
        {
            
            var defaultHolidayReasons = new [] 
            {
                new HolidayReason { ShortName = "V", FullName = "Vacation", Color = new HolidayColor(146,53,35)},
                new HolidayReason { ShortName = "P", FullName = "Personal", Color = new HolidayColor(225,131,59)},
                new HolidayReason { ShortName = "S", FullName = "Sick", Color = new HolidayColor(158,177,96)},
                new HolidayReason { ShortName = "H", FullName = "Day", Color = new HolidayColor(191,154,14)},
                new HolidayReason { ShortName = "T", FullName = "Training", Color = new HolidayColor(80,98,160)},
                new HolidayReason { ShortName = "OC", FullName = "On Call", Color = new HolidayColor(109,52,147)},
                new HolidayReason { ShortName = "HO", FullName = "Home Office", Color = new HolidayColor(0,21,78)},
                new HolidayReason { ShortName = "D", FullName = "Delegation", Color = new HolidayColor(3,3,3)},
                new HolidayReason { ShortName = "VOC", FullName = "Vacation and On Call", Color = new HolidayColor(146,159,193)},
                new HolidayReason { ShortName = "17", FullName = "Work till 17", Color = new HolidayColor(148,119,2)},
                new HolidayReason { ShortName = "POC", FullName = "Peak On Call", Color = new HolidayColor(57,72,111)},
                new HolidayReason { ShortName = "HOC", FullName = "Day On Call", Color = new HolidayColor(126,126,126)}
            };

            var defaultEmployees = new[]
            {
                new Employee {FirstName = "Kajetan", FamilyName = "Chmiliński", Login = "fca85a2171d6dc56b9f56a736bafdae1b41f4c8dda3bf516c7fb1883f105042", Holidays = new [] {new Day {Date = new DateTime(2015,9,15), HolidayReason = defaultHolidayReasons.First()} } }
            };
            context.Employees.AddRange(defaultEmployees);
            context.HolidayReasons.AddRange(defaultHolidayReasons);
            context.SaveChanges();
        }
    }
}