using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbProject.Web.Models;

[Table("Users")]
public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string UserName { get; set; } = null!;

    public string? AspNetUserId { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<UserSubscription> UserSubscriptions { get; } = new List<UserSubscription>();

    public virtual ICollection<UserTotalStat> UserTotalStats { get; } = new List<UserTotalStat>();
}
