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
    public class CountryRegionCurrenciesController : Controller
    {
        private readonly AdventureWorksContext _context;

        public CountryRegionCurrenciesController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: CountryRegionCurrencies
        public async Task<IActionResult> Index()
        {
            var adventureWorksContext = _context.CountryRegionCurrency.Include(c => c.CountryRegionCodeNavigation).Include(c => c.CurrencyCodeNavigation);
            return View(await adventureWorksContext.ToListAsync());
        }

        // GET: CountryRegionCurrencies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryRegionCurrency = await _context.CountryRegionCurrency
                .Include(c => c.CountryRegionCodeNavigation)
                .Include(c => c.CurrencyCodeNavigation)
                .SingleOrDefaultAsync(m => m.CountryRegionCode == id);
            if (countryRegionCurrency == null)
            {
                return NotFound();
            }

            return View(countryRegionCurrency);
        }

        // GET: CountryRegionCurrencies/Create
        public IActionResult Create()
        {
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode");
            ViewData["CurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode");
            return View();
        }

        // POST: CountryRegionCurrencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryRegionCode,CurrencyCode,ModifiedDate")] CountryRegionCurrency countryRegionCurrency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countryRegionCurrency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode", countryRegionCurrency.CountryRegionCode);
            ViewData["CurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", countryRegionCurrency.CurrencyCode);
            return View(countryRegionCurrency);
        }

        // GET: CountryRegionCurrencies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryRegionCurrency = await _context.CountryRegionCurrency.SingleOrDefaultAsync(m => m.CountryRegionCode == id);
            if (countryRegionCurrency == null)
            {
                return NotFound();
            }
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode", countryRegionCurrency.CountryRegionCode);
            ViewData["CurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", countryRegionCurrency.CurrencyCode);
            return View(countryRegionCurrency);
        }

        // POST: CountryRegionCurrencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CountryRegionCode,CurrencyCode,ModifiedDate")] CountryRegionCurrency countryRegionCurrency)
        {
            if (id != countryRegionCurrency.CountryRegionCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryRegionCurrency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryRegionCurrencyExists(countryRegionCurrency.CountryRegionCode))
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
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode", countryRegionCurrency.CountryRegionCode);
            ViewData["CurrencyCode"] = new SelectList(_context.Currency, "CurrencyCode", "CurrencyCode", countryRegionCurrency.CurrencyCode);
            return View(countryRegionCurrency);
        }

        // GET: CountryRegionCurrencies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryRegionCurrency = await _context.CountryRegionCurrency
                .Include(c => c.CountryRegionCodeNavigation)
                .Include(c => c.CurrencyCodeNavigation)
                .SingleOrDefaultAsync(m => m.CountryRegionCode == id);
            if (countryRegionCurrency == null)
            {
                return NotFound();
            }

            return View(countryRegionCurrency);
        }

        // POST: CountryRegionCurrencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var countryRegionCurrency = await _context.CountryRegionCurrency.SingleOrDefaultAsync(m => m.CountryRegionCode == id);
            _context.CountryRegionCurrency.Remove(countryRegionCurrency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryRegionCurrencyExists(string id)
        {
            return _context.CountryRegionCurrency.Any(e => e.CountryRegionCode == id);
        }
    }
}
