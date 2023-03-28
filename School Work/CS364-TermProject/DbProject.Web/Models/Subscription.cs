using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbProject.Web.Models;

[Table("Subscriptions")]
public partial class Subscription
{
    public int Id { get; set; }

    [Precision(18,2)]
    public decimal MonthlyPrice { get; set; }

    public string? SubscriptionTier { get; set; }

    public int CompanyId { get; set; }

   /* public virtual Company Company { get; set; } = null!;*/

    public virtual ICollection<UserSubscription> UserSubscriptions { get; } = new List<UserSubscription>();
}
