namespace WebApi.Models.Requests;

public class GetFilteredProductsRequest
{
    public double? MinPrice { get; set; }

    public double? MaxPrice { get; set; }

    public List<string> Highlights { get; set; }

    public string Size { get; set; }
}
