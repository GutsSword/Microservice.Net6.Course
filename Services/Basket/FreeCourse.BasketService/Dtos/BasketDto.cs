namespace FreeCourse.BasketService.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }

        public List<BasketItemDto> basketItem {  get; set; }
        public decimal TotalPrice
        {
            get => basketItem.Sum(b => b.Quantity * b.Price);
        }
        
    }
}
