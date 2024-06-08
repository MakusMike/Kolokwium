using System;
using System.Linq;
using Kolokwium.Models;

namespace Kolokwium.Services;

public class PaymentService
{
    private readonly AppContext.AppContext _context;

    public PaymentService(AppContext.AppContext context)
    {
        _context = context;
    }

    public (bool Success, string Message, int? PaymentId) AddPayment(int clientId, int subscriptionId, decimal paymentAmount)
    {
        var client = _context.Clients.Find(clientId);
        if (client == null)
        {
            return (false, "Klient nie znaleziony", null);
        }
        
        var subscription = _context.Subscriptions.Find(subscriptionId);
        if (subscription == null)
        {
            return (false, "Subskrypcja nie znaleziona", null);
        }
        
        if (subscription.EndTime <= DateTime.Now)
        {
            return (false, "Subskrypcja nieaktywna", null);
        }
        
        var lastPayment = _context.Payments
            .Where(p => p.IdClient == clientId && p.IdSubscription == subscriptionId)
            .OrderByDescending(p => p.Date)
            .FirstOrDefault();

        if (lastPayment != null)
        {
            var nextPaymentDueDate = lastPayment.Date.AddMonths(subscription.RenewalPeriod);
            if (nextPaymentDueDate > DateTime.Now)
            {
                return (false, "Juz zaplaciles", null);
            }
        }
        
        var discount = _context.Discounts
            .Where(d => d.IdSubscription == subscriptionId && d.DateFrom <= DateTime.Now && d.DateTo >= DateTime.Now)
            .OrderByDescending(d => d.Value)
            .FirstOrDefault();

        var finalPaymentAmount = paymentAmount;
        if (discount != null)
        {
            finalPaymentAmount -= (finalPaymentAmount * discount.Value / 100);
        }

        if (finalPaymentAmount != subscription.Price)
        {
            return (false, "Zła kwota", null);
        }

        
        var payment = new Payment
        {
            IdClient = clientId,
            IdSubscription = subscriptionId,
            Date = DateTime.Now,
            Amount = finalPaymentAmount
        };
        _context.Payments.Add(payment);
        _context.SaveChanges();

        return (true, "Platnosc udana", payment.IdPayment);
    }
}