using HolidayCalendar.ViewModel;

namespace HolidayCalendarTests.Stubs
{
    public class UtilityViewModelStub : UtilityViewModel
    {
        public UtilityViewModelStub(MainViewModel mainViewModel) : base(mainViewModel)
        {
        }

        public void CloseMe()
        {
            Close();
        }

        public void LoadChildrenViewModel(ChildViewModel viewModel)
        {
            LoadViewModel(viewModel);
        }

        public void ChangeUtilityViewModel(UtilityViewModel viewModel)
        {
            ChangeUtility(viewModel);
        }
    }
}
