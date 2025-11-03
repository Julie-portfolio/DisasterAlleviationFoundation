using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Controllers
{
    [Authorize]
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DonationsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var donations = await _db.Donations
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
            return View(donations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.CreatedAt = DateTime.UtcNow;
            model.Status = "pending";

            _db.Donations.Add(model);
            await _db.SaveChangesAsync();

            TempData["ok"] = "Donation submitted.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        public async Task<IActionResult> Manage()
        {
            var list = await _db.Donations
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();

            ViewBag.Summary = list
                .GroupBy(d => d.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(x => x.Quantity) })
                .ToList();

            return View(list);
        }

        [Authorize(Roles = "Admin,Coordinator")]
        public async Task<IActionResult> MarkDistributed(int id)
        {
            var donation = await _db.Donations.FindAsync(id);
            if (donation == null)
                return NotFound();

            donation.Status = "distributed";
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Manage));
        }
    }
}
