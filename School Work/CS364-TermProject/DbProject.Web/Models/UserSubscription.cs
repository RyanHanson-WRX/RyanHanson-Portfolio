using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DbProject.Web.Models;

[Table("UserSubscriptions")]
public partial class UserSubscription
{
    public int Id { get; set; }

    public int SubscriptionId { get; set; }

    public int UserId { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    //public virtual Subscription Subscription { get; set; } = null!;

    //public virtual User User { get; set; } = null!;

    public virtual ICollection<UserSubStat> UserSubStats { get; } = new List<UserSubStat>();
}
