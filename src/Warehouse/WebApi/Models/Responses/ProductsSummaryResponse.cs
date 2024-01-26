namespace WebApi.Models.Responses;

public class ProductsSummaryResponse
{
    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    public List<string> Sizes { get; set; }

    public List<string> Highlights { get; set; }
}