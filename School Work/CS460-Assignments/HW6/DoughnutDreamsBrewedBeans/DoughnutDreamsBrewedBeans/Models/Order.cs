using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DoughnutDreamsBrewedBeans.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; }

    public int DeliveryId { get; set; }

    public int StoreId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Complete { get; set; }

    [ForeignKey("DeliveryId")]
    [InverseProperty("Orders")]
    public virtual DeliveryLocation Delivery { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("StoreId")]
    [InverseProperty("Orders")]
    public virtual Store Store { get; set; }
}
