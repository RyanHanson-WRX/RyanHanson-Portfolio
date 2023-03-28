using System;
using System.Collections.Generic;

namespace DbProject.Web.Models;

public class SubscriptionsViewModel 
{
    public List<UserSubFormatted> SubStats {get; set;} = null!;
    public UserTotalStat TotalStat {get; set;} = null!;
    public User CurrentUser {get; set;} = null!;

}