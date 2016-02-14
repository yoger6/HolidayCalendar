using System.Collections.Generic;
using HolidayCalendar.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class SettingsHelperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            SettingsHelper.ResetSettings();
        }

        [TestMethod]
        public void WhenRememberLoginIsFalseSettingsAreNotSaved()
        {
            var settings = SetAndGetSettings(false, "domain", "user");

            Assert.IsNull(settings);
        }
        [TestMethod]
        public void WhenRememberLoginIsTrueSettingsAreSaved()
        {
            var settings = SetAndGetSettings(true, "domain", "user");

            Assert.IsNotNull(settings);
        }

        [TestMethod]
        public void LoadedSettingsMatchSavedValues()
        {
            var remember = true;
            var domain = "Domain name";
            var user = "User name";

            var settings = SetAndGetSettings(remember, domain, user);

            Assert.AreEqual(remember, settings["RememberLogin"]);
            Assert.AreEqual(domain, settings["Domain"]);
            Assert.AreEqual(user, settings["UserName"]);
        }

        private IDictionary<string, object> SetAndGetSettings(bool rememberLogin, string domainName, string userName)
        {
            SettingsHelper.SaveUserSettings(rememberLogin, domainName, userName);
            return SettingsHelper.LoadSettings();
        } 
    }
}
