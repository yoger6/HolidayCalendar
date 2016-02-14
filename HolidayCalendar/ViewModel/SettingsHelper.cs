using System.Collections.Generic;
using HolidayCalendar.Properties;

namespace HolidayCalendar.ViewModel
{
    public static class SettingsHelper
    {
        public static Dictionary<string, object> LoadSettings()
        {
            if (Settings.Default.RememberLogin)
            {
                var settings = new Dictionary<string, object>
                {
                    {"RememberLogin", Settings.Default.RememberLogin},
                    {"Domain", Settings.Default.Domain},
                    {"UserName", Settings.Default.UserName},
                    {"ConnectionString", Settings.Default.ConnectionString }
                };
                
                return settings;
            }

            return null;
        }

        public static string GetConnectionString()
        {
            return Settings.Default.ConnectionString;
        }

        public static void SaveUserSettings(bool remeberLogin, string domain, string userName)
        {
            if (remeberLogin)
            {
                Settings.Default.RememberLogin = remeberLogin;
                Settings.Default.Domain = domain;
                Settings.Default.UserName = userName;

                Settings.Default.Save();
            }
        }

        public static void SaveConnectionString(string connectionString)
        {
            Settings.Default.ConnectionString = connectionString;

            Settings.Default.Save();
        }

        public static void ResetSettings()
        {
            Settings.Default.Reset();
        }
    }
}