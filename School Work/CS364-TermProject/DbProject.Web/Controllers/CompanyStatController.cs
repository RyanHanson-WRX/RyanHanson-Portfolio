using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbProject.Web.Models;

namespace DbProject.Web.Controllers
{
    public class CompanyStatController : Controller
    {
        private readonly SubscriptionServicesContext _context;

        public CompanyStatController(SubscriptionServicesContext context)
        {
            _context = context;
        }

        // GET: CompanyStat
        public async Task<IActionResult> Index()
        {
            var subscriptionServiceContext = _context.CompanyStats;
            return View(await subscriptionServiceContext.ToListAsync());
        }

        // GET: CompanyStat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompanyStats == null)
            {
                return NotFound();
            }

            var companyStat = await _context.CompanyStats
                //.Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyStat == null)
            {
                return NotFound();
            }

            return View(companyStat);
        }

        // GET: CompanyStat/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        // POST: CompanyStat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,YearlyEarnings,ActiveSubCount,CancelledSubCount")] CompanyStat companyStat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyStat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", companyStat.CompanyId);
            return View(companyStat);
        }

        // GET: CompanyStat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompanyStats == null)
            {
                return NotFound();
            }

            var companyStat = await _context.CompanyStats.FindAsync(id);
            if (companyStat == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", companyStat.CompanyId);
            return View(companyStat);
        }

        // POST: CompanyStat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,YearlyEarnings,ActiveSubCount,CancelledSubCount")] CompanyStat companyStat)
        {
            if (id != companyStat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyStat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyStatExists(companyStat.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", companyStat.CompanyId);
            return View(companyStat);
        }

        // GET: CompanyStat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompanyStats == null)
            {
                return NotFound();
            }

            var companyStat = await _context.CompanyStats
                //.Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyStat == null)
            {
                return NotFound();
            }

            return View(companyStat);
        }

        // POST: CompanyStat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompanyStats == null)
            {
                return Problem("Entity set 'SubscriptionServicesContext.CompanyStat'  is null.");
            }
            var companyStat = await _context.CompanyStats.FindAsync(id);
            if (companyStat != null)
            {
                _context.CompanyStats.Remove(companyStat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyStatExists(int id)
        {
          return (_context.CompanyStats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
