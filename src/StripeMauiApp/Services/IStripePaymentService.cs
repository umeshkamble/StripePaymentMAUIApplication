using StripeMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeMauiApp.Services
{
    public interface IStripePaymentService
    {
        Task<bool> PayWithCard(PaymentModel paymentModel);
        Task<string> GeneratePaymentToken(CardModel cardModel);
        void PaymentLink(PriceOption priceOption);
    }
}
