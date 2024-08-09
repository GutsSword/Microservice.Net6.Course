namespace FreeCourse.Web.Models.Discount
{
    public class DiscountApplyViewModel
    {
        public int DiscountId { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
    }
}
