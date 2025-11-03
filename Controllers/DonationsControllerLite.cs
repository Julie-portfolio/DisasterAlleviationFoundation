using Microsoft.AspNetCore.Mvc;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Services;

namespace DisasterAlleviationApp.Controllers
{
    public class DonationsControllerLite : Controller
    {
        private readonly IDonationRepository _repo;
        public DonationsControllerLite(IDonationRepository repo) => _repo = repo;

        public IActionResult Index() => View(_repo.GetAll());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Donation model)
        {
            if (model == null) return BadRequest();

            if (model.Quantity <= 0 || string.IsNullOrWhiteSpace(model.Category))
            {
                ModelState.AddModelError(string.Empty, "Invalid donation.");
                return View(model);
            }

            model.Status = "pending";
            model.CreatedAt = DateTime.UtcNow;
            _repo.Add(model);

            return RedirectToAction("Index");
        }

        public IActionResult Manage() => View(_repo.GetAll());

        public IActionResult MarkDistributed(int id)
        {
            var donation = _repo.GetById(id);
            if (donation == null) return NotFound();

            donation.Status = "distributed";
            _repo.Update(donation);
            return RedirectToAction("Manage");
        }
    }
}
