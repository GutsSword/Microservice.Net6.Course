using FreeCourse.Web.Models.Orders;

namespace FreeCourse.Web.Models.FakePayment
{
    public class PaymentInfoViewModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        
        public OrderCreateViewModel Order { get; set; }
    }
}
