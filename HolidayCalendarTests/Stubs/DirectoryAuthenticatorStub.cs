using System;
using HolidayCalendar.Tools;

namespace HolidayCalendarTests.Stubs
{
    public class DirectoryAuthenticatorStub : IDirectoryAuthenticator
    {
        public event EventHandler<bool> AuthenticationOccured; 
        private string _validUser;
        private string _validDomain;
        private string _validPassword;

        public string UserName { get; set; }
        public string Domain { get; set; }

        public DirectoryAuthenticatorStub(string validDomain, string validUser, string validPassword)
        {
            _validDomain = validDomain;
            _validUser = validUser;
            _validPassword = validPassword;
        }

        public bool Authenticate(string password)
        {   
            var result = UserName == _validUser && Domain == _validDomain && password == _validPassword;
            
            OnAuthenticationOccured(result);
            return result;
        }

        protected virtual void OnAuthenticationOccured(bool authenticationResult)
        {
            AuthenticationOccured?.Invoke(this, authenticationResult);
        }
    }
}
