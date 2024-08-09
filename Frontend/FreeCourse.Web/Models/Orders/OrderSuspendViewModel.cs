namespace FreeCourse.Web.Models.Orders
{
    public class OrderSuspendViewModel
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public int OrderId { get; set; }
    }
}
