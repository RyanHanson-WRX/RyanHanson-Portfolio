using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbProject.Web.Models;

[Table("Companies")]
public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public virtual ICollection<CompanyStat> CompanyStats { get; } = new List<CompanyStat>();

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
