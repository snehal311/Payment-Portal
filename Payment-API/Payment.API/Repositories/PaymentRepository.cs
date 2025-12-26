
using System;
using System.Collections.Generic;
using System.Linq;
using Payment.API.Data;
using Payment.API.Models;

namespace Payment.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentsDBContext _context;

        public PaymentRepository(PaymentsDBContext context)
        {
            _context = context;
        }

        public void Add(Payment.API.Models.Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void Delete(Payment.API.Models.Payment payment)
        {
            _context.Payments.Remove(payment);
            _context.SaveChanges();
        }

        public List<Payment.API.Models.Payment> GetAll()
        {
            return _context.Payments.ToList();
        }

        public Payment.API.Models.Payment? GetById(Guid id)
        {
            return _context.Payments.Find(id);
        }

        public void Update(Payment.API.Models.Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public Payment.API.Models.Payment? GetByClientRequestId(Guid clientRequestId)
        {
            return _context.Payments.FirstOrDefault(p => p.ClientRequestId == clientRequestId);
        }
    }
}
