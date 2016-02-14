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
        public void DatabaseLoginViewModelIsLoadedByDefault()
        {
            Assert.IsInstanceOfType(_viewModel.CurrentUtilityViewModel, typeof(DatabaseLoginViewModel));
        }

        [TestMethod]
        public void LoginViewModelIsLoadedByDefaultIfValidConnectionStringWasFound()
        {
            SettingsHelper.SaveConnectionString("Data Source=YOGER-SUPERMAN\\SQLEXPRESS;Initial Catalog=HolidayCalendar;Integrated Security=True;Connect Timeout=5");
            Assert.IsInstanceOfType(_viewModel.CurrentUtilityViewModel, typeof(LoginViewModel));
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

        [TestMethod]
        public void LoadUtilityViewModelAssignsItToProperty()
        {
            var utilityViewModel = new UtilityViewModelStub(_viewModel);
            _viewModel.LoadUtilityViewModel(utilityViewModel);

            Assert.AreEqual(utilityViewModel, _viewModel.CurrentUtilityViewModel);
        }

        [TestMethod]
        public void UnloadUtilityViewModelRemovesCurrentUtilityViewModel()
        {
            _viewModel.CurrentUtilityViewModel = new UtilityViewModelStub(_viewModel);
            _viewModel.UnloadUtilityViewModel();

            Assert.IsNull(_viewModel.CurrentUtilityViewModel);
        }
    }
}
