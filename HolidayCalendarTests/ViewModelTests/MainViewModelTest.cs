using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class MainViewModelTest
    {
        private MainViewModel _viewModel;

        [TestInitialize]
        public void Initialize()
        {
            _viewModel = new MainViewModel();
        }

        [TestMethod]
        public void LoginViewModelIsLoadedByDefault()
        {
            Assert.IsInstanceOfType(_viewModel.CurrentViewModel, typeof(LoginViewModel));
        }

        [TestMethod]
        public void LoadingSpecifiedViewModelSetsItAsCurrentViewModel()
        {
            var calendarViewModel = new ChildViewModelStub();

            _viewModel.LoadModel(calendarViewModel);

            Assert.AreEqual(calendarViewModel, _viewModel.CurrentViewModel);
        }

        [TestMethod]
        public void OnExitMainViewModelUnloadsCurrentView()
        {
            _viewModel.Close();

            Assert.IsNull(_viewModel.CurrentViewModel);
        }
    }
}
