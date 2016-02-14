using System;
using System.DirectoryServices.Protocols;
using System.Net;

namespace HolidayCalendar.Tools
{
    public class ActiveDirectoryAuthenticator : IDirectoryAuthenticator
    {
        public string UserName { get; set; }
        public string Domain { get; set; }

        public bool Authenticate(string password)
        {
            try
            {
                var credential = new NetworkCredential(UserName, password, Domain);
                var ldapServer = Domain;
                var ldapConnection = new LdapConnection(ldapServer);
                ldapConnection.Bind(credential);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return false;
        }
    }
}
