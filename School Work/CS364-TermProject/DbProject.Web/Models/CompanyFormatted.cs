using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DbProject.Web.Models;

public class CompanyFormatted
{
    public int CompanyId {get; set;}
    public string Name {get; set;} = null!;
    public string URL {get; set;} = null!;

    [Precision(32,2)]
    public decimal? YearlyEarnings {get; set;}

    public int? ActiveSubs { get; set; }

    public int? CancelledSubs { get; set; }

}