using System;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class UtilityViewModelTest
    {
        private MainViewModel _mainViewModel;
        private UtilityViewModelStub _utilityViewModel;

        [TestInitialize]
        public void Initialize()
        {
            _mainViewModel = new MainViewModel();
            _utilityViewModel = new UtilityViewModelStub(_mainViewModel);
            _mainViewModel.CurrentUtilityViewModel = _utilityViewModel;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsExceptionIfMainViewModelIsNull()
        {
            _utilityViewModel = new UtilityViewModelStub(null);
        }

        [TestMethod]
        public void CloseRemovesUtilityFromMainViewModel()
        {
            _utilityViewModel.CloseMe();

            Assert.IsNull(_mainViewModel.CurrentUtilityViewModel);
        }

        [TestMethod]
        public void LoadChildrenViewModelChangesCurrentViewModel()
        {
            var childrenViewModel = new ChildViewModelStub();
            _utilityViewModel.LoadChildrenViewModel(childrenViewModel);

            Assert.AreEqual(childrenViewModel, _mainViewModel.CurrentViewModel);
        }

        [TestMethod]
        public void ChangeUtilityLoadsAnotherUtilityViewModel()
        {
            var newUtility = new UtilityViewModelStub(_mainViewModel);
            _utilityViewModel.ChangeUtilityViewModel(newUtility);

            Assert.AreEqual(newUtility, _mainViewModel.CurrentUtilityViewModel);
        }
    }
}
