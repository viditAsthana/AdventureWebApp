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
    public class BusinessEntityContactsController : Controller
    {
        private readonly AdventureWorksContext _context;

        public BusinessEntityContactsController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: BusinessEntityContacts
        public async Task<IActionResult> Index()
        {
            var adventureWorksContext = _context.BusinessEntityContact.Include(b => b.BusinessEntity).Include(b => b.ContactType).Include(b => b.Person);
            return View(await adventureWorksContext.ToListAsync());
        }

        // GET: BusinessEntityContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityContact = await _context.BusinessEntityContact
                .Include(b => b.BusinessEntity)
                .Include(b => b.ContactType)
                .Include(b => b.Person)
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId");
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "ContactTypeId", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName");
            return View();
        }

        // POST: BusinessEntityContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,PersonId,ContactTypeId,Rowguid,ModifiedDate")] BusinessEntityContact businessEntityContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessEntityContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId", businessEntityContact.BusinessEntityId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "ContactTypeId", "Name", businessEntityContact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName", businessEntityContact.PersonId);
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityContact = await _context.BusinessEntityContact.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityContact == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId", businessEntityContact.BusinessEntityId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "ContactTypeId", "Name", businessEntityContact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName", businessEntityContact.PersonId);
            return View(businessEntityContact);
        }

        // POST: BusinessEntityContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,PersonId,ContactTypeId,Rowguid,ModifiedDate")] BusinessEntityContact businessEntityContact)
        {
            if (id != businessEntityContact.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessEntityContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessEntityContactExists(businessEntityContact.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntity, "BusinessEntityId", "BusinessEntityId", businessEntityContact.BusinessEntityId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "ContactTypeId", "Name", businessEntityContact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.Person, "BusinessEntityId", "FirstName", businessEntityContact.PersonId);
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityContact = await _context.BusinessEntityContact
                .Include(b => b.BusinessEntity)
                .Include(b => b.ContactType)
                .Include(b => b.Person)
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            return View(businessEntityContact);
        }

        // POST: BusinessEntityContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessEntityContact = await _context.BusinessEntityContact.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            _context.BusinessEntityContact.Remove(businessEntityContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessEntityContactExists(int id)
        {
            return _context.BusinessEntityContact.Any(e => e.BusinessEntityId == id);
        }
    }
}
