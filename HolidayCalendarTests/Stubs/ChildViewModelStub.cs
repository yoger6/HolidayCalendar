using HolidayCalendar.ViewModel;

namespace HolidayCalendarTests.Stubs
{
    public class ChildViewModelStub : ChildViewModel
    {
        public bool ViewHasBeenLoaded { get; set; }
        public bool ViewHasBeenUnloaded { get; set; }

        public override void OnLoaded()
        {
            ViewHasBeenLoaded = true;
        }

        public override void OnUnloaded()
        {
            ViewHasBeenUnloaded = true;
        }
    }
}