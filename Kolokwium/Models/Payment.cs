using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models;

public class Payment
{
    [Key]
    public int IdPayment { get; set; }

    public DateTime Date { get; set; }

    public int IdClient { get; set; }
    public Client Client { get; set; }

    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }
    
    [Column(TypeName = "money")]
    public decimal Amount { get; set; }
}