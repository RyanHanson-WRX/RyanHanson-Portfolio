using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbProject.Web.Models;

[Table("UserTotalStats")]
public partial class UserTotalStat
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Precision(32,2)]
    public decimal? TotalSubYearlyCost { get; set; }

    [Precision(32,2)]
    public decimal? AllSubRunningTotal { get; set; }

    //public virtual User User { get; set; } = null!;
}
