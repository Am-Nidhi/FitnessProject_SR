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
    public class MembershiptypesController : Controller
    {
        private readonly FinalContext _context;

        public MembershiptypesController(FinalContext context)
        {
            _context = context;
        }

        // GET: Membershiptypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Membershiptypes.ToListAsync());
        }

        // GET: Membershiptypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.Membershiptypes
                .FirstOrDefaultAsync(m => m.MembershipId == id);
            if (membershiptype == null)
            {
                return NotFound();
            }

            return View(membershiptype);
        }

        // GET: Membershiptypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membershiptypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipId,TypeName,Description,Fee")] Membershiptype membershiptype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershiptype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershiptype);
        }

        // GET: Membershiptypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.Membershiptypes.FindAsync(id);
            if (membershiptype == null)
            {
                return NotFound();
            }
            return View(membershiptype);
        }

        // POST: Membershiptypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipId,TypeName,Description,Fee")] Membershiptype membershiptype)
        {
            if (id != membershiptype.MembershipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershiptype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershiptypeExists(membershiptype.MembershipId))
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
            return View(membershiptype);
        }

        // GET: Membershiptypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershiptype = await _context.Membershiptypes
                .FirstOrDefaultAsync(m => m.MembershipId == id);
            if (membershiptype == null)
            {
                return NotFound();
            }

            return View(membershiptype);
        }

        // POST: Membershiptypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershiptype = await _context.Membershiptypes.FindAsync(id);
            if (membershiptype != null)
            {
                _context.Membershiptypes.Remove(membershiptype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershiptypeExists(int id)
        {
            return _context.Membershiptypes.Any(e => e.MembershipId == id);
        }
    }
}
