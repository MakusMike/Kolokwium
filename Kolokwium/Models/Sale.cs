using System;
using System.ComponentModel.DataAnnotations;

namespace Kolokwium.Models;

public class Sale
{
    [Key]
    public int IdSale { get; set; }

    public int IdClient { get; set; }
    public Client Client { get; set; }

    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }

    public DateTime CreatedAt { get; set; }
}