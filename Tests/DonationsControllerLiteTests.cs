using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisasterAlleviationApp.Controllers;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DisasterAlleviationApp.Tests
{
    [TestClass]
    public class DonationsControllerLiteTests
    {
        private DonationsControllerLite _controller = null!;
        private FakeDonationRepository _repo = null!;

        [TestInitialize]
        public void Setup()
        {
            _repo = new FakeDonationRepository();
            _controller = new DonationsControllerLite(_repo);
        }

        [TestMethod]
        public void Create_ValidDonation_RedirectsToIndexAndPersistsPending()
        {
            var donation = new Donation { Category = "Food", Quantity = 5, DonorEmail = "donor@example.com" };

            var result = _controller.Create(donation);

            // Assert redirect
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirect.ActionName);

            // Assert persisted
            var all = _repo.GetAll().ToList();
            Assert.AreEqual(1, all.Count);
            var saved = all[0];
            Assert.AreEqual("pending", saved.Status);
            Assert.AreEqual(5, saved.Quantity);
            Assert.AreEqual("Food", saved.Category);
        }

        [TestMethod]
        public void Create_InvalidDonation_ReturnsCreateViewWithModel()
        {
            // Quantity zero
            var d1 = new Donation { Category = "Food", Quantity = 0 };
            var r1 = _controller.Create(d1);
            Assert.IsInstanceOfType(r1, typeof(ViewResult));
            var v1 = (ViewResult)r1;
            Assert.AreSame(d1, v1.Model);

            // Empty category
            var d2 = new Donation { Category = "", Quantity = 3 };
            var r2 = _controller.Create(d2);
            Assert.IsInstanceOfType(r2, typeof(ViewResult));
            var v2 = (ViewResult)r2;
            Assert.AreSame(d2, v2.Model);

            // Repository should still be empty
            Assert.AreEqual(0, _repo.GetAll().Count());
        }

        [TestMethod]
        public void MarkDistributed_ExistingDonation_UpdatesAndRedirectsToManage()
        {
            var donation = new Donation { Id = 10, Category = "Clothing", Quantity = 2, Status = "pending" };
            _repo.Add(donation);

            var result = _controller.MarkDistributed(10);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var red = (RedirectToActionResult)result;
            Assert.AreEqual("Manage", red.ActionName);

            var saved = _repo.GetById(10);
            Assert.IsNotNull(saved);
            Assert.AreEqual("distributed", saved!.Status);
        }

        [TestMethod]
        public void MarkDistributed_NonExistingDonation_ReturnsNotFound()
        {
            var result = _controller.MarkDistributed(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
