using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class LegendViewModelTest
    {
        private HolidayRepositoryStub _repository;
        private List<HolidayReason> _items;
        
        [TestInitialize]
        public void Initialize()
        {
            _items = new List<HolidayReason>
            {
                new HolidayReason {FullName = "HolidayReason number one"},
                new HolidayReason {FullName = "HolidayReason number two"},
            };
            _repository = new HolidayRepositoryStub(_items);
        }

        [TestMethod]
        public void GetsItemsFromRepositoryWhenInstantiated()
        {
            var getCallRegisteredByRepository = false;
            _repository.GetCalled += (sender, args) => { getCallRegisteredByRepository = true; };

            var viewModel = new LegendViewModel(_repository);

            Assert.IsTrue(getCallRegisteredByRepository);
        }

        [TestMethod]
        public void FillsItemsFromRepositoryIntoCollection()
        {
            var viewModel = new LegendViewModel(_repository);
            
            CollectionAssert.AreEquivalent(_items, viewModel.Reasons.ToArray());
        }

        [TestMethod]
        public void DisposesRepository()
        {
            var disposeCallRegisteredByRepository = false;
            _repository.DisposeCalled += (sender, args) => { disposeCallRegisteredByRepository = true; };

            var viewModel = new LegendViewModel(_repository);

            Assert.IsTrue(disposeCallRegisteredByRepository);
        }
    }
}
