using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public ActionResult AddPayment([FromBody] PaymentRequest paymentRequest)
    {
        var result = _paymentService.AddPayment(paymentRequest.ClientId, paymentRequest.SubscriptionId, paymentRequest.Amount);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(new { PaymentId = result.PaymentId });
    }
}
public class PaymentRequest
{
    public int ClientId { get; set; }
    public int SubscriptionId { get; set; }
    public decimal Amount { get; set; }
}