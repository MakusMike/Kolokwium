using System.Linq;
using Kolokwium.DTOs;
using Kolokwium.AppContext;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Services;

public class ClientService
{
    private readonly AppContext.AppContext _context;

    public ClientService(AppContext.AppContext context)
    {
        _context = context;
    }

    public ClientDto GetClientWithSubscriptions(int clientId)
    {
        var client = _context.Clients
            .Where(c => c.IdClient == clientId)
            .Select(c => new ClientDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Subscriptions = c.Sales
                    .Select(s => new SubscriptionDto
                    {
                        IdSubscription = s.Subscription.IdSubscription,
                        Name = s.Subscription.Name,
                        TotalPaidAmount = _context.Payments
                            .Where(p => p.IdClient == clientId && p.IdSubscription == s.Subscription.IdSubscription)
                            .Sum(p => p.Amount)
                    })
                    .ToList()
            })
            .FirstOrDefault();

        return client;
    }
}