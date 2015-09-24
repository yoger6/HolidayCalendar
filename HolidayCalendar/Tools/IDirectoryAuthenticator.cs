namespace HolidayCalendar.Tools
{
    public interface IDirectoryAuthenticator
    {
        string UserName { get; set; }
        string Domain { get; set; }
        bool Authenticate(string password);
    }

    public class DirectoryAuthenticatorDummy : IDirectoryAuthenticator
    {
        public string UserName { get; set; }
        public string Domain { get; set; }
        public bool Authenticate(string password)
        {
            return true;
        }
    }
}
