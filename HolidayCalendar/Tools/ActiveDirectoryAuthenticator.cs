using System;
using System.DirectoryServices;

namespace HolidayCalendar.Tools
{
    public class ActiveDirectoryAuthenticator : IDirectoryAuthenticator
    {
        public string UserName { get; set; }
        public string Domain { get; set; }

        public bool Authenticate(string password)
        {
            using (var entry = new DirectoryEntry())
            {
                entry.Username = UserName;
                entry.Path = Domain;
                entry.Password = password;
                try
                {
                    var nativeObject = entry.NativeObject;
                    return true;
                }
                catch (DirectoryServicesCOMException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
    }
}
