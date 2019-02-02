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
    public class CurrencyRatesController : Controller
    {
        private readonly AdventureWorksContext _context;

        public CurrencyRatesController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: CurrencyRates
        public async Task<IActionResult> Index()
        {
            var adventureWorksContext = _context.CurrencyRate.Include(c => c.FromCurrencyCodeNavigation).Include(c => c.ToCurrencyCodeNavigation);
            return View(await adventureWorksContext.ToListAsync());
        }

        // GET: CurrencyRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyRate = await _context.CurrencyRate
                .Include(c => c.FromCurrencyCodeNavigation)
                .Include(c => c.ToCurrencyCodeNavigation)
                .SingleOrDefaultAsync(m => m.CurrencyRateId == id);
            if (currencyRate == null)
            {
                return NotFound();
            }

            return View(currencyRate);
        }

        // GET: CurrencyRates/Create
        public IActionResult Create()
        {
            ViewData["FromCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode");
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode");
            return View();
        }

        // POST: CurrencyRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrencyRateId,CurrencyRateDate,FromCurrencyCode,ToCurrencyCode,AverageRate,EndOfDayRate,ModifiedDate")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currencyRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", currencyRate.FromCurrencyCode);
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", currencyRate.ToCurrencyCode);
            return View(currencyRate);
        }

        // GET: CurrencyRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyRate = await _context.CurrencyRate.SingleOrDefaultAsync(m => m.CurrencyRateId == id);
            if (currencyRate == null)
            {
                return NotFound();
            }
            ViewData["FromCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", currencyRate.FromCurrencyCode);
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", currencyRate.ToCurrencyCode);
            return View(currencyRate);
        }

        // POST: CurrencyRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurrencyRateId,CurrencyRateDate,FromCurrencyCode,ToCurrencyCode,AverageRate,EndOfDayRate,ModifiedDate")] CurrencyRate currencyRate)
        {
            if (id != currencyRate.CurrencyRateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currencyRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyRateExists(currencyRate.CurrencyRateId))
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
            ViewData["FromCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", currencyRate.FromCurrencyCode);
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", currencyRate.ToCurrencyCode);
            return View(currencyRate);
        }

        // GET: CurrencyRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyRate = await _context.CurrencyRate
                .Include(c => c.FromCurrencyCodeNavigation)
                .Include(c => c.ToCurrencyCodeNavigation)
                .SingleOrDefaultAsync(m => m.CurrencyRateId == id);
            if (currencyRate == null)
            {
                return NotFound();
            }

            return View(currencyRate);
        }

        // POST: CurrencyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currencyRate = await _context.CurrencyRate.SingleOrDefaultAsync(m => m.CurrencyRateId == id);
            _context.CurrencyRate.Remove(currencyRate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyRateExists(int id)
        {
            return _context.CurrencyRate.Any(e => e.CurrencyRateId == id);
        }
    }
}
