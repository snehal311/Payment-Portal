using Microsoft.EntityFrameworkCore;

namespace Payment.API.Data
{
    public class PaymentsDBContext : DbContext
    {
        public PaymentsDBContext(DbContextOptions<PaymentsDBContext> options) : base(options)
        {
        }
        public DbSet<Payment.API.Models.Payment> Payments => Set<Payment.API.Models.Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var paymentEntity = modelBuilder.Entity<Payment.API.Models.Payment>();
            paymentEntity.ToTable("Payment");
            paymentEntity.HasIndex(p => p.ClientRequestId)
                .IsUnique();
        }
    }
}
