namespace Domain.Models;

public class ProductsSummary
{
    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public List<string> Sizes { get; set; }

    public List<string> Highlights { get; set; }
}