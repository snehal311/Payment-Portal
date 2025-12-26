using System;
using System.Collections.Generic;

namespace Payment.API.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment.API.Models.Payment> GetAll();
        Payment.API.Models.Payment? GetById(Guid id);
        Payment.API.Models.Payment? GetByClientRequestId(Guid clientRequestId);
        void Add(Payment.API.Models.Payment payment);
        void Delete(Payment.API.Models.Payment payment);
        void Update(Payment.API.Models.Payment payment);
    }
}
