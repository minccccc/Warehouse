namespace WebApi.Models.Responses;

public class ProductResponse
{
    public string Title { get; set; }

    public double Price { get; set; }

    public List<string> Sizes { get; set; }

    public string Description { get; set; }
}