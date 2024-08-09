using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient httpClient;

        public PaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoViewModel paymentInfoViewModel)
        {
            var response = await httpClient.PostAsJsonAsync("fakepayment",paymentInfoViewModel);

            if(!response.IsSuccessStatusCode)
            {
                return false;
            }

            return response.IsSuccessStatusCode;
        }
    }
}
