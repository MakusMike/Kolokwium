using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models;

public class Subscription
{
    [Key]
    public int IdSubscription { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public int RenewalPeriod { get; set; }

    public DateTime EndTime { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public ICollection<Sale> Sales { get; set; }
    public ICollection<Discount> Discounts { get; set; }
    public ICollection<Payment> Payments { get; set; }
}