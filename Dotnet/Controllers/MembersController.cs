using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessFinal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FitnessFinal.Controllers
{
    public class MembersController : Controller
    {
        private readonly FinalContext _context;

        public MembersController(FinalContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "AdminOnly")]
        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email,Name,City,Password,Phone,IsAdmin,SecurityQues,SecurityAns")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            return View(member);
        }

        // POST: Members/Edit/5
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

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.UserId == id);
        }

        // GET: Members/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Members/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Member member)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingMember = await _context.Members
                    .FirstOrDefaultAsync(m => m.Email == member.Email);

                if (existingMember != null)
                {
                    ViewBag.ErrorMessage = "Email already exists.";
                    return View(member);
                }

                // Optionally hash the password before saving
                // member.Password = HashPassword(member.Password);

                // Set default value for IsAdmin if not provided
                member.IsAdmin = false;

                _context.Add(member);
                await _context.SaveChangesAsync();

                // Automatically log in after registration
                HttpContext.Session.SetInt32("UserId", member.UserId);
                HttpContext.Session.SetString("MemberName", member.Name);
                HttpContext.Session.SetInt32("IsAdmin", member.IsAdmin ? 1 : 0);

                TempData["RegistrationSuccessful"] = "Registration Successful";

                // Redirect to the Login page on successful registration
                return RedirectToAction("Login");
            }

            // Stay on the same page if registration fails
            return View(member);
        }

        // GET: Members/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Members/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Member member)
        {
            var user = await _context.Members.FirstOrDefaultAsync(m => m.Email == member.Email && m.Password == member.Password);

            if (user != null)
            {
                // Create claims and identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                };

                if (user.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Store user information in session
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("MemberName", user.Name);
                HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);

                TempData["LoginSuccessful"] = "Login successful.";

                // Redirect based on role
                if (user.IsAdmin)
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "UserDashboard");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Email or Password is incorrect.";
                return View();
            }
        }

        // GET: Members/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear session data
            HttpContext.Session.Clear();

            TempData["LogoutMessage"] = "You have been logged out.";
            return RedirectToAction("Login", "Members");
        }
    }
}
