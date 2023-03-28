using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbProject.Web.Models;

[Table("CompanyStats")]
public partial class CompanyStat
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    [Precision(32,2)]
    public decimal? YearlyEarnings { get; set; }

    public int? ActiveSubCount { get; set; }

    public int? CancelledSubCount { get; set; }

    //public virtual Company Company { get; set; } = null!;
}
