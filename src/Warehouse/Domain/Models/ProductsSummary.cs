namespace Domain.Models
{
    public class ProductsSummary
    {
        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public List<string> Sizes { get; set; }

        public List<string> Highlights { get; set; }
    }
}
