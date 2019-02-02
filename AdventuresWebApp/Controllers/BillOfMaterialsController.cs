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
    public class BillOfMaterialsController : Controller
    {
        private readonly AdventureWorksContext _context;

        public BillOfMaterialsController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: BillOfMaterials
        public async Task<IActionResult> Index()
        {
            var adventureWorksContext = _context.BillOfMaterials.Include(b => b.Component).Include(b => b.ProductAssembly).Include(b => b.UnitMeasureCodeNavigation);
            return View(await adventureWorksContext.ToListAsync());
        }

        // GET: BillOfMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterials = await _context.BillOfMaterials
                .Include(b => b.Component)
                .Include(b => b.ProductAssembly)
                .Include(b => b.UnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.BillOfMaterialsId == id);
            if (billOfMaterials == null)
            {
                return NotFound();
            }

            return View(billOfMaterials);
        }

        // GET: BillOfMaterials/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["ProductAssemblyId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: BillOfMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillOfMaterialsId,ProductAssemblyId,ComponentId,StartDate,EndDate,UnitMeasureCode,Bomlevel,PerAssemblyQty,ModifiedDate")] BillOfMaterials billOfMaterials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billOfMaterials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Product, "ProductId", "Name", billOfMaterials.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Product, "ProductId", "Name", billOfMaterials.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", billOfMaterials.UnitMeasureCode);
            return View(billOfMaterials);
        }

        // GET: BillOfMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterials = await _context.BillOfMaterials.SingleOrDefaultAsync(m => m.BillOfMaterialsId == id);
            if (billOfMaterials == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Product, "ProductId", "Name", billOfMaterials.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Product, "ProductId", "Name", billOfMaterials.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", billOfMaterials.UnitMeasureCode);
            return View(billOfMaterials);
        }

        // POST: BillOfMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillOfMaterialsId,ProductAssemblyId,ComponentId,StartDate,EndDate,UnitMeasureCode,Bomlevel,PerAssemblyQty,ModifiedDate")] BillOfMaterials billOfMaterials)
        {
            if (id != billOfMaterials.BillOfMaterialsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billOfMaterials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillOfMaterialsExists(billOfMaterials.BillOfMaterialsId))
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
            ViewData["ComponentId"] = new SelectList(_context.Product, "ProductId", "Name", billOfMaterials.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Product, "ProductId", "Name", billOfMaterials.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", billOfMaterials.UnitMeasureCode);
            return View(billOfMaterials);
        }

        // GET: BillOfMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterials = await _context.BillOfMaterials
                .Include(b => b.Component)
                .Include(b => b.ProductAssembly)
                .Include(b => b.UnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.BillOfMaterialsId == id);
            if (billOfMaterials == null)
            {
                return NotFound();
            }

            return View(billOfMaterials);
        }

        // POST: BillOfMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billOfMaterials = await _context.BillOfMaterials.SingleOrDefaultAsync(m => m.BillOfMaterialsId == id);
            _context.BillOfMaterials.Remove(billOfMaterials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillOfMaterialsExists(int id)
        {
            return _context.BillOfMaterials.Any(e => e.BillOfMaterialsId == id);
        }
    }
}
