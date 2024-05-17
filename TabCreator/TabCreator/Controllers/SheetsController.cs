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
    public class SheetsController : Controller
    {
        private readonly TabCreatorContext _context;

        public SheetsController(TabCreatorContext context)
        {
            _context = context;
        }

        // GET: Sheets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sheets.ToListAsync());
        }

        // GET: Sheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheet = await _context.Sheets
                .FirstOrDefaultAsync(m => m.SheetId == id);
            if (sheet == null)
            {
                return NotFound();
            }

            return View(sheet);
        }

        // GET: Sheets/Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            return View();
        }

        // POST: Sheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SheetId,UserId,UserSheet")] Sheet sheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sheet);
        }

        // GET: Sheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheet = await _context.Sheets.FindAsync(id);
            if (sheet == null)
            {
                return NotFound();
            }
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            return View(sheet);
        }

        // POST: Sheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SheetId,UserId,UserSheet")] Sheet sheet)
        {
            if (id != sheet.SheetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheetExists(sheet.SheetId))
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
            return View(sheet);
        }

        // GET: Sheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheet = await _context.Sheets
                .FirstOrDefaultAsync(m => m.SheetId == id);
            if (sheet == null)
            {
                return NotFound();
            }

            return View(sheet);
        }

        // POST: Sheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sheet = await _context.Sheets.FindAsync(id);
            if (sheet != null)
            {
                _context.Sheets.Remove(sheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheetExists(int id)
        {
            return _context.Sheets.Any(e => e.SheetId == id);
        }
    }
}
