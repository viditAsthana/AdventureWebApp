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
    public class BusinessEntityAddressesController : Controller
    {
        private readonly AdventureWorksContext _context;

        public BusinessEntityAddressesController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: BusinessEntityAddresses
        public async Task<IActionResult> Index()
        {
            var adventureWorksContext = _context.BusinessEntityAddress.Include(b => b.Address).Include(b => b.AddressType).Include(b => b.BusinessEntity);
            return View(await adventureWorksContext.ToListAsync());
        }

        // GET: BusinessEntityAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityAddress = await _context.BusinessEntityAddress
                .Include(b => b.Address)
                .Include(b => b.AddressType)
                .Include(b => b.BusinessEntity)
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1");
            ViewData["AddressTypeId"] = new SelectList(_context.AddressType, "AddressTypeId", "Name");
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId");
            return View();
        }

        // POST: BusinessEntityAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,AddressId,AddressTypeId,Rowguid,ModifiedDate")] BusinessEntityAddress businessEntityAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessEntityAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", businessEntityAddress.AddressId);
            ViewData["AddressTypeId"] = new SelectList(_context.AddressType, "AddressTypeId", "Name", businessEntityAddress.AddressTypeId);
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId", businessEntityAddress.BusinessEntityId);
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityAddress = await _context.BusinessEntityAddress.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", businessEntityAddress.AddressId);
            ViewData["AddressTypeId"] = new SelectList(_context.AddressType, "AddressTypeId", "Name", businessEntityAddress.AddressTypeId);
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId", businessEntityAddress.BusinessEntityId);
            return View(businessEntityAddress);
        }

        // POST: BusinessEntityAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,AddressId,AddressTypeId,Rowguid,ModifiedDate")] BusinessEntityAddress businessEntityAddress)
        {
            if (id != businessEntityAddress.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessEntityAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessEntityAddressExists(businessEntityAddress.BusinessEntityId))
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
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", businessEntityAddress.AddressId);
            ViewData["AddressTypeId"] = new SelectList(_context.AddressType, "AddressTypeId", "Name", businessEntityAddress.AddressTypeId);
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId", businessEntityAddress.BusinessEntityId);
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityAddress = await _context.BusinessEntityAddress
                .Include(b => b.Address)
                .Include(b => b.AddressType)
                .Include(b => b.BusinessEntity)
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            return View(businessEntityAddress);
        }

        // POST: BusinessEntityAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessEntityAddress = await _context.BusinessEntityAddress.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            _context.BusinessEntityAddress.Remove(businessEntityAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessEntityAddressExists(int id)
        {
            return _context.BusinessEntityAddress.Any(e => e.BusinessEntityId == id);
        }
    }
}
