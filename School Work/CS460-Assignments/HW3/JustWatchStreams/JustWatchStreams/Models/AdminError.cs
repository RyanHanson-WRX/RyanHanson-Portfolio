using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace JustWatchStreams.Models;
public partial class AdminError
{
    public string ErrorMessage { get; set; }
}