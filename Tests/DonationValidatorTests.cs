using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisasterAlleviationApp.Services;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Tests
{
    [TestClass]
    public class DonationValidatorTests
    {
        private DonationValidator _validator = null!;

        [TestInitialize]
        public void Setup()
        {
            _validator = new DonationValidator();
        }

        [TestMethod]
        public void ValidDonation_ReturnsTrue()
        {
            var donation = new Donation
            {
                Category = "Food",
                Quantity = 10,
                DonorEmail = "donor@example.com"
            };

            var result = _validator.IsValid(donation);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ZeroOrNegativeQuantity_ReturnsFalse()
        {
            var d1 = new Donation { Category = "Food", Quantity = 0 };
            var d2 = new Donation { Category = "Food", Quantity = -5 };

            Assert.IsFalse(_validator.IsValid(d1));
            Assert.IsFalse(_validator.IsValid(d2));
        }

        [TestMethod]
        public void EmptyOrNullCategory_ReturnsFalse()
        {
            var d1 = new Donation { Category = "", Quantity = 5 };
            var d2 = new Donation { Category = null!, Quantity = 5 };

            Assert.IsFalse(_validator.IsValid(d1));
            Assert.IsFalse(_validator.IsValid(d2));
        }

        [TestMethod]
        public void InvalidEmailFormat_ReturnsFalse()
        {
            var donation = new Donation
            {
                Category = "Clothing",
                Quantity = 2,
                DonorEmail = "not-an-email"
            };

            var result = _validator.IsValid(donation);

            Assert.IsFalse(result);
        }
    }
}
