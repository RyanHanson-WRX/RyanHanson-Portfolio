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
    public class UserSubStatController : Controller
    {
        private readonly SubscriptionServicesContext _context;

        public UserSubStatController(SubscriptionServicesContext context)
        {
            _context = context;
        }

        // GET: UserSubStat
        public async Task<IActionResult> Index()
        {
            return _context.UserSubStats != null ? 
                    View(await _context.UserSubStats.ToListAsync()) :
                    Problem("Entity set 'SubscriptionServicesContext.Users'  is null.");
        }

        // GET: UserSubStat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserSubStats == null)
            {
                return NotFound();
            }

            var userSubStat = await _context.UserSubStats
                //.Include(u => u.UserSub)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubStat == null)
            {
                return NotFound();
            }

            return View(userSubStat);
        }

        // GET: UserSubStat/Create
        public IActionResult Create()
        {
            ViewData["UserSubId"] = new SelectList(_context.Set<UserSubscription>(), "Id", "Id");
            return View();
        }

        // POST: UserSubStat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserSubId,YearlyCost,RunningTotal")] UserSubStat userSubStat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSubStat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserSubId"] = new SelectList(_context.Set<UserSubscription>(), "Id", "Id", userSubStat.UserSubId);
            return View(userSubStat);
        }

        // GET: UserSubStat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserSubStats == null)
            {
                return NotFound();
            }

            var userSubStat = await _context.UserSubStats.FindAsync(id);
            if (userSubStat == null)
            {
                return NotFound();
            }
            ViewData["UserSubId"] = new SelectList(_context.Set<UserSubscription>(), "Id", "Id", userSubStat.UserSubId);
            return View(userSubStat);
        }

        // POST: UserSubStat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserSubId,YearlyCost,RunningTotal")] UserSubStat userSubStat)
        {
            if (id != userSubStat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSubStat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSubStatExists(userSubStat.Id))
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
            ViewData["UserSubId"] = new SelectList(_context.Set<UserSubscription>(), "Id", "Id", userSubStat.UserSubId);
            return View(userSubStat);
        }

        // GET: UserSubStat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserSubStats == null)
            {
                return NotFound();
            }

            var userSubStat = await _context.UserSubStats
                //.Include(u => u.UserSub)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubStat == null)
            {
                return NotFound();
            }

            return View(userSubStat);
        }

        // POST: UserSubStat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserSubStats == null)
            {
                return Problem("Entity set 'SubscriptionServicesContext.UserSubStat'  is null.");
            }
            var userSubStat = await _context.UserSubStats.FindAsync(id);
            if (userSubStat != null)
            {
                _context.UserSubStats.Remove(userSubStat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSubStatExists(int id)
        {
          return (_context.UserSubStats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
