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
    public class AddressTypesController : Controller
    {
        private readonly AdventureWorksContext _context;

        public AddressTypesController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: AddressTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddressType.ToListAsync());
        }

        // GET: AddressTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressType
                .SingleOrDefaultAsync(m => m.AddressTypeId == id);
            if (addressType == null)
            {
                return NotFound();
            }

            return View(addressType);
        }

        // GET: AddressTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressTypeId,Name,Rowguid,ModifiedDate")] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addressType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        // GET: AddressTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressType.SingleOrDefaultAsync(m => m.AddressTypeId == id);
            if (addressType == null)
            {
                return NotFound();
            }
            return View(addressType);
        }

        // POST: AddressTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressTypeId,Name,Rowguid,ModifiedDate")] AddressType addressType)
        {
            if (id != addressType.AddressTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressTypeExists(addressType.AddressTypeId))
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
            return View(addressType);
        }

        // GET: AddressTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressType
                .SingleOrDefaultAsync(m => m.AddressTypeId == id);
            if (addressType == null)
            {
                return NotFound();
            }

            return View(addressType);
        }

        // POST: AddressTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addressType = await _context.AddressType.SingleOrDefaultAsync(m => m.AddressTypeId == id);
            _context.AddressType.Remove(addressType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressTypeExists(int id)
        {
            return _context.AddressType.Any(e => e.AddressTypeId == id);
        }
    }
}
