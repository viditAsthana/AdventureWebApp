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
    public class AwbuildVersionsController : Controller
    {
        private readonly AdventureWorksContext _context;

        public AwbuildVersionsController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: AwbuildVersions
        public async Task<IActionResult> Index()
        {
            return View(await _context.AwbuildVersion.ToListAsync());
        }

        // GET: AwbuildVersions/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awbuildVersion = await _context.AwbuildVersion
                .SingleOrDefaultAsync(m => m.SystemInformationId == id);
            if (awbuildVersion == null)
            {
                return NotFound();
            }

            return View(awbuildVersion);
        }

        // GET: AwbuildVersions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AwbuildVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SystemInformationId,DatabaseVersion,VersionDate,ModifiedDate")] AwbuildVersion awbuildVersion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(awbuildVersion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(awbuildVersion);
        }

        // GET: AwbuildVersions/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awbuildVersion = await _context.AwbuildVersion.SingleOrDefaultAsync(m => m.SystemInformationId == id);
            if (awbuildVersion == null)
            {
                return NotFound();
            }
            return View(awbuildVersion);
        }

        // POST: AwbuildVersions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("SystemInformationId,DatabaseVersion,VersionDate,ModifiedDate")] AwbuildVersion awbuildVersion)
        {
            if (id != awbuildVersion.SystemInformationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(awbuildVersion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwbuildVersionExists(awbuildVersion.SystemInformationId))
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
            return View(awbuildVersion);
        }

        // GET: AwbuildVersions/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awbuildVersion = await _context.AwbuildVersion
                .SingleOrDefaultAsync(m => m.SystemInformationId == id);
            if (awbuildVersion == null)
            {
                return NotFound();
            }

            return View(awbuildVersion);
        }

        // POST: AwbuildVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var awbuildVersion = await _context.AwbuildVersion.SingleOrDefaultAsync(m => m.SystemInformationId == id);
            _context.AwbuildVersion.Remove(awbuildVersion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwbuildVersionExists(byte id)
        {
            return _context.AwbuildVersion.Any(e => e.SystemInformationId == id);
        }
    }
}
