using System;
using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models;

public class Discount
{
    [Key]
    public int IdDiscount { get; set; }

    public int Value { get; set; }

    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }

    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}