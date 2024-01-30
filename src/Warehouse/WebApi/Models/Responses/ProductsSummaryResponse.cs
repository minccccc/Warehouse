namespace WebApi.Models.Responses;

public class ProductsSummaryResponse
{
    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public List<string> Sizes { get; set; }

    public List<string> Highlights { get; set; }
}