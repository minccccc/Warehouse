namespace WebApi.Models.Responses;

public class QueryProductsResponse
{
    public ProductsSummaryResponse Summary { get; set; }

    public List<ProductResponse> Products { get; set; }
}