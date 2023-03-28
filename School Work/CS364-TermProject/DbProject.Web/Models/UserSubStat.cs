using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbProject.Web.Models;

[Table("UserSubStats")]
public partial class UserSubStat
{
    public int Id { get; set; }

    [Column("UserSubID")]
    public int UserSubId { get; set; }

    [Precision(32,2)]
    public decimal? YearlyCost { get; set; }

    [Precision(32,2)]
    public decimal? RunningTotal { get; set; }

    public virtual UserSubscription UserSub { get; set; } = null!;
}
