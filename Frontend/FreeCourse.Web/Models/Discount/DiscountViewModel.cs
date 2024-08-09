namespace FreeCourse.Web.Models.Discount
{
    public class DiscountViewModel
    {
        public int DiscountId { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}
