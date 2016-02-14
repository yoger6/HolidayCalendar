using System;
using System.Configuration;
using System.Linq;
using DataLib.Contexts;

namespace HolidayCalendar.Tools
{
    public static class ConnectionVerifier
    {
        public static bool IsConnectionValid(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) return false;
            using (var context = new HolidayCalendarContext(connectionString))
            {
                try
                {
                    context.Database.Initialize(false);
                    context.Database.Connection.Open();
                    var contex = context.Employees.FirstOrDefault();
                    context.Database.Connection.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}