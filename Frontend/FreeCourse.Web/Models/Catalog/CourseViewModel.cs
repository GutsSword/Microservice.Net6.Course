namespace FreeCourse.Web.Models.Catalog
{
    public class CourseViewModel
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        private int _shorDescription;

        public string ShortDescription
        {
            get
            {
                if(Description.Length>100)
                    return Description.Substring(0,100) + "..." ;

                else return Description ;
            }
            

        }

        public string StockPictureUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public FeatureViewModel Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }

    }
}
