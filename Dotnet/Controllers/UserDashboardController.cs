using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessAppcf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using FitnessFinal.Models;

namespace FitnessFinal.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly FinalContext _context;

        public UserDashboardController(FinalContext context)
        {
            _context = context;
        }

        // GET: UserDashboard
        public async Task<IActionResult> Index()
        {
            // Get the UserId from session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Redirect to login if UserId is not available
                return RedirectToAction("Login", "Members");
            }

            var member = await _context.Members
                .Include(m => m.Enquiries)
                .Include(m => m.UserMemberships)
                .FirstOrDefaultAsync(m => m.UserId == userId);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
    


// GET: Members/Edit/5
public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            // If the member is found, return the Edit view with the member data
            return View(member);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,Name,City,Password,Phone,IsAdmin,SecurityQues,SecurityAns")] Member member)
        {
            if (id != member.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }


        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.UserId == id);
        }


        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the member with the given UserId
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }



    }
}
