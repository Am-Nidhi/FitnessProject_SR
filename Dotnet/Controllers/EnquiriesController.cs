using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessFinal.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class EnquiriesController : Controller
    {
        private readonly FinalContext _context;

        public EnquiriesController(FinalContext context)
        {
            _context = context;
        }

        // GET: Enquiries
        public async Task<IActionResult> Index()
        {
            var finalContext = _context.Enquiries.Include(e => e.User);
            return View(await finalContext.ToListAsync());
        }

        // GET: Enquiries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiries
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Enqid == id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return View(enquiry);
        }

        // GET: Enquiries/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Members, "UserId", "Name");
            return View();
        }

        //POST : Enquiries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Enqid,UserId,Address,City,Email,Fee,Gympackage,Status")] Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the Member entity based on the UserId
                    var member = await _context.Members.FindAsync(enquiry.UserId);
                    if (member != null)
                    {
                        // Assign the User property
                        enquiry.User = member;

                        _context.Add(enquiry);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "User not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Log detailed information
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }

            // Reloading the ViewBag.UserId in case of error
            ViewData["UserId"] = new SelectList(_context.Members, "UserId", "Name", enquiry.UserId);
            return View(enquiry);
        }


        // GET: Enquiries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Members, "UserId", "Name", enquiry.UserId);
            return View(enquiry);
        }

        // POST: Enquiries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Enqid,UserId,Address,City,Email,Fee,Gympackage,Status")] Enquiry enquiry)
        {
            if (id != enquiry.Enqid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enquiry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnquiryExists(enquiry.Enqid))
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
            ViewData["UserId"] = new SelectList(_context.Members, "UserId", "Name", enquiry.UserId);
            return View(enquiry);
        }

        // GET: Enquiries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiries
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Enqid == id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return View(enquiry);
        }

        // POST: Enquiries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry != null)
            {
                _context.Enquiries.Remove(enquiry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnquiryExists(int id)
        {
            return _context.Enquiries.Any(e => e.Enqid == id);
        }
    }
}
