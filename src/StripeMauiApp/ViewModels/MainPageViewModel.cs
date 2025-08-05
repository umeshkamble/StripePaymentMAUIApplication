using CommunityToolkit.Mvvm.ComponentModel;
using StripeMauiApp.Services;
using System.Windows.Input;

namespace StripeMauiApp.ViewModels
{
    public class MainPageViewModel: ObservableObject
    {
        readonly IStripePaymentService paymentService;
        int count = 0;
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string Cvv { get; set; }
        public string Amount { get; set; }
        public ICommand PayCommand { get; set; }
        public MainPageViewModel(IStripePaymentService _paymentService)
        {
            paymentService = _paymentService;
            PayCommand = new Command(pay);
        }

        private async void pay()
        {
            try
            {
                var token = await paymentService.GeneratePaymentToken(new Models.CardModel
                {
                    Number = CardNumber.Replace(" ", string.Empty),
                    ExpMonth = Expiry.Substring(0, 2),
                    ExpYear = Expiry.Remove(0,3),
                    Cvc = Cvv,
                });
                var success = await paymentService.PayWithCard(new Models.PaymentModel
                {
                    Amount = Convert.ToInt16(Amount) * 100,
                    Token = token,
                    Description = "Stripe test payment subscription"
                });

                await App.Current.MainPage.DisplayAlert("Alert", $"Payment Result: { success.ToString()}", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
    }
}
