using Microsoft.Extensions.Options;
using Stripe;
using StripeGateway.API.Models;
using System;
using System.Collections.Generic;

namespace StripeGateway.API.Services
{
    public class PaymentService : IPaymentService
    {
        public bool PayWithCard(PaymentModel paymentModel)
        {
            try
            {
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = paymentModel.Amount,
                    Currency = "eur",
                    Source = paymentModel.Token,
                    Description = paymentModel.Description
                };
                var service = new ChargeService();
                var response = service.Create(chargeOptions);

                if (response != null && response.Status.ToLower() == "succeeded")
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return false;
        }

        public void PaymentLink(PriceOption priceOption)
        {
            // Set your secret key. Remember to switch to your live secret key in production.
            // See your keys here: https://dashboard.stripe.com/apikeys
            try
            {

                var options = new PriceCreateOptions
                {
                    Currency = priceOption.Currency,
                    UnitAmount = priceOption.UnitAmount * 100,
                    ProductData=new PriceProductDataOptions { Name="product", StatementDescriptor="This is new product"}
                };
                var service = new PriceService();
                var price = service.Create(options);

                var options1 = new PaymentLinkCreateOptions
                {
                    LineItems = new List<PaymentLinkLineItemOptions>
                    {
                        new PaymentLinkLineItemOptions { Price = price.Id, Quantity = 1 },
                    },
                    AfterCompletion = new PaymentLinkAfterCompletionOptions
                    {
                        Type = "redirect",
                        Redirect = new PaymentLinkAfterCompletionRedirectOptions
                        {
                            Url = "https://example.com",

                        },
                    },
                };
                var service1 = new PaymentLinkService();
                var paymentLink = service1.Create(options1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
