using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviationApp.Controllers
{
    [Authorize]
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VolunteersController(ApplicationDbContext context) => _context = context;

        public IActionResult Index() => View(_context.Volunteers.ToList());

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            if (!ModelState.IsValid) return View(volunteer);
            _context.Add(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AssignTask(int id) => View(new TaskAssignment { VolunteerId = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTask(TaskAssignment task)
        {
            if (!ModelState.IsValid) return View(task);
            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
