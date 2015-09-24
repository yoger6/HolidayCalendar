using System;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class ChildViewModelTest
    {
        private ChildViewModelStub _child = new ChildViewModelStub();
        private MainViewModel _main = new MainViewModel();

        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void ThrowsExceptionWhenTryingToNavigateWithoutMainViewModelSet()
        {
            _child.NavigateTo(new ChildViewModelStub());
        }

        [TestMethod]
        public void OnLoadedTriggersWhenMainViewModelLoadsChildrenToCurrentViewModel()
        {
            _main.LoadModel(_child);

            Assert.IsTrue(_child.ViewHasBeenLoaded);
        }
        [TestMethod]
        public void OnUnloadedLoadedTriggersWhenNextViewModelIsLoaded()
        {
            _main.CurrentViewModel = _child;
            _main.LoadModel(new ChildViewModelStub());

            Assert.IsTrue(_child.ViewHasBeenUnloaded);
        }
    }
}
