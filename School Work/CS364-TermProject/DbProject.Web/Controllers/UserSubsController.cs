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
    public class UserSubsController : Controller
    {
        private readonly SubscriptionServicesContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserSubsController(SubscriptionServicesContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: UserSubscription
        public ActionResult Index()
        {       if (_userManager.GetUserName(User) != null) {
                    List<UserSubFormatted> userSubFormat = new List<UserSubFormatted>();
                    var userEmail = _userManager.GetUserName(User);
                    var user = from m in _context.Users where m.Email == userEmail select m;
                    List<User> currUser = new List<User>();
                    foreach (var u in user) {
                        currUser.Add(u);
                    }
                    var currentUser = currUser[0];
                    var userSubs = from m in _context.UserSubscriptions where m.UserId == currentUser.Id select m;
                    List<UserSubscription> userSubList = new List<UserSubscription>();
                    foreach (var us in userSubs) {
                        userSubList.Add(us);
                    }
                    foreach (var userSub in userSubList){
                        var subscription = from m in _context.Subscriptions where m.Id == userSub.SubscriptionId select m;
                        List<Subscription> currSub = new List<Subscription>();
                        foreach (var sub in subscription){
                            currSub.Add(sub);
                        }
                        var company = from m in _context.Companies where m.Id == currSub[0].CompanyId select m;
                        List<Company> currCompany = new List<Company>();
                        foreach (var comp in company){
                            currCompany.Add(comp);
                        }
                        var userSubStats = from m in _context.UserSubStats where m.UserSubId == userSub.Id select m;
                        List<UserSubStat> currSubStat = new List<UserSubStat>();
                        foreach (var stat in userSubStats){
                            currSubStat.Add(stat);
                        }
                        var userSubF = new UserSubFormatted {
                            UserSubID = userSub.Id,
                            CompanyName = currCompany[0].Name,
                            SubscriptionTier = currSub[0].SubscriptionTier,
                            MonthlyPrice = currSub[0].MonthlyPrice,
                            StartDate = userSub.StartDate,
                            EndDate = userSub.EndDate,
                            YearlyCost = currSubStat[0].YearlyCost,
                            RunningTotal = currSubStat[0].RunningTotal
                        };
                        userSubFormat.Add(userSubF);
                    }
                    var userTotalStats = from m in _context.UserTotalStats where m.UserId == currentUser.Id select m;
                    List<UserTotalStat> totalStat = new List<UserTotalStat>();
                    foreach (var stat in userTotalStats) {
                        totalStat.Add(stat);
                    }
                    if (totalStat.Count == 0){
                        return View();
                    } else {
                        var viewModel = new SubscriptionsViewModel {
                            SubStats = userSubFormat,
                            TotalStat = totalStat[0],
                            CurrentUser = currentUser
                    };
                    return View(viewModel);
                    }
            }
            else {
                return NotFound();
            }
        }
    }
}
