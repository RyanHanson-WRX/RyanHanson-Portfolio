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
    public class UserTotalStatController : Controller
    {
        private readonly SubscriptionServicesContext _context;

        public UserTotalStatController(SubscriptionServicesContext context)
        {
            _context = context;
        }

        // GET: UserTotalStat
        public async Task<IActionResult> Index()
        {
            var subscriptionServiceContext = _context.UserTotalStats;
            return View(await subscriptionServiceContext.ToListAsync());
        }

        // GET: UserTotalStat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserTotalStats == null)
            {
                return NotFound();
            }

            var userTotalStat = await _context.UserTotalStats
                //.Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTotalStat == null)
            {
                return NotFound();
            }

            return View(userTotalStat);
        }

        // GET: UserTotalStat/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserTotalStat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TotalSubYearlyCost,AllSubRunningTotal")] UserTotalStat userTotalStat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTotalStat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTotalStat.UserId);
            return View(userTotalStat);
        }

        // GET: UserTotalStat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserTotalStats == null)
            {
                return NotFound();
            }

            var userTotalStat = await _context.UserTotalStats.FindAsync(id);
            if (userTotalStat == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTotalStat.UserId);
            return View(userTotalStat);
        }

        // POST: UserTotalStat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TotalSubYearlyCost,AllSubRunningTotal")] UserTotalStat userTotalStat)
        {
            if (id != userTotalStat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTotalStat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTotalStatExists(userTotalStat.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTotalStat.UserId);
            return View(userTotalStat);
        }

        // GET: UserTotalStat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserTotalStats == null)
            {
                return NotFound();
            }

            var userTotalStat = await _context.UserTotalStats
                //.Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTotalStat == null)
            {
                return NotFound();
            }

            return View(userTotalStat);
        }

        // POST: UserTotalStat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserTotalStats == null)
            {
                return Problem("Entity set 'SubscriptionServicesContext.UserTotalStat'  is null.");
            }
            var userTotalStat = await _context.UserTotalStats.FindAsync(id);
            if (userTotalStat != null)
            {
                _context.UserTotalStats.Remove(userTotalStat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTotalStatExists(int id)
        {
          return (_context.UserTotalStats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
