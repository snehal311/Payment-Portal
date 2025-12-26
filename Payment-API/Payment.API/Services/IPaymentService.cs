using System;
using System.Collections.Generic;
using Payment.API.DTOs;

namespace Payment.API.Services
{
    public interface IPaymentService
    {
        Payment.API.Models.Payment Create(CreatePaymentRequest request);
        List<Payment.API.Models.Payment> GetAll();
        Payment.API.Models.Payment? Update(Guid id, UpdatePaymentRequest request);
        bool Delete(Guid id);
        Payment.API.Models.Payment? GetById(Guid id);
    }
}
