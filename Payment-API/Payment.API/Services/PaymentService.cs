using System;
using System.Collections.Generic;
using System.Linq;
using Payment.API.DTOs;
using Payment.API.Repositories;

namespace Payment.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Payment.API.Models.Payment Create(CreatePaymentRequest request)
        {
            var existing = _paymentRepository.GetByClientRequestId(request.ClientRequestId);
            if (existing != null)
                return existing;

            var today = DateTime.UtcNow.Date;
            var countToday = _paymentRepository.GetAll()
                .Count(p => p.CreatedAt.Date == today);

            var payment = new Payment.API.Models.Payment
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Currency = request.Currency,
                ClientRequestId = request.ClientRequestId,
                CreatedAt = DateTime.UtcNow,
                Reference = $"PAY-{today:yyyyMMdd}-{(countToday + 1):D4}"
            };

            _paymentRepository.Add(payment);

            return payment;
        }

        public bool Delete(Guid id)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null) return false;

            _paymentRepository.Delete(payment);
            return true;
        }

        public List<Payment.API.Models.Payment> GetAll()
        {
            return _paymentRepository.GetAll();
        }

        public Payment.API.Models.Payment? Update(Guid id, UpdatePaymentRequest request)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null) return null;

            payment.Amount = request.Amount;
            payment.Currency = request.Currency;

            _paymentRepository.Update(payment);
            return payment;
        }

        public Payment.API.Models.Payment? GetById(Guid id)
        {
            return _paymentRepository.GetById(id);
        }
    }
}
