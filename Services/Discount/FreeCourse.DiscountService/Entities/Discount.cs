namespace FreeCourse.DiscountService.Entities
{
    [Dapper.Contrib.Extensions.Table("discount")]   // Postgre Sql deki table alanına mapler.
    public class Discount
    {
        public int DiscountId { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
