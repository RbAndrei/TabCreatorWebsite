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
    public class ChordsController : Controller
    {
        private readonly TabCreatorContext _context;

        public ChordsController(TabCreatorContext context)
        {
            _context = context;
        }

        // GET: Chords
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chords.ToListAsync());
        }

        // GET: Chords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chords = await _context.Chords
                .FirstOrDefaultAsync(m => m.ChordsId == id);
            if (chords == null)
            {
                return NotFound();
            }

            return View(chords);
        }

        // GET: Chords/Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            return View();
        }

        // POST: Chords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChordsId,UserId,UserChord")] Chords chords)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chords);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chords);
        }

        // GET: Chords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chords = await _context.Chords.FindAsync(id);
            if (chords == null)
            {
                return NotFound();
            }
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            return View(chords);
        }

        // POST: Chords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChordsId,UserId,UserChord")] Chords chords)
        {
            if (id != chords.ChordsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chords);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChordsExists(chords.ChordsId))
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
            return View(chords);
        }

        // GET: Chords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chords = await _context.Chords
                .FirstOrDefaultAsync(m => m.ChordsId == id);
            if (chords == null)
            {
                return NotFound();
            }

            return View(chords);
        }

        // POST: Chords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chords = await _context.Chords.FindAsync(id);
            if (chords != null)
            {
                _context.Chords.Remove(chords);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChordsExists(int id)
        {
            return _context.Chords.Any(e => e.ChordsId == id);
        }
    }
}
