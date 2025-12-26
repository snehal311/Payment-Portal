using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Payment.API.DTOs;
using Payment.API.Services;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequest request)
        {
            try
            {
                if (request.Amount <= 0)
                    return BadRequest("Amount must be greater than zero");

                string[] currencies = { "USD", "EUR", "INR", "GBP" };
                if (!currencies.Contains(request.Currency))
                    return BadRequest("Invalid currency");

                var payment = _service.Create(request);
                return Ok(payment);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the payment.");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var list = _service.GetAll();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving payments.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPaymentById(Guid id)
        {
            try
            {
                var payment = _service.GetById(id);
                if (payment == null) return NotFound();
                return Ok(payment);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the payment.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdatePaymentRequest request)
        {
            try
            {
                var payment = _service.GetById(id);
                if (payment == null) return NotFound();

                var updated = _service.Update(id, request);
                return Ok(updated);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the payment.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return _service.Delete(id) ? NoContent() : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the payment.");
            }
        }
    }
}
