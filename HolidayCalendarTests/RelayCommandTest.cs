using System;
using HolidayCalendar.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            var command = new RelayCommand(null);
        }

        [TestMethod]
        public void ExecuteInvokeAction()
        {
            var invoked = false;
            var command = new RelayCommand(obj => invoked = true);

            command.Execute();

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void ExecuteOverloadInvokeActionWithParameter()
        {
            var invoked = false;
            var command = new RelayCommand(obj =>
            {
                Assert.IsNotNull(obj);
                invoked = true;
            });

            command.Execute(new object());
            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new RelayCommand(obj => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void CanExecuteOverloadExecutesTruePredicate()
        {
            var command = new RelayCommand(obj => { }, obj => (int)obj == 1);
            Assert.IsTrue(command.CanExecute(1));
        }

        [TestMethod]
        public void CanExecuteOverloadExecutesFalsePredicate()
        {
            var command = new RelayCommand(obj => { }, obj => (int)obj == 1);
            Assert.IsFalse(command.CanExecute(0));
        }
    }
}
