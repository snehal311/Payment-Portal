namespace Payment.API.DTOs
{
    public class UpdatePaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
