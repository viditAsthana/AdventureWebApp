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
    public class SalesTerritoriesController : Controller
    {
        private readonly AdventureWorksContext _context;

        public SalesTerritoriesController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: SalesTerritories
        public async Task<IActionResult> Index()
        {
            var adventureWorksContext = _context.SalesTerritory.Include(s => s.CountryRegionCodeNavigation);
            return View(await adventureWorksContext.ToListAsync());
        }

        // GET: SalesTerritories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritory = await _context.SalesTerritory
                .Include(s => s.CountryRegionCodeNavigation)
                .SingleOrDefaultAsync(m => m.TerritoryId == id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            return View(salesTerritory);
        }

        // GET: SalesTerritories/Create
        public IActionResult Create()
        {
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode");
            return View();
        }

        // POST: SalesTerritories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerritoryId,Name,CountryRegionCode,Group,SalesYtd,SalesLastYear,CostYtd,CostLastYear,Rowguid,ModifiedDate")] SalesTerritory salesTerritory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesTerritory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode", salesTerritory.CountryRegionCode);
            return View(salesTerritory);
        }

        // GET: SalesTerritories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritory = await _context.SalesTerritory.SingleOrDefaultAsync(m => m.TerritoryId == id);
            if (salesTerritory == null)
            {
                return NotFound();
            }
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode", salesTerritory.CountryRegionCode);
            return View(salesTerritory);
        }

        // POST: SalesTerritories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TerritoryId,Name,CountryRegionCode,Group,SalesYtd,SalesLastYear,CostYtd,CostLastYear,Rowguid,ModifiedDate")] SalesTerritory salesTerritory)
        {
            if (id != salesTerritory.TerritoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesTerritory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTerritoryExists(salesTerritory.TerritoryId))
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
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegion, "CountryRegionCode", "CountryRegionCode", salesTerritory.CountryRegionCode);
            return View(salesTerritory);
        }

        // GET: SalesTerritories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritory = await _context.SalesTerritory
                .Include(s => s.CountryRegionCodeNavigation)
                .SingleOrDefaultAsync(m => m.TerritoryId == id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            return View(salesTerritory);
        }

        // POST: SalesTerritories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesTerritory = await _context.SalesTerritory.SingleOrDefaultAsync(m => m.TerritoryId == id);
            _context.SalesTerritory.Remove(salesTerritory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesTerritoryExists(int id)
        {
            return _context.SalesTerritory.Any(e => e.TerritoryId == id);
        }
    }
}
