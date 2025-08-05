using Newtonsoft.Json;
using Stripe;
using StripeMauiApp.Models;
using System.Text;

namespace StripeMauiApp.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        //Deploy api devTunnels link https://14r8wsdt-27878.inc1.devtunnels.ms/
        string apiUri = "https://14r8wsdt-27878.inc1.devtunnels.ms/api/Payments/";
        public async Task<bool> PayWithCard(PaymentModel paymentModel)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiUri) })
            {
                try
                {
                    var content = JsonConvert.SerializeObject(paymentModel);
                    HttpContent postContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("PayWithCard", postContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return default;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception exp)
                {
                    throw;
                }
            }
        }

        public async Task<string> GeneratePaymentToken(CardModel cardModel)
        {
            StripeConfiguration.ApiKey = "pk_test_51QUnj9JBmhTAAHG0b51H9vlYD9WvIOOejikoNl5vuTaI7TM8tyUn3ROuiS1ejpIUaoc0lbWqQjFyRjDpyyy3dCD800JM8HTQtR";

            var option = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = cardModel.Number,
                    ExpMonth = cardModel.ExpMonth,
                    ExpYear = cardModel.ExpYear,
                    Cvc = cardModel.Cvc,
                    Currency = "EUR",
                    Name = cardModel.Name,
                    AddressCity = cardModel.AddressCity,
                    AddressZip = cardModel.AddressZip,
                    AddressLine1 = cardModel.AddressLine1,
                    AddressCountry = cardModel.AddressCountry
                }
            };

            var service = new TokenService();
            var token = await service.CreateAsync(option);
            return token.Id;
        }

        public async void PaymentLink(PriceOption priceOption)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiUri) })
            {
                try
                {
                    var content = JsonConvert.SerializeObject(priceOption);
                    HttpContent postContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("PaymentLink", postContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        //return default;
                    }
                    else
                    {
                        //return true;
                    }
                }
                catch (Exception exp)
                {
                    throw;
                }
            }
        }
    }
}
