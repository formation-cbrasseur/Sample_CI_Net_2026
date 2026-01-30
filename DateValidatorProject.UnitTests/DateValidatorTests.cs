using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DateValidatorProject.UnitTests
{
    [TestClass]
    public sealed class DateValidatorTests
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void CheckNumberOfDashes_WithNullOrEmptyString_ShouldThrowArgumentException(string date)
        {
            var validator = new DateValidator(date);

            Assert.ThrowsException<ArgumentException>(() => validator.CheckNumberOfDashes());
        }

        [TestMethod]
        [DataRow("a-e-a-")]
        [DataRow("a-r")]
        [DataRow("10--10-2025--")]
        public void CheckNumberOfDashes_WithLessOrHigherNumberOfDashes_ShouldReturnsFalse(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberOfDashes();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckNumberOfDashes_WithTwoDashes_ShouldReturnTrue()
        {
            var validator = new DateValidator("--");

            var result = validator.CheckNumberOfDashes();

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("a-10-2025")]
        [DataRow("1-hdfg-2025")]
        [DataRow("3-10-dsfsdfqds")]
        [DataRow("qsdqdsf-dfd-dsfsdfqds")]
        public void CheckAllNumerics_WithNonAllNumericsValue_ShouldReturnFalse(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckAllNumerics();

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("122-10-2025")]
        [DataRow("02-4-2025")]
        public void CheckAllNumerics_WithAllNumericsValues_ShouldReturnTrue(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckAllNumerics();

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("0-10-2025")]
        [DataRow("32-10-2025")]
        [DataRow("60-10-2025")]
        public void CheckNumberBetween_WithWrongDay_ShouldReturnFalse(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberBetween(1, 31, 0);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("1-10-2025")]
        [DataRow("31-10-2025")]
        [DataRow("18-10-2025")]
        public void CheckNumberBetween_WithGoodDay_ShouldReturnTrue(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberBetween(1, 31, 0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("1-0-2025")]
        [DataRow("1-13-2025")]
        [DataRow("1-43-2025")]
        public void CheckNumberBetween_WithWrongMonth_ShouldReturnFalse(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberBetween(1, 12, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("1-1-2025")]
        [DataRow("1-12-2025")]
        [DataRow("1-6-2025")]
        public void CheckNumberBetween_WithGoodMonth_ShouldReturnTrue(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberBetween(1, 12, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumberBetween_WithActualYear_ShouldReturnTrue()
        {
            var validator = new DateValidator($"01-01-{DateTime.Now.Year}");

            var result = validator.CheckNumberBetween(2000, DateTime.Now.Year, 2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumberBetween_WithActualYearPlusOne_ShouldReturnFalse()
        {
            var validator = new DateValidator($"01-01-{DateTime.Now.Year + 1}");

            var result = validator.CheckNumberBetween(2000, DateTime.Now.Year, 2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("1-10-1999")]
        [DataRow("1-8-1500")]
        public void CheckNumberBetween_WithWrongYear_ShouldReturnFalse(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberBetween(2000, DateTime.Now.Year, 2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("1-1-2000")]
        [DataRow("1-12-2001")]
        public void CheckNumberBetween_WithGoodYear_ShouldReturnTrue(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.CheckNumberBetween(2000, DateTime.Now.Year, 2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("1--1-2000")]
        [DataRow("1-12-200101")]
        [DataRow("azeaze-12-200101")]
        public void ValidateStringDate_WithWrongDate_ShouldReturnFalse(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.ValidateStringDate();

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("01-10-2000")]
        [DataRow("1-12-2005")]
        [DataRow("9-4-2022")]
        public void ValidateStringDate_WithGoodDate_ShouldReturnTrue(string date)
        {
            var validator = new DateValidator(date);

            var result = validator.ValidateStringDate();

            Assert.IsTrue(result);
        }
    }
}
