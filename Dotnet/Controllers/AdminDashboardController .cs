using System.Linq;
using System.Threading.Tasks;
using FitnessFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessFinal.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly FinalContext _context;

        public AdminDashboardController(FinalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve the userId from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Members");
            }

            // Find the member with the given userId
            var member = await _context.Members.FindAsync(userId);

            if (member == null || !member.IsAdmin)
            {
                return RedirectToAction("Login", "Members");
            }

            // User is admin, proceed to admin dashboard
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Login", "Members");
        }
    }
}
