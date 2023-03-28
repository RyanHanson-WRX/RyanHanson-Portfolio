using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DbProject.Web.Models;

public class UserSubFormatted
{
    public int UserSubID {get; set;}
    public string CompanyName {get; set;} = null!;
    public string SubscriptionTier {get; set;} = null!;
    public decimal MonthlyPrice {get; set;}

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Precision(32,2)]
    public decimal? YearlyCost { get; set; }

    [Precision(32,2)]
    public decimal? RunningTotal { get; set; }

}