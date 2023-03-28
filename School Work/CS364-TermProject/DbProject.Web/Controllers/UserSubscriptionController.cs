using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbProject.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace DbProject.Web.Controllers
{
    public class UserSubscriptionController : Controller
    {
        private readonly SubscriptionServicesContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserSubscriptionController(SubscriptionServicesContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserSubscription
        public async Task<IActionResult> Index()
        {
            var subscriptionServiceContext = _context.UserSubscriptions;
            return View(await subscriptionServiceContext.ToListAsync());
        }

        // GET: UserSubscription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserSubscriptions == null)
            {
                return NotFound();
            }

            var userSubscription = await _context.UserSubscriptions
                //.Include(u => u.Subscription)
                //.Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubscription == null)
            {
                return NotFound();
            }

            return View(userSubscription);
        }

        // GET: UserSubscription/Create
        public IActionResult Create()
        {
            Repository.Clear();
            foreach (var sub in _context.Subscriptions) {
                Repository.AddSub(sub);
            }
            foreach (var comp in _context.Companies) {
                Repository.AddCompany(comp);
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserSubscription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubscriptionId,UserId,StartDate,EndDate")] UserSubscription userSubscription)
        {
            if (_userManager.GetUserName(User) != null) {
                    var userEmail = _userManager.GetUserName(User);
                    var user = from m in _context.Users where m.Email == userEmail select m;
                    List<User> currUser = new List<User>();
                    foreach (var u in user) {
                        currUser.Add(u);
                    }
                    var currentUser = currUser[0];
            userSubscription.UserId = currentUser.Id;
            if (ModelState.IsValid)
            {
                _context.Add(userSubscription);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UserSubs");
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", userSubscription.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSubscription.UserId);
            return View(userSubscription);
            }   else {
                return NotFound();
            }
        }

        // GET: UserSubscription/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserSubscriptions == null)
            {
                return NotFound();
            }

            var userSubscription = await _context.UserSubscriptions.FindAsync(id);
            if (userSubscription == null)
            {
                return NotFound();
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", userSubscription.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSubscription.UserId);
            return View(userSubscription);
        }

        // POST: UserSubscription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubscriptionId,UserId,StartDate,EndDate")] UserSubscription userSubscription)
        {
            if (id != userSubscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSubscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSubscriptionExists(userSubscription.Id))
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
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", userSubscription.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSubscription.UserId);
            return View(userSubscription);
        }

        // GET: UserSubscription/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserSubscriptions == null)
            {
                return NotFound();
            }

            var userSubscription = await _context.UserSubscriptions
                //.Include(u => u.Subscription)
                //.Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubscription == null)
            {
                return NotFound();
            }

            return View(userSubscription);
        }

        // POST: UserSubscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserSubscriptions == null)
            {
                return Problem("Entity set 'SubscriptionServicesContext.UserSubscription'  is null.");
            }
            var userSubscription = await _context.UserSubscriptions.FindAsync(id);
            if (userSubscription != null)
            {
                _context.UserSubscriptions.Remove(userSubscription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "UserSubs");
        }

        private bool UserSubscriptionExists(int id)
        {
          return (_context.UserSubscriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
