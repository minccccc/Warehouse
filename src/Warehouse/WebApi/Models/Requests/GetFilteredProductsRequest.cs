namespace WebApi.Models.Requests;

public class GetFilteredProductsRequest
{
    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public List<string> Highlights { get; set; }

    public string Size { get; set; }
}
