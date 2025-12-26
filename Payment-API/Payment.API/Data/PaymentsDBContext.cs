using Microsoft.EntityFrameworkCore;

namespace Payment.API.Data
{
    public class PaymentsDBContext : DbContext
    {
        public PaymentsDBContext(DbContextOptions<PaymentsDBContext> options) : base(options)
        {
        }
        public DbSet<Payment.API.Models.Payment> Payments { get; set; }
    }
}
