using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.API.Data;
using Payment.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsDBContext _context;
        public PaymentsController(PaymentsDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Models.CreatePaymentRequest request)
        {
            if (request.Amount <= 0)
                return BadRequest("Amount must be greater than zero");

            string[] currencies = { "USD", "EUR", "INR", "GBP" };
            if (!currencies.Contains(request.Currency))
                return BadRequest("Invalid currency");

            var existing = await _context.Payments
                .FirstOrDefaultAsync(p => p.ClientRequestId == request.ClientRequestId);

            if (existing != null)
                return Ok(existing);

            var today = DateTime.UtcNow.Date;
            var count = await _context.Payments
                .CountAsync(p => p.CreatedAt.Date == today);

            var payment = new Payment.API.Models.Payment
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Currency = request.Currency,
                ClientRequestId = request.ClientRequestId,
                CreatedAt = DateTime.UtcNow,
                Reference = $"PAY-{today:yyyyMMdd}-{(count + 1):D4}"
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _context.Payments.ToListAsync();
            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreatePaymentRequest request)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            payment.Amount = request.Amount;
            payment.Currency = request.Currency;
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
