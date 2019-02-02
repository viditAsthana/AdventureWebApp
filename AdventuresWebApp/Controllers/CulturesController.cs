using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdventuresWebApp.Models;

namespace AdventuresWebApp.Controllers
{
    public class CulturesController : Controller
    {
        private readonly AdventureWorksContext _context;

        public CulturesController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: Cultures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Culture.ToListAsync());
        }

        // GET: Cultures/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = await _context.Culture
                .SingleOrDefaultAsync(m => m.CultureId == id);
            if (culture == null)
            {
                return NotFound();
            }

            return View(culture);
        }

        // GET: Cultures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cultures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CultureId,Name,ModifiedDate")] Culture culture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(culture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(culture);
        }

        // GET: Cultures/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = await _context.Culture.SingleOrDefaultAsync(m => m.CultureId == id);
            if (culture == null)
            {
                return NotFound();
            }
            return View(culture);
        }

        // POST: Cultures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CultureId,Name,ModifiedDate")] Culture culture)
        {
            if (id != culture.CultureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(culture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CultureExists(culture.CultureId))
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
            return View(culture);
        }

        // GET: Cultures/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = await _context.Culture
                .SingleOrDefaultAsync(m => m.CultureId == id);
            if (culture == null)
            {
                return NotFound();
            }

            return View(culture);
        }

        // POST: Cultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var culture = await _context.Culture.SingleOrDefaultAsync(m => m.CultureId == id);
            _context.Culture.Remove(culture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CultureExists(string id)
        {
            return _context.Culture.Any(e => e.CultureId == id);
        }
    }
}
