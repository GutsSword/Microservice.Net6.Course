namespace FreeCourse.Web.Models.Orders
{
    public class OrderCreateViewModel
    {
        public OrderCreateViewModel()
        {
           OrderItems = new List<OrderItemCreateViewModel>();
        }
        public string BuyerId { get; set; }
        public List<OrderItemCreateViewModel> OrderItems { get; set; }
        public AddressCreateViewModel Address { get; set; }
    }
}
