using System;
using HolidayCalendar.Tools;
using HolidayCalendar.Tools.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{   
    [TestClass]
    public class LoginHasherTest
    {
        private Sha256StringHasher _hasher = new Sha256StringHasher();
        private string _inputString = "1";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInputThrowsException()
        {
            _hasher.Hash(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InputEmptyThrowsException()
        {
            _hasher.Hash(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InputWhitespaceThrowsException()
        {
            _hasher.Hash(" ");
        }

        [TestMethod]
        public void HashedStringDoesNotEqualOriginalOne()
        {
            var outputString = _hasher.Hash(_inputString);

            Assert.AreNotEqual(_inputString, outputString);
        }

        [TestMethod]
        public void HasherReturnsCorrectSha256EncodedValue()
        {
            var outputString = _hasher.Hash(_inputString);

            Assert.AreEqual("6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b", outputString);
        }

        [TestMethod]
        public void ComparisionOfStringThatWasPreviouslyHashedWithPlainOneIsSuccessfull()
        {
            var hashedInput = _hasher.Hash(_inputString);

            Assert.IsTrue(_hasher.Compare(_inputString, hashedInput));
        }

        [TestMethod]
        public void CompariosionOfHashedStringAndOtherDifferentFromPlainReturnsFalse()
        {
            var hashedInput = _hasher.Hash(_inputString);
            var differentString = "something different";

            Assert.IsFalse(_hasher.Compare(differentString, hashedInput));
        }
    }
}
