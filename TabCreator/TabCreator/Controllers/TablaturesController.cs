using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TabCreator.Models;

namespace TabCreator.Controllers
{
    public class TablaturesController : Controller
    {
        private readonly TabCreatorContext _context;

        public TablaturesController(TabCreatorContext context)
        {
            _context = context;
        }

        // GET: Tablatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tablatures.ToListAsync());
        }

        // GET: Tablatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablature = await _context.Tablatures
                .FirstOrDefaultAsync(m => m.TablatureId == id);
            if (tablature == null)
            {
                return NotFound();
            }

            return View(tablature);
        }

        // GET: Tablatures/Create
        public IActionResult Create()
        {
			ViewBag.Users = new SelectList(_context.Users, "Id", "UserName");
			return View();
        }

        // POST: Tablatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TablatureId,UserId,UserTab")] Tablature tablature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", tablature.UserId);
			return View(tablature);
		}

        // GET: Tablatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablature = await _context.Tablatures.FindAsync(id);
            if (tablature == null)
            {
                return NotFound();
            }

            ViewBag.Users = new SelectList(_context.Users, "Id", "UserName");
            return View(tablature);
        }

        // POST: Tablatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TablatureId,UserId,UserTab")] Tablature tablature)
        {
            if (id != tablature.TablatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablatureExists(tablature.TablatureId))
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
            return View(tablature);
        }

        // GET: Tablatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablature = await _context.Tablatures
                .FirstOrDefaultAsync(m => m.TablatureId == id);
            if (tablature == null)
            {
                return NotFound();
            }

            return View(tablature);
        }

        // POST: Tablatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tablature = await _context.Tablatures.FindAsync(id);
            if (tablature != null)
            {
                _context.Tablatures.Remove(tablature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablatureExists(int id)
        {
            return _context.Tablatures.Any(e => e.TablatureId == id);
        }
    }
}
