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
    public class DatabaseLogsController : Controller
    {
        private readonly AdventureWorksContext _context;

        public DatabaseLogsController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: DatabaseLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.DatabaseLog.ToListAsync());
        }

        // GET: DatabaseLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseLog = await _context.DatabaseLog
                .SingleOrDefaultAsync(m => m.DatabaseLogId == id);
            if (databaseLog == null)
            {
                return NotFound();
            }

            return View(databaseLog);
        }

        // GET: DatabaseLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DatabaseLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DatabaseLogId,PostTime,DatabaseUser,Event,Schema,Object,Tsql,XmlEvent")] DatabaseLog databaseLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(databaseLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(databaseLog);
        }

        // GET: DatabaseLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseLog = await _context.DatabaseLog.SingleOrDefaultAsync(m => m.DatabaseLogId == id);
            if (databaseLog == null)
            {
                return NotFound();
            }
            return View(databaseLog);
        }

        // POST: DatabaseLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DatabaseLogId,PostTime,DatabaseUser,Event,Schema,Object,Tsql,XmlEvent")] DatabaseLog databaseLog)
        {
            if (id != databaseLog.DatabaseLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(databaseLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatabaseLogExists(databaseLog.DatabaseLogId))
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
            return View(databaseLog);
        }

        // GET: DatabaseLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseLog = await _context.DatabaseLog
                .SingleOrDefaultAsync(m => m.DatabaseLogId == id);
            if (databaseLog == null)
            {
                return NotFound();
            }

            return View(databaseLog);
        }

        // POST: DatabaseLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var databaseLog = await _context.DatabaseLog.SingleOrDefaultAsync(m => m.DatabaseLogId == id);
            _context.DatabaseLog.Remove(databaseLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatabaseLogExists(int id)
        {
            return _context.DatabaseLog.Any(e => e.DatabaseLogId == id);
        }
    }
}
