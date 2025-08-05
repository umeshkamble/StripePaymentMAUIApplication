namespace StripeGateway.API.Models
{
    public class PaymentModel
    {
        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }

    }
}
