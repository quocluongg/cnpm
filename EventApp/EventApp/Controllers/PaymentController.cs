using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using EventApp.Models.Dtos;
using EventApp.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController(IUnitOfWork unitOfWork) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentDto? paymentDto)
    {
        if (paymentDto == null)
        {
            return BadRequest("Payment data is required.");
        }

        paymentDto.Id = 0;
        paymentDto.ExpiryDate = DateTime.UtcNow.AddDays(30); // Set expiry date to 30 days from now
        paymentDto.Status = SD.PaymentStatusPending; // Set status to pending by default
        var payment = paymentDto.Adapt<Payment>();

        var result = await unitOfWork.Payments.AddAsync(payment);
        await unitOfWork.CompleteAsync();

        return Created(string.Empty, result.Adapt<PaymentDto>());
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayment(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        return Ok(payment.Adapt<PaymentDto>());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await unitOfWork.Payments.GetAllAsync();
        if (!payments.Any())
        {
            return NotFound("No payments found.");
        }

        return Ok(payments.Adapt<IEnumerable<PaymentDto>>());
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentDto? paymentDto)
    {
        if (paymentDto == null || paymentDto.Id != id)
        {
            return BadRequest("Invalid payment data.");
        }

        var existingPayment = await unitOfWork.Payments.GetByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        existingPayment = paymentDto.Adapt(existingPayment);
        unitOfWork.Payments.Update(existingPayment);
        await unitOfWork.CompleteAsync();

        return Ok(existingPayment.Adapt<PaymentDto>());
    }
    
    [HttpPut("{id:int}/complete/stripe")]
    public async Task<IActionResult> CompletePaymentByStripe(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }
        
        if (payment.ExpiryDate < DateTime.UtcNow)
        {
            return BadRequest("Cannot complete payment as it has expired.");
        }
        
        payment.Status = SD.PaymentStatusSuccess;
        payment.PaymentTime = DateTime.UtcNow; // Set the payment time to now
        payment.Method = SD.PaymentMethodStripe; // Set the payment method to Stripe
        payment.TransactionCode = Guid.NewGuid().ToString(); // Generate a unique transaction code
        
        unitOfWork.Payments.Update(payment);
        await unitOfWork.CompleteAsync();

        return Ok(payment.Adapt<PaymentDto>());
    }
    
    [HttpPut("{id:int}/complete/paypal")]
    public async Task<IActionResult> CompletePaymentByPayPal(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }
        
        if (payment.ExpiryDate < DateTime.UtcNow)
        {
            return BadRequest("Cannot complete payment as it has expired.");
        }
        
        payment.Status = SD.PaymentStatusSuccess;
        payment.PaymentTime = DateTime.UtcNow; // Set the payment time to now
        payment.Method = SD.PaymentMethodPaypal; // Set the payment method to PayPal
        payment.TransactionCode = Guid.NewGuid().ToString(); // Generate a unique transaction code
        
        unitOfWork.Payments.Update(payment);
        await unitOfWork.CompleteAsync();

        return Ok(payment.Adapt<PaymentDto>());
    }
    
    [HttpPut("{id:int}/complete/cash")]
    public async Task<IActionResult> CompletePaymentByCash(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }
        
        if (payment.ExpiryDate < DateTime.UtcNow)
        {
            return BadRequest("Cannot complete payment as it has expired.");
        }
        
        payment.Status = SD.PaymentStatusSuccess;
        payment.PaymentTime = DateTime.UtcNow; // Set the payment time to now
        payment.Method = SD.PaymentMethodCash; // Set the payment method to Cash
        payment.TransactionCode = Guid.NewGuid().ToString(); // Generate a unique transaction code
        
        unitOfWork.Payments.Update(payment);
        await unitOfWork.CompleteAsync();

        return Ok(payment.Adapt<PaymentDto>());
    }

    [HttpPut("{id:int}/pending")]
    public async Task<IActionResult> UpdatePaymentStatusToPending(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        payment.Status = SD.PaymentStatusPending;
        payment.PaymentTime = null; // Clear the payment time
        payment.Method = null; // Clear the payment method
        payment.TransactionCode = null; // Clear the transaction code
        
        unitOfWork.Payments.Update(payment);
        await unitOfWork.CompleteAsync();

        return Ok(payment.Adapt<PaymentDto>());
    }
    
    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> UpdatePaymentStatusToCancel(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        payment.Status = SD.PaymentStatusFailed;
        payment.PaymentTime = null; // Clear the payment time
        payment.Method = null; // Clear the payment method
        payment.TransactionCode = null; // Clear the transaction code
        payment.Description = "Payment has been cancelled."; // Optionally set a description
        
        unitOfWork.Payments.Update(payment);
        await unitOfWork.CompleteAsync();

        return Ok(payment.Adapt<PaymentDto>());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        unitOfWork.Payments.Remove(payment);
        await unitOfWork.CompleteAsync();

        return NoContent();
    }
}