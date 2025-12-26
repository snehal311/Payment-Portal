namespace Payment.API.Models
{
    public class CreatePaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public Guid ClientRequestId { get; set; }
    }
}
